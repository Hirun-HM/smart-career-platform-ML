import { useState, useEffect } from 'react';
import { userAPI, User } from '../utils/api';

interface Progress {
  totalCourses: number;
  completedCourses: number;
  skillsAcquired: number;
  weeklyProgress: number;
  monthlyProgress: number;
  recentAchievements: string[];
}

interface ProgressTrackingProps {
  user: User;
}

export default function ProgressTracking({ user }: ProgressTrackingProps) {
  const [progress, setProgress] = useState<Progress | null>(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const fetchProgress = async () => {
      try {
        const response = await userAPI.getProgress();
        setProgress(response.data);
      } catch (error) {
        console.error('Failed to fetch progress:', error);
        // Mock data for demo
        setProgress({
          totalCourses: 12,
          completedCourses: 7,
          skillsAcquired: user.skills.length,
          weeklyProgress: 15,
          monthlyProgress: 45,
          recentAchievements: [
            'Completed "React Fundamentals" course',
            'Acquired "TypeScript" skill',
            'Reached 80% in Web Development path'
          ]
        });
      } finally {
        setLoading(false);
      }
    };

    fetchProgress();
  }, [user.skills.length]);

  if (loading) {
    return <div className="animate-pulse">Loading progress...</div>;
  }

  if (!progress) return null;

  const courseCompletionRate = (progress.completedCourses / progress.totalCourses) * 100;

  return (
    <div className="bg-white shadow rounded-lg p-6">
      <div className="flex items-center mb-4">
        <span className="text-2xl mr-2">📊</span>
        <h3 className="text-xl font-semibold">Progress Tracking</h3>
      </div>
      
      <p className="text-gray-600 mb-6">
        Monitor your learning progress with AI-powered insights
      </p>

      {/* Progress Stats */}
      <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-4 mb-6">
        <div className="bg-blue-50 rounded-lg p-4">
          <div className="text-2xl font-bold text-blue-600">{progress.completedCourses}</div>
          <div className="text-sm text-gray-600">Courses Completed</div>
          <div className="text-xs text-gray-500">of {progress.totalCourses} enrolled</div>
        </div>
        
        <div className="bg-green-50 rounded-lg p-4">
          <div className="text-2xl font-bold text-green-600">{progress.skillsAcquired}</div>
          <div className="text-sm text-gray-600">Skills Acquired</div>
          <div className="text-xs text-gray-500">across all categories</div>
        </div>
        
        <div className="bg-purple-50 rounded-lg p-4">
          <div className="text-2xl font-bold text-purple-600">{progress.weeklyProgress}%</div>
          <div className="text-sm text-gray-600">Weekly Progress</div>
          <div className="text-xs text-gray-500">this week</div>
        </div>
        
        <div className="bg-orange-50 rounded-lg p-4">
          <div className="text-2xl font-bold text-orange-600">{progress.monthlyProgress}%</div>
          <div className="text-sm text-gray-600">Monthly Progress</div>
          <div className="text-xs text-gray-500">this month</div>
        </div>
      </div>

      {/* Course Completion Progress */}
      <div className="mb-6">
        <h4 className="font-medium text-gray-700 mb-2">Course Completion Rate</h4>
        <div className="w-full bg-gray-200 rounded-full h-3">
          <div 
            className="bg-blue-600 h-3 rounded-full transition-all duration-300"
            style={{ width: `${courseCompletionRate}%` }}
          ></div>
        </div>
        <div className="text-sm text-gray-600 mt-1">
          {courseCompletionRate.toFixed(1)}% completed
        </div>
      </div>

      {/* Recent Achievements */}
      <div>
        <h4 className="font-medium text-gray-700 mb-3">🏆 Recent Achievements</h4>
        <div className="space-y-2">
          {progress.recentAchievements.map((achievement, index) => (
            <div key={index} className="flex items-center p-3 bg-gray-50 rounded-lg">
              <span className="text-green-500 mr-3">✓</span>
              <span className="text-gray-700">{achievement}</span>
            </div>
          ))}
        </div>
      </div>
    </div>
  );
}