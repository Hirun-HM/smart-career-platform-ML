import { useState, useEffect } from 'react';
import { CareerPath, User, SkillGap, mlAPI } from '../utils/api';

interface CareerPathProps {
  careerPath: CareerPath;
  user: User;
}

export default function CareerPathComponent({ careerPath, user }: CareerPathProps) {
  const [skillGap, setSkillGap] = useState<SkillGap | null>(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const fetchSkillGap = async () => {
      try {
        const response = await mlAPI.analyzeSkillGap(user.id, careerPath.title);
        setSkillGap(response.data);
      } catch (error) {
        console.error('Error fetching skill gap:', error);
        
        setSkillGap({
          missingSkills: ['Machine Learning', 'Data Analysis', 'Python'],
          roadmap: [
            'Complete Python for Data Science course',
            'Learn Machine Learning fundamentals',
            'Practice with real datasets',
            'Build ML projects'
          ],
          estimatedTime: 12
        });
      } finally {
        setLoading(false);
      }
    };

    fetchSkillGap();
  }, [user.id, careerPath.title]);

  if (loading) {
    return (
      <div className="bg-white shadow rounded-lg p-6">
        <div className="text-center">Loading career path analysis...</div>
      </div>
    );
  }

  return (
    <div className="space-y-6">
      {/* Career Path Overview */}
      <div className="bg-white shadow rounded-lg p-6">
        <div className="flex items-center mb-4">
          <span className="text-3xl mr-3">🎯</span>
          <div>
            <h2 className="text-2xl font-bold text-gray-900">{careerPath.title}</h2>
            <p className="text-gray-600">{careerPath.description}</p>
          </div>
        </div>

        <div className="grid grid-cols-1 md:grid-cols-2 gap-6 mt-6">
          <div className="bg-green-50 p-4 rounded-lg">
            <h4 className="font-semibold text-green-800 mb-2">Average Salary</h4>
            <p className="text-2xl font-bold text-green-600">
              ${careerPath.averageSalary.toLocaleString()}
            </p>
          </div>
          
          <div className="bg-blue-50 p-4 rounded-lg">
            <h4 className="font-semibold text-blue-800 mb-2">Growth Rate</h4>
            <p className="text-2xl font-bold text-blue-600">
              {careerPath.growthRate}%
            </p>
          </div>
        </div>
      </div>

      {/* Required Skills */}
      <div className="bg-white shadow rounded-lg p-6">
        <h3 className="text-lg font-medium text-gray-900 mb-4">Required Skills</h3>
        
        <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
          <div>
            <h4 className="font-medium text-gray-700 mb-2">✅ You Have</h4>
            <div className="flex flex-wrap gap-2">
              {careerPath.requiredSkills
                .filter(skill => user.skills.includes(skill))
                .map((skill) => (
                  <span
                    key={skill}
                    className="inline-flex items-center px-3 py-1 rounded-full text-sm font-medium bg-green-100 text-green-800"
                  >
                    {skill}
                  </span>
                ))}
            </div>
          </div>
          
          <div>
            <h4 className="font-medium text-gray-700 mb-2">❌ Missing Skills</h4>
            <div className="flex flex-wrap gap-2">
              {skillGap?.missingSkills.map((skill) => (
                <span
                  key={skill}
                  className="inline-flex items-center px-3 py-1 rounded-full text-sm font-medium bg-red-100 text-red-800"
                >
                  {skill}
                </span>
              ))}
            </div>
          </div>
        </div>
      </div>

      {/* Learning Roadmap */}
      {skillGap && (
        <div className="bg-white shadow rounded-lg p-6">
          <h3 className="text-lg font-medium text-gray-900 mb-4">
            Learning Roadmap ({skillGap.estimatedTime} weeks)
          </h3>
          
          <div className="space-y-4">
            {skillGap.roadmap.map((step, index) => (
              <div key={index} className="flex items-start">
                <div className="flex-shrink-0 w-8 h-8 bg-blue-100 rounded-full flex items-center justify-center text-blue-600 font-semibold text-sm">
                  {index + 1}
                </div>
                <div className="ml-4 flex-1">
                  <p className="text-gray-900">{step}</p>
                </div>
              </div>
            ))}
          </div>
          
          <div className="mt-6 p-4 bg-blue-50 rounded-lg">
            <div className="flex items-center">
              <span className="text-blue-500 text-xl mr-2">💡</span>
              <div>
                <h4 className="font-medium text-blue-800">Pro Tip</h4>
                <p className="text-blue-700 text-sm">
                  Focus on building projects while learning. Practical experience is highly valued in this career path.
                </p>
              </div>
            </div>
          </div>
        </div>
      )}
    </div>
  );
}