# 🤖 ML Model Testing Guide

## How to Run and Test the Machine Learning Model

### 1. Start the ML Service

```bash
# Terminal 1: Start ML Service
./start-ml-service.sh
```

The ML service will start on **http://localhost:5001**

### 2. Start the Backend Server

```bash
# Terminal 2: Start Backend Server
./start-server.sh
```

The backend will start on **http://localhost:5000**

### 3. Test ML Service Endpoints

#### Check if ML service is running:
```bash
curl http://localhost:5001/health
```

Expected response:
```json
{
  "status": "healthy",
  "message": "ML service is running"
}
```

#### Store sample courses for training:
```bash
curl -X POST http://localhost:5001/store-courses \
  -H "Content-Type: application/json" \
  -d '{
    "courses": [
      {
        "id": 1,
        "title": "Python for Data Science",
        "description": "Learn Python programming for data analysis and machine learning",
        "skills": ["Python", "Data Analysis", "Machine Learning"],
        "category": "Programming",
        "level": "Beginner",
        "provider": "Internal",
        "rating": 4.5,
        "duration": 40
      },
      {
        "id": 2,
        "title": "Advanced Machine Learning",
        "description": "Deep dive into advanced ML algorithms and techniques",
        "skills": ["Machine Learning", "Deep Learning", "Python"],
        "category": "AI",
        "level": "Advanced",
        "provider": "Internal",
        "rating": 4.8,
        "duration": 60
      }
    ]
  }'
```

#### Store sample user interaction:
```bash
curl -X POST http://localhost:5001/store-interaction \
  -H "Content-Type: application/json" \
  -d '{
    "user_id": 1,
    "course_id": 1,
    "interaction_type": "enroll",
    "rating": 4.5,
    "user_skills": ["Python", "Programming"],
    "user_experience": 2
  }'
```

#### Train the ML model:
```bash
curl -X POST http://localhost:5001/train-model \
  -H "Content-Type: application/json"
```

#### Get ML recommendations:
```bash
curl -X POST http://localhost:5001/recommend-courses \
  -H "Content-Type: application/json" \
  -d '{
    "user_id": 1,
    "user_skills": ["Python", "Programming"],
    "user_experience": 2,
    "limit": 5
  }'
```

Expected response:
```json
{
  "recommendations": [
    {
      "course_id": 2,
      "title": "Advanced Machine Learning",
      "description": "Deep dive into advanced ML algorithms...",
      "skills": ["Machine Learning", "Deep Learning", "Python"],
      "recommendation_score": 0.85,
      "recommendation_reason": "Based on your learning history and preferences"
    }
  ],
  "count": 1
}
```

### 4. Test Backend Integration

#### Sync courses from Coursera (requires API credentials):
```bash
curl -X POST http://localhost:5000/api/course-recommendations/sync-coursera
```

#### Train model via backend:
```bash
curl -X POST http://localhost:5000/api/course-recommendations/train-model
```

#### Get recommendations for a user:
```bash
curl http://localhost:5000/api/course-recommendations/user/1
```

### 5. Test Full Integration

1. **Start Frontend:**
   ```bash
   # Terminal 3: Start Frontend
   ./start-client.sh
   ```

2. **Access the application:** http://localhost:3000

3. **Create a user account** and complete the profile

4. **View recommendations** in the dashboard

## ML Model Architecture

### Content-Based Filtering
- **TF-IDF Vectorization**: Analyzes course titles, descriptions, and skills
- **Skill Matching**: Compares user skills with course requirements
- **Experience Level**: Matches course difficulty with user experience

### Collaborative Filtering
- **User-Course Interactions**: Learns from user behavior (views, enrollments, ratings)
- **Similarity Scoring**: Finds users with similar interests and skills
- **Preference Learning**: Adapts to user preferences over time

### Hybrid Approach
- **Random Forest Ensemble**: Combines multiple recommendation strategies
- **Feature Engineering**: Uses course metadata, user profiles, and interaction history
- **Continuous Learning**: Model improves with more user data

## Troubleshooting

### ML Service Issues
- **Port 5001 busy**: Change port in `ml-service/app.py`
- **Package errors**: Reinstall requirements: `pip install -r requirements.txt`
- **Model training fails**: Ensure enough interaction data exists

### Integration Issues
- **Backend can't connect to ML**: Check ML service URL in `appsettings.json`
- **CORS errors**: ML service has CORS enabled for localhost
- **Database errors**: Check PostgreSQL connection or use SQLite for development

### Performance Issues
- **Slow recommendations**: Consider caching and model optimization
- **Memory usage**: Limit course dataset size during development
- **Training time**: Start with smaller datasets and scale up

## Model Performance Metrics

Monitor these metrics to evaluate ML model performance:

1. **Recommendation Accuracy**: How often users interact with recommended courses
2. **User Engagement**: Increase in course views and enrollments
3. **Skill Development**: Progression in user skill assessments
4. **Model Confidence**: Internal scoring from the recommendation engine

The ML model will improve over time as more users interact with the system!
