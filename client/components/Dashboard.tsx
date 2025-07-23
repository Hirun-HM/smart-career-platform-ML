import { useState, useEffect } from 'react';
import { User, CareerPath, Course } from '../utils/api';
import { mlAPI, courseAPI } from '../utils/api';
import CourseRecommendations from './CourseRecommendations';
import SkillAssessment from './SkillAssessment';
import ProgressTracker from './ProgressTracker';
import CareerPathComponent from './CareerPath';

interface DashboardProps {
  user: User;
}

export default function Dashboard({ user }: DashboardProps) {
  const [activeTab, setActiveTab] = useState('overview');
  const [careerPrediction, setCareerPrediction] = useState<CareerPath | null>(null);
  const [recommendations, setRecommendations] = useState<Course[]>([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const fetchDashboardData = async () => {
      try {
        // Get career prediction
        const careerResponse = await mlAPI.predictCareer({
          skills: user.skills,
          interests: user.interests,
          experience: user.experience
        });
        setCareerPrediction(careerResponse.data);

        // Get course recommendations
        const courseResponse = await courseAPI.getRecommendations(user.id);
        setRecommendations(courseResponse.data);
      } catch (error) {
        console.error('Error fetching dashboard data:', error);
      } finally {
        setLoading(false);
      }
    };

    fetchDashboardData();
  }, [user]);

  const tabs = [
    { id: 'overview', label: 'Overview', icon: '📊' },
    { id: 'career', label: 'Career Path', icon: '🎯' },
    { id: 'courses', label: 'Courses', icon: '📚' },
    { id: 'skills', label: 'Skills', icon: '🛠️' },
    { id: 'progress', label: 'Progress', icon: '📈' }
  ];

  const handleLogout = () => {
    localStorage.removeItem('token');
    window.location.href = '/login';
  };

  if (loading) {
    return (
      <div className="min-h-screen bg-gray-100 flex items-center justify-center">
        <div className="text-xl">Loading dashboard...</div>
      </div>
    );
  }

  return (
    <div className="min-h-screen bg-gray-100">
      {/* Header */}
      <header className="bg-white shadow-sm border-b">
        <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
          <div className="flex justify-between items-center py-4">
            <div className="flex items-center">
              <h1 className="text-2xl font-bold text-gray-900">Smart Career Platform</h1>
            </div>
            <div className="flex items-center space-x-4">
              <span className="text-gray-700">Welcome, {user.username}!</span>
              <button
                onClick={handleLogout}
                className="bg-red-600 text-white px-4 py-2 rounded-md hover:bg-red-700 transition-colors"
              >
                Logout
              </button>
            </div>
          </div>
        </div>
      </header>

      {/* Navigation */}
      <nav className="bg-white shadow-sm">
        <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
          <div className="flex space-x-8">
            {tabs.map((tab) => (
              <button
                key={tab.id}
                onClick={() => setActiveTab(tab.id)}
                className={`py-4 px-1 border-b-2 font-medium text-sm ${
                  activeTab === tab.id
                    ? 'border-blue-500 text-blue-600'
                    : 'border-transparent text-gray-500 hover:text-gray-700 hover:border-gray-300'
                }`}
              >
                <span className="mr-2">{tab.icon}</span>
                {tab.label}
              </button>
            ))}
          </div>
        </div>
      </nav>

      {/* Content */}
      <main className="max-w-7xl mx-auto py-6 sm:px-6 lg:px-8">
        <div className="px-4 py-6 sm:px-0">
          {activeTab === 'overview' && (
            <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
              <div className="bg-white overflow-hidden shadow rounded-lg">
                <div className="p-5">
                  <div className="flex items-center">
                    <div className="flex-shrink-0">
                      <span className="text-2xl">🎯</span>
                    </div>
                    <div className="ml-5 w-0 flex-1">
                      <dl>
                        <dt className="text-sm font-medium text-gray-500 truncate">
                          Predicted Career
                        </dt>
                        <dd className="text-lg font-medium text-gray-900">
                          {careerPrediction?.title || 'Loading...'}
                        </dd>
                      </dl>
                    </div>
                  </div>
                </div>
              </div>

              <div className="bg-white overflow-hidden shadow rounded-lg">
                <div className="p-5">
                  <div className="flex items-center">
                    <div className="flex-shrink-0">
                      <span className="text-2xl">📚</span>
                    </div>
                    <div className="ml-5 w-0 flex-1">
                      <dl>
                        <dt className="text-sm font-medium text-gray-500 truncate">
                          Recommended Courses
                        </dt>
                        <dd className="text-lg font-medium text-gray-900">
                          {recommendations.length}
                        </dd>
                      </dl>
                    </div>
                  </div>
                </div>
              </div>

              <div className="bg-white overflow-hidden shadow rounded-lg">
                <div className="p-5">
                  <div className="flex items-center">
                    <div className="flex-shrink-0">
                      <span className="text-2xl">🛠️</span>
                    </div>
                    <div className="ml-5 w-0 flex-1">
                      <dl>
                        <dt className="text-sm font-medium text-gray-500 truncate">
                          Current Skills
                        </dt>
                        <dd className="text-lg font-medium text-gray-900">
                          {user.skills.length}
                        </dd>
                      </dl>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          )}

          {activeTab === 'career' && careerPrediction && (
            <CareerPathComponent careerPath={careerPrediction} user={user} />
          )}            {activeTab === 'courses' && (
              <CourseRecommendations 
                userId={user.id}
                userSkills={user.skills.map(skill => skill.name)}
                targetSkills={user.interests}
                maxRecommendations={12}
              />
            )}

          {activeTab === 'skills' && (
            <SkillAssessment user={user} />
          )}

          {activeTab === 'progress' && (
            <ProgressTracker user={user} />
          )}
        </div>
      </main>
    </div>
  );
}