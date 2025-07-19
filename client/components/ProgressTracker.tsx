import { useState, useEffect } from 'react';
import { User, userAPI, mlAPI } from '../utils/api';

interface ProgressTrackerProps {
  user: User;
}

interface ProgressData {
  weeklyProgress: number[];
  skillProgress: {[key: string]: number};
  predictions: {
    completionRate: number;
    nextMilestone: string;
    estimatedTime: number;
  };
}

export default function ProgressTracker({ user }: ProgressTrackerProps) {
  const [progress, setProgress] = useState<ProgressData | null>(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const fetchProgress = async () => {
      try {
        const [progressResponse, forecastResponse] = await Promise.all([
          userAPI.getProgress(),
          mlAPI.forecastProgress(user.id)
        ]);

        setProgress({
          weeklyProgress: progressResponse.data.weeklyProgress || [20, 35, 45, 60, 75, 80, 85],
          skillProgress: progressResponse.data.skillProgress || {},
          predictions: forecastResponse.data
        });
      } catch (error) {
        console.error('Error fetching progress:', error);
        // Set mock data for demo
        setProgress({
          weeklyProgress: [20, 35, 45, 60, 75, 80, 85],
          skillProgress: {
            'JavaScript': 85,
            'React': 70,
            'Node.js': 60,
            'Python': 45
          },
          predictions: {
            completionRate: 78,
            nextMilestone: 'Complete Advanced React Course',
            estimatedTime: 2
          }
        });
      } finally {
        setLoading(false);
      }
    };

    fetchProgress();
  }, [user.id]);

  if (loading) {
    return (
      <div className="bg-white shadow rounded-lg p-6">
        <div className="text-center">Loading progress data...</div>
      </div>
    );
  }

  if (!progress) {
    return (
      <div className="bg-white shadow rounded-lg p-6">
        <div className="text-center text-gray-500">No progress data available</div>
      </div>
    );
  }

  return (
    <div className="space-y-6">
      {/* Progress Overview */}
      <div className="bg-white shadow rounded-lg p-6">
        <h3 className="text-lg font-medium text-gray-900 mb-4">Learning Progress</h3>
        
        <div className="grid grid-cols-1 md:grid-cols-3 gap-4">
          <div className="text-center">
            <div className="text-2xl font-bold text-blue-600">
              {progress.predictions.completionRate}%
            </div>
            <div className="text-sm text-gray-500">Overall Progress</div>
          </div>
          
          <div className="text-center">
            <div className="text-2xl font-bold text-green-600">
              {progress.predictions.estimatedTime}
            </div>
            <div className="text-sm text-gray-500">Weeks to Next Milestone</div>
          </div>
          
          <div className="text-center">
            <div className="text-2xl font-bold text-purple-600">
              {user.skills.length}
            </div>
            <div className="text-sm text-gray-500">Skills Acquired</div>
          </div>
        </div>
      </div>

      {/* Weekly Progress Chart */}
      <div className="bg-white shadow rounded-lg p-6">
        <h3 className="text-lg font-medium text-gray-900 mb-4">Weekly Progress</h3>
        
        <div className="flex items-end space-x-2 h-40">
          {progress.weeklyProgress.map((value, index) => (
            <div key={index} className="flex-1 flex flex-col items-center">
              <div
                className="w-full bg-blue-500 rounded-t"
                style={{ height: `${(value / 100) * 120}px` }}
              />
              <div className="text-xs text-gray-500 mt-1">
                Week {index + 1}
              </div>
            </div>
          ))}
        </div>
      </div>

      {/* Skill Progress */}
      <div className="bg-white shadow rounded-lg p-6">
        <h3 className="text-lg font-medium text-gray-900 mb-4">Skill Proficiency</h3>
        
        <div className="space-y-4">
          {Object.entries(progress.skillProgress).map(([skill, level]) => (
            <div key={skill}>
              <div className="flex justify-between text-sm">
                <span className="font-medium text-gray-700">{skill}</span>
                <span className="text-gray-500">{level}%</span>
              </div>
              <div className="mt-1 relative">
                <div className="overflow-hidden h-2 text-xs flex rounded bg-gray-200">
                  <div
                    style={{ width: `${level}%` }}
                    className="shadow-none flex flex-col text-center whitespace-nowrap text-white justify-center bg-blue-500"
                  />
                </div>
              </div>
            </div>
          ))}
        </div>
      </div>

      {/* Next Milestone */}
      <div className="bg-white shadow rounded-lg p-6">
        <h3 className="text-lg font-medium text-gray-900 mb-4">Next Milestone</h3>
        
        <div className="flex items-center p-4 bg-yellow-50 rounded-lg">
          <span className="text-2xl mr-3">🎯</span>
          <div>
            <div className="font-medium text-gray-900">
              {progress.predictions.nextMilestone}
            </div>
            <div className="text-sm text-gray-500">
              Estimated completion: {progress.predictions.estimatedTime} weeks
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}