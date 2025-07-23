import pandas as pd
import numpy as np
from sklearn.feature_extraction.text import TfidfVectorizer
from sklearn.metrics.pairwise import cosine_similarity
from sklearn.ensemble import RandomForestRegressor
from sklearn.model_selection import train_test_split
from sklearn.preprocessing import LabelEncoder
import joblib
import json
import sqlite3
from datetime import datetime
import logging
from typing import List, Dict, Any, Optional
import os

# Set up logging
logging.basicConfig(level=logging.INFO)
logger = logging.getLogger(__name__)

class CourseRecommendationML:
    def __init__(self, db_path: str = 'recommendations.db'):
        self.db_path = db_path
        self.tfidf_vectorizer = TfidfVectorizer(max_features=1000, stop_words='english')
        self.model = None
        self.skill_encoder = LabelEncoder()
        self.category_encoder = LabelEncoder()
        self.level_encoder = LabelEncoder()
        self.course_features = None
        self.courses_df = None
        self.interactions_df = None
        
        # Initialize database
        self._init_database()
        
    def _init_database(self):
        """Initialize SQLite database for storing training data"""
        conn = sqlite3.connect(self.db_path)
        cursor = conn.cursor()
        
        # Create courses table
        cursor.execute('''
            CREATE TABLE IF NOT EXISTS courses (
                id INTEGER PRIMARY KEY,
                title TEXT NOT NULL,
                description TEXT,
                skills TEXT,
                category TEXT,
                level TEXT,
                provider TEXT,
                rating REAL,
                duration INTEGER,
                created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
            )
        ''')
        
        # Create user interactions table
        cursor.execute('''
            CREATE TABLE IF NOT EXISTS user_interactions (
                id INTEGER PRIMARY KEY AUTOINCREMENT,
                user_id INTEGER NOT NULL,
                course_id INTEGER NOT NULL,
                interaction_type TEXT NOT NULL,
                rating REAL,
                timestamp TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
                user_skills TEXT,
                user_experience INTEGER
            )
        ''')
        
        # Create user profiles table
        cursor.execute('''
            CREATE TABLE IF NOT EXISTS user_profiles (
                user_id INTEGER PRIMARY KEY,
                skills TEXT,
                interests TEXT,
                experience_level INTEGER,
                career_goals TEXT,
                updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
            )
        ''')
        
        conn.commit()
        conn.close()
        
    def store_courses(self, courses: List[Dict[str, Any]]):
        """Store courses in database"""
        conn = sqlite3.connect(self.db_path)
        cursor = conn.cursor()
        
        for course in courses:
            cursor.execute('''
                INSERT OR REPLACE INTO courses 
                (id, title, description, skills, category, level, provider, rating, duration)
                VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?)
            ''', (
                course.get('id'),
                course.get('title', ''),
                course.get('description', ''),
                json.dumps(course.get('skills', [])),
                course.get('category', ''),
                course.get('level', ''),
                course.get('provider', ''),
                course.get('rating', 0.0),
                course.get('duration', 0)
            ))
        
        conn.commit()
        conn.close()
        logger.info(f"Stored {len(courses)} courses in database")
        
    def store_interaction(self, user_id: int, course_id: int, interaction_type: str, 
                         rating: Optional[float] = None, user_skills: List[str] = None, 
                         user_experience: int = 0):
        """Store user interaction"""
        conn = sqlite3.connect(self.db_path)
        cursor = conn.cursor()
        
        cursor.execute('''
            INSERT INTO user_interactions 
            (user_id, course_id, interaction_type, rating, user_skills, user_experience)
            VALUES (?, ?, ?, ?, ?, ?)
        ''', (
            user_id,
            course_id,
            interaction_type,
            rating,
            json.dumps(user_skills or []),
            user_experience
        ))
        
        conn.commit()
        conn.close()
        
    def store_user_profile(self, user_id: int, skills: List[str], interests: List[str], 
                          experience_level: int, career_goals: List[str]):
        """Store or update user profile"""
        conn = sqlite3.connect(self.db_path)
        cursor = conn.cursor()
        
        cursor.execute('''
            INSERT OR REPLACE INTO user_profiles 
            (user_id, skills, interests, experience_level, career_goals, updated_at)
            VALUES (?, ?, ?, ?, ?, ?)
        ''', (
            user_id,
            json.dumps(skills),
            json.dumps(interests),
            experience_level,
            json.dumps(career_goals),
            datetime.now().isoformat()
        ))
        
        conn.commit()
        conn.close()
        
    def load_data(self):
        """Load data from database"""
        conn = sqlite3.connect(self.db_path)
        
        # Load courses
        self.courses_df = pd.read_sql_query('''
            SELECT * FROM courses
        ''', conn)
        
        # Load interactions
        self.interactions_df = pd.read_sql_query('''
            SELECT * FROM user_interactions
        ''', conn)
        
        conn.close()
        
        if not self.courses_df.empty:
            # Parse JSON fields
            self.courses_df['skills_list'] = self.courses_df['skills'].apply(
                lambda x: json.loads(x) if x else []
            )
            
        if not self.interactions_df.empty:
            self.interactions_df['user_skills_list'] = self.interactions_df['user_skills'].apply(
                lambda x: json.loads(x) if x else []
            )
            
        logger.info(f"Loaded {len(self.courses_df)} courses and {len(self.interactions_df)} interactions")
        
    def prepare_features(self):
        """Prepare features for ML model"""
        if self.courses_df.empty:
            logger.warning("No courses data available for feature preparation")
            return
            
        # Create course content features using TF-IDF
        course_texts = []
        for _, course in self.courses_df.iterrows():
            skills_text = ' '.join(course['skills_list']) if course['skills_list'] else ''
            text = f"{course['title']} {course['description']} {skills_text}"
            course_texts.append(text)
            
        # Fit TF-IDF vectorizer
        tfidf_features = self.tfidf_vectorizer.fit_transform(course_texts)
        
        # Encode categorical features
        categories = self.courses_df['category'].fillna('Unknown')
        levels = self.courses_df['level'].fillna('Beginner')
        
        self.category_encoder.fit(categories)
        self.level_encoder.fit(levels)
        
        # Create feature matrix
        category_encoded = self.category_encoder.transform(categories)
        level_encoded = self.level_encoder.transform(levels)
        
        # Combine features
        basic_features = np.column_stack([
            self.courses_df['rating'].fillna(0),
            self.courses_df['duration'].fillna(0),
            category_encoded,
            level_encoded
        ])
        
        self.course_features = np.hstack([
            tfidf_features.toarray(),
            basic_features
        ])
        
        logger.info(f"Prepared features with shape: {self.course_features.shape}")
        
    def train_model(self):
        """Train the recommendation model"""
        if self.interactions_df.empty:
            logger.warning("No interaction data available for training")
            return
            
        self.load_data()
        self.prepare_features()
        
        # Prepare training data
        training_data = []
        training_labels = []
        
        for _, interaction in self.interactions_df.iterrows():
            course_idx = self.courses_df[self.courses_df['id'] == interaction['course_id']].index
            if len(course_idx) == 0:
                continue
                
            course_idx = course_idx[0]
            user_skills = interaction['user_skills_list']
            
            # Create user skill vector
            user_skill_text = ' '.join(user_skills)
            user_tfidf = self.tfidf_vectorizer.transform([user_skill_text])
            
            # Combine user and course features
            course_features = self.course_features[course_idx].reshape(1, -1)
            user_features = np.hstack([
                user_tfidf.toarray(),
                [[interaction['user_experience']]]
            ])
            
            # Pad or truncate to match dimensions
            min_dim = min(course_features.shape[1], user_features.shape[1])
            combined_features = np.hstack([
                course_features[:, :min_dim],
                user_features[:, :min_dim]
            ])
            
            training_data.append(combined_features.flatten())
            
            # Create label based on interaction type and rating
            label = self._create_interaction_score(interaction)
            training_labels.append(label)
            
        if len(training_data) == 0:
            logger.warning("No valid training data prepared")
            return
            
        X = np.array(training_data)
        y = np.array(training_labels)
        
        # Train model
        self.model = RandomForestRegressor(n_estimators=100, random_state=42)
        self.model.fit(X, y)
        
        # Save model
        self.save_model()
        
        logger.info(f"Model trained on {len(X)} samples")
        
    def _create_interaction_score(self, interaction):
        """Create interaction score based on type and rating"""
        base_scores = {
            'view': 0.1,
            'click': 0.3,
            'enroll': 0.7,
            'complete': 1.0,
            'rate': 0.5
        }
        
        score = base_scores.get(interaction['interaction_type'], 0.1)
        
        if interaction['rating'] and interaction['rating'] > 0:
            # Normalize rating (assuming 1-5 scale)
            rating_score = interaction['rating'] / 5.0
            score = (score + rating_score) / 2
            
        return min(score, 1.0)
        
    def get_recommendations(self, user_id: int, user_skills: List[str], 
                          user_experience: int, limit: int = 10) -> List[Dict[str, Any]]:
        """Get course recommendations for a user"""
        try:
            self.load_data()
            
            if self.courses_df.empty:
                return []
                
            # If no model is trained, use content-based filtering
            if self.model is None:
                return self._content_based_recommendations(user_skills, user_experience, limit)
                
            # Use trained model
            return self._model_based_recommendations(user_id, user_skills, user_experience, limit)
            
        except Exception as e:
            logger.error(f"Error generating recommendations: {e}")
            return []
            
    def _content_based_recommendations(self, user_skills: List[str], 
                                     user_experience: int, limit: int) -> List[Dict[str, Any]]:
        """Content-based recommendations using skill matching"""
        recommendations = []
        
        for _, course in self.courses_df.iterrows():
            course_skills = course['skills_list']
            
            # Calculate skill match score
            if user_skills and course_skills:
                skill_overlap = len(set(user_skills) & set(course_skills))
                skill_score = skill_overlap / max(len(user_skills), len(course_skills))
            else:
                skill_score = 0
                
            # Experience level matching
            exp_score = 1.0
            if course['level']:
                level_map = {'Beginner': 1, 'Intermediate': 2, 'Advanced': 3}
                course_level = level_map.get(course['level'], 2)
                exp_diff = abs(course_level - min(user_experience, 3)) / 2
                exp_score = max(0, 1 - exp_diff)
                
            # Combined score
            total_score = (skill_score * 0.7 + exp_score * 0.2 + course['rating'] / 5.0 * 0.1)
            
            recommendations.append({
                'course_id': course['id'],
                'title': course['title'],
                'description': course['description'],
                'skills': course_skills,
                'category': course['category'],
                'level': course['level'],
                'provider': course['provider'],
                'rating': course['rating'],
                'duration': course['duration'],
                'recommendation_score': total_score,
                'recommendation_reason': f"Matches {len(set(user_skills) & set(course_skills))} of your skills"
            })
            
        # Sort by score and return top recommendations
        recommendations.sort(key=lambda x: x['recommendation_score'], reverse=True)
        return recommendations[:limit]
        
    def _model_based_recommendations(self, user_id: int, user_skills: List[str], 
                                   user_experience: int, limit: int) -> List[Dict[str, Any]]:
        """Model-based recommendations using trained ML model"""
        if self.course_features is None:
            self.prepare_features()
            
        recommendations = []
        user_skill_text = ' '.join(user_skills)
        user_tfidf = self.tfidf_vectorizer.transform([user_skill_text])
        
        for idx, course in self.courses_df.iterrows():
            try:
                # Prepare features for prediction
                course_features = self.course_features[idx].reshape(1, -1)
                user_features = np.hstack([
                    user_tfidf.toarray(),
                    [[user_experience]]
                ])
                
                # Ensure feature dimensions match
                min_dim = min(course_features.shape[1], user_features.shape[1])
                combined_features = np.hstack([
                    course_features[:, :min_dim],
                    user_features[:, :min_dim]
                ])
                
                # Predict score
                score = self.model.predict(combined_features)[0]
                
                recommendations.append({
                    'course_id': course['id'],
                    'title': course['title'],
                    'description': course['description'],
                    'skills': course['skills_list'],
                    'category': course['category'],
                    'level': course['level'],
                    'provider': course['provider'],
                    'rating': course['rating'],
                    'duration': course['duration'],
                    'recommendation_score': float(score),
                    'recommendation_reason': 'Based on your learning history and preferences'
                })
                
            except Exception as e:
                logger.warning(f"Error predicting for course {course['id']}: {e}")
                continue
                
        # Sort by predicted score
        recommendations.sort(key=lambda x: x['recommendation_score'], reverse=True)
        return recommendations[:limit]
        
    def save_model(self):
        """Save trained model and vectorizer"""
        try:
            model_dir = 'models'
            os.makedirs(model_dir, exist_ok=True)
            
            if self.model:
                joblib.dump(self.model, f'{model_dir}/recommendation_model.pkl')
            joblib.dump(self.tfidf_vectorizer, f'{model_dir}/tfidf_vectorizer.pkl')
            joblib.dump(self.category_encoder, f'{model_dir}/category_encoder.pkl')
            joblib.dump(self.level_encoder, f'{model_dir}/level_encoder.pkl')
            
            logger.info("Model saved successfully")
        except Exception as e:
            logger.error(f"Error saving model: {e}")
            
    def load_model(self):
        """Load trained model and vectorizer"""
        try:
            model_dir = 'models'
            
            if os.path.exists(f'{model_dir}/recommendation_model.pkl'):
                self.model = joblib.load(f'{model_dir}/recommendation_model.pkl')
            if os.path.exists(f'{model_dir}/tfidf_vectorizer.pkl'):
                self.tfidf_vectorizer = joblib.load(f'{model_dir}/tfidf_vectorizer.pkl')
            if os.path.exists(f'{model_dir}/category_encoder.pkl'):
                self.category_encoder = joblib.load(f'{model_dir}/category_encoder.pkl')
            if os.path.exists(f'{model_dir}/level_encoder.pkl'):
                self.level_encoder = joblib.load(f'{model_dir}/level_encoder.pkl')
                
            logger.info("Model loaded successfully")
        except Exception as e:
            logger.error(f"Error loading model: {e}")
