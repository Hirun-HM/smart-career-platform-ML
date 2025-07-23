# Smart Career Platform

An AI-powered course recommendation system that integrates with Coursera API to provide personalized learning paths based on user skills and career goals.

## Features

- 🤖 **AI-Powered Recommendations**: Machine learning model that learns from user interactions and preferences
- 📚 **Coursera Integration**: Fetches real courses from Coursera Business API
- 🎯 **Skill-Based Matching**: Recommends courses based on current skills and target learning goals
- 📊 **Interactive Dashboard**: Modern React-based frontend with real-time updates
- 🔄 **Continuous Learning**: ML model retrains based on user interactions and feedback
- 🔐 **Secure Authentication**: JWT-based authentication system
- 📈 **Progress Tracking**: Track learning progress and course completions

## Architecture

### Backend (.NET Core)
- **CourseRecommendationService**: Core recommendation logic and Coursera API integration
- **CourseraApiService**: Interface with Coursera Business API using API keys
- **Repository Pattern**: Clean data access layer with Entity Framework
- **Controllers**: RESTful API endpoints for frontend integration

### ML Service (Python/Flask)
- **course_recommendation_ml.py**: Advanced ML model using scikit-learn
- **Flask API**: Endpoints for training, storing data, and generating recommendations
- **SQLite Database**: Stores courses, user interactions, and training data
- **Content-Based + Collaborative Filtering**: Hybrid recommendation approach

### Frontend (Next.js/React)
- **CourseRecommendations Component**: Interactive course recommendation interface
- **Dashboard**: Comprehensive user dashboard with multiple views
- **Real-time Interactions**: Track user behavior for ML training
- **Responsive Design**: Modern UI with Tailwind CSS

## Setup Instructions

### Prerequisites

- **Node.js** (v18 or higher)
- **Python** (v3.8 or higher)
- **.NET 8 SDK**
- **PostgreSQL** (for production) or **SQLite** (for development)
- **Coursera Business API Access** (API key and Organization ID)

### 1. Clone Repository

```bash
git clone <repository-url>
cd smart-career-platform
```

### 2. Configure Coursera API

1. Sign up for [Coursera for Business](https://www.coursera.org/business)
2. Get your API credentials from the developer dashboard
3. Update `server/appsettings.json`:

```json
{
  "CourseraApi": {
    "BaseUrl": "https://api.coursera.org/api",
    "ApiKey": "YOUR_COURSERA_API_KEY",
    "OrganizationId": "YOUR_ORGANIZATION_ID"
  }
}
```

### 3. Start Services

#### Option A: Using the startup scripts (Recommended)

```bash
# Make scripts executable
chmod +x start-*.sh

# Terminal 1: Start ML Service
./start-ml-service.sh

# Terminal 2: Start Backend Server  
./start-server.sh

# Terminal 3: Start Frontend
./start-client.sh
```

#### Option B: Manual startup

**ML Service:**
```bash
cd ml-service
python3 -m venv venv
source venv/bin/activate  # On Windows: venv\Scripts\activate
pip install -r requirements.txt
python app.py
```

**Backend Server:**
```bash
cd server
dotnet restore
dotnet build
dotnet run
```

**Frontend:**
```bash
cd client
npm install
npm run dev
```

### 4. Initial Data Setup

1. **Sync Coursera Courses:**
   ```bash
   curl -X POST http://localhost:5000/api/course-recommendations/sync-coursera
   ```

2. **Train Initial ML Model:**
   ```bash
   curl -X POST http://localhost:5000/api/course-recommendations/train-model
   ```

## API Endpoints

### Course Recommendations
- `GET /api/course-recommendations/user/{userId}` - Get personalized recommendations
- `POST /api/course-recommendations/advanced` - Advanced recommendations with filters
- `POST /api/course-recommendations/skill-based` - Skill-based recommendations
- `POST /api/course-recommendations/interaction` - Record user interactions
- `POST /api/course-recommendations/train-model` - Trigger ML model training
- `POST /api/course-recommendations/sync-coursera` - Sync courses from Coursera

### ML Service Endpoints
- `POST /store-courses` - Store courses for ML training
- `POST /store-interaction` - Store user interactions
- `POST /train-model` - Train recommendation model
- `POST /recommend-courses` - Get ML-powered recommendations

## Usage

### 1. User Registration & Profile Setup
1. Register a new account
2. Complete skill assessment
3. Set learning goals and interests

### 2. Getting Recommendations
1. Navigate to the Dashboard
2. View AI-powered course recommendations
3. Filter by skills, level, duration, etc.
4. Interact with courses (view, enroll, rate)

### 3. Continuous Learning
- The system learns from your interactions
- Recommendations improve over time
- Regular model retraining for better accuracy

## Configuration

### Environment Variables

**Frontend (.env.local):**
```
NEXT_PUBLIC_API_URL=http://localhost:5000/api
NEXT_PUBLIC_ML_URL=http://localhost:5001
```

**Backend (appsettings.json):**
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=smart_career;Username=postgres;Password=your_password"
  },
  "MLServiceUrl": "http://localhost:5001",
  "CourseraApi": {
    "ApiKey": "your_api_key",
    "OrganizationId": "your_org_id"
  }
}
```

## ML Model Details

### Features Used
- **Course Content**: Title, description, skills required
- **User Profile**: Current skills, experience level, interests
- **Interaction History**: Views, enrollments, completions, ratings
- **Skill Matching**: Overlap between user skills and course requirements

### Training Data
- User-course interactions
- Course metadata from Coursera
- Skill relationships and hierarchies
- User feedback and ratings

### Model Architecture
- **Content-Based Filtering**: TF-IDF vectorization of course content
- **Collaborative Filtering**: User-item matrix factorization
- **Hybrid Approach**: Combines both methods for better recommendations
- **Random Forest**: Ensemble model for prediction

## Development

### Adding New Features

1. **Backend**: Add controllers and services in the `server` directory
2. **ML**: Extend the ML model in `ml-service/course_recommendation_ml.py`
3. **Frontend**: Add React components in `client/components`

### Database Migrations

```bash
cd server
dotnet ef migrations add MigrationName
dotnet ef database update
```

## Deployment

### Production Setup

1. **Database**: Use PostgreSQL for production
2. **ML Service**: Deploy on separate server/container
3. **Backend**: Use IIS or Docker for .NET Core
4. **Frontend**: Deploy to Vercel, Netlify, or similar

### Docker Deployment

```bash
# Build and run with Docker Compose
docker-compose up --build
```

## Troubleshooting

### Common Issues

1. **Coursera API Rate Limits**: Implement caching and request throttling
2. **ML Model Training**: Ensure sufficient interaction data before training
3. **Database Connections**: Check connection strings and database availability
4. **CORS Issues**: Configure CORS policy in backend for frontend domain

### Logs

- **Backend**: Check console output or configure logging to files
- **ML Service**: Python logs show in terminal
- **Frontend**: Browser console for client-side issues

## Contributing

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Add tests
5. Submit a pull request

## License

This project is licensed under the MIT License - see the LICENSE file for details.

## Support

For issues and questions:
1. Check the troubleshooting section
2. Review API documentation
3. Submit an issue on GitHub