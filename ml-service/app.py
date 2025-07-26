from flask import Flask, request, jsonify
from flask_cors import CORS
from course_recommendation_ml import CourseRecommendationML
import logging


logging.basicConfig(level=logging.INFO)
logger = logging.getLogger(__name__)

app = Flask(__name__)
CORS(app)  # Enable CORS for frontend integration

# Initialize ML recommendation system
ml_recommender = CourseRecommendationML()
ml_recommender.load_model()

@app.route('/health', methods=['GET'])
def health_check():
    """Health check endpoint"""
    return jsonify({'status': 'healthy', 'message': 'ML service is running'})

@app.route('/store-courses', methods=['POST'])
def store_courses():
    """Store courses from Coursera API"""
    try:
        data = request.json
        courses = data.get('courses', [])
        
        if not courses:
            return jsonify({'error': 'No courses provided'}), 400
            
        ml_recommender.store_courses(courses)
        return jsonify({'message': f'Successfully stored {len(courses)} courses'})
        
    except Exception as e:
        logger.error(f"Error storing courses: {e}")
        return jsonify({'error': str(e)}), 500

@app.route('/store-interaction', methods=['POST'])
def store_interaction():
    """Store user interaction with a course"""
    try:
        data = request.json
        required_fields = ['user_id', 'course_id', 'interaction_type']
        
        for field in required_fields:
            if field not in data:
                return jsonify({'error': f'Missing required field: {field}'}), 400
                
        ml_recommender.store_interaction(
            user_id=data['user_id'],
            course_id=data['course_id'],
            interaction_type=data['interaction_type'],
            rating=data.get('rating'),
            user_skills=data.get('user_skills', []),
            user_experience=data.get('user_experience', 0)
        )
        
        return jsonify({'message': 'Interaction stored successfully'})
        
    except Exception as e:
        logger.error(f"Error storing interaction: {e}")
        return jsonify({'error': str(e)}), 500

@app.route('/store-user-profile', methods=['POST'])
def store_user_profile():
    """Store or update user profile"""
    try:
        data = request.json
        required_fields = ['user_id', 'skills', 'experience_level']
        
        for field in required_fields:
            if field not in data:
                return jsonify({'error': f'Missing required field: {field}'}), 400
                
        ml_recommender.store_user_profile(
            user_id=data['user_id'],
            skills=data['skills'],
            interests=data.get('interests', []),
            experience_level=data['experience_level'],
            career_goals=data.get('career_goals', [])
        )
        
        return jsonify({'message': 'User profile stored successfully'})
        
    except Exception as e:
        logger.error(f"Error storing user profile: {e}")
        return jsonify({'error': str(e)}), 500

@app.route('/train-model', methods=['POST'])
def train_model():
    """Train the recommendation model"""
    try:
        ml_recommender.train_model()
        return jsonify({'message': 'Model trained successfully'})
        
    except Exception as e:
        logger.error(f"Error training model: {e}")
        return jsonify({'error': str(e)}), 500

@app.route('/recommend-courses', methods=['POST'])
def recommend_courses():
    """Get course recommendations for a user"""
    try:
        data = request.json
        required_fields = ['user_id', 'user_skills', 'user_experience']
        
        for field in required_fields:
            if field not in data:
                return jsonify({'error': f'Missing required field: {field}'}), 400
                
        recommendations = ml_recommender.get_recommendations(
            user_id=data['user_id'],
            user_skills=data['user_skills'],
            user_experience=data['user_experience'],
            limit=data.get('limit', 10)
        )
        
        return jsonify({
            'recommendations': recommendations,
            'count': len(recommendations)
        })
        
    except Exception as e:
        logger.error(f"Error getting recommendations: {e}")
        return jsonify({'error': str(e)}), 500

@app.route('/reload-model', methods=['POST'])
def reload_model():
    """Reload the trained model"""
    try:
        ml_recommender.load_model()
        return jsonify({'message': 'Model reloaded successfully'})
        
    except Exception as e:
        logger.error(f"Error reloading model: {e}")
        return jsonify({'error': str(e)}), 500

if __name__ == '__main__':
    app.run(host='0.0.0.0', port=5001, debug=True)