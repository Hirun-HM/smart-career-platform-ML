#!/bin/bash

echo "Testing Smart Career Platform End-to-End..."
echo

# Test ML Service Health
echo "1. Testing ML Service Health:"
curl -s http://localhost:5001/health | jq .
echo

# Test Backend Skills API
echo "2. Testing Backend Skills API:"
curl -s http://localhost:5002/api/skills | jq '. | length'
echo " skills available"
echo

# Test ML Service Training (with sample data)
echo "3. Storing sample courses:"
curl -X POST -H "Content-Type: application/json" -d '{
  "courses": [
    {"id": 1, "title": "JavaScript Fundamentals", "category": "Programming", "skills": ["JavaScript", "Web Development"]},
    {"id": 2, "title": "React Masterclass", "category": "Frontend", "skills": ["React", "JavaScript", "Frontend"]},
    {"id": 3, "title": "Python for Beginners", "category": "Programming", "skills": ["Python", "Programming"]},
    {"id": 4, "title": "Data Science with Python", "category": "Data Science", "skills": ["Python", "Data Science", "Machine Learning"]}
  ]
}' http://localhost:5001/store-courses
echo

echo "4. Training ML Model:"
curl -X POST http://localhost:5001/train-model
echo

# Test ML Service Recommendations
echo "5. Testing ML Service Recommendations:"
curl -X POST -H "Content-Type: application/json" -d '{
  "user_id": 1,
  "user_skills": ["JavaScript", "Python"],
  "user_experience": 2,
  "limit": 3
}' http://localhost:5001/recommend-courses
echo

# Test Backend Course Recommendations (should call ML service internally)
echo "6. Testing Backend Course Recommendations:"
curl -X POST -H "Content-Type: application/json" -d '{
  "userId": 1,
  "skillIds": [1, 2, 31]
}' http://localhost:5002/api/courserecommendation/recommendations
echo

echo "End-to-End testing completed!"
