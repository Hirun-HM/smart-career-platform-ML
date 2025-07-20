import { useState } from 'react';
import { mlAPI, User } from '../utils/api';

interface SkillGap {
  missingSkills: string[];
  roadmap: string[];
  estimatedTime: number;
}

interface SkillGapAnalysisProps {
  user: User;
}

export default function SkillGapAnalysis({ user }: SkillGapAnalysisProps) {
  const [targetCareer, setTargetCareer] = useState('');
  const [skillGap, setSkillGap] = useState<SkillGap | null>(null);
  const [loading, setLoading] = useState(false);

  const handleAnalyze = async () => {
    if (!targetCareer.trim()) return;
    
    setLoading(true);
    try {
      const response = await mlAPI.analyzeSkillGap(user.id, targetCareer);
      setSkillGap(response.data);
    } catch (error) {
      console.error('Failed to analyze skill gap:', error);
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="bg-white shadow rounded-lg p-6">
      <div className="flex items-center mb-4">
        <span className="text-2xl mr-2">🔍</span>
        <h3 className="text-xl font-semibold">Skill Gap Analysis</h3>
      </div>
      
      <p className="text-gray-600 mb-4">
        Identify missing skills and get a roadmap to bridge them
      </p>

      <div className="flex gap-2 mb-6">
        <input
          type="text"
          value={targetCareer}
          onChange={(e) => setTargetCareer(e.target.value)}
          placeholder="Enter target career (e.g., Data Scientist)"
          className="flex-1 px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500"
        />
        <button
          onClick={handleAnalyze}
          disabled={loading || !targetCareer.trim()}
          className="bg-blue-600 text-white px-6 py-2 rounded-md hover:bg-blue-700 disabled:opacity-50"
        >
          {loading ? 'Analyzing...' : 'Analyze'}
        </button>
      </div>

      {skillGap && (
        <div className="space-y-6">
          {/* Current Skills */}
          <div>
            <h4 className="font-medium text-gray-700 mb-2">✅ Your Current Skills</h4>
            <div className="flex flex-wrap gap-2">
              {user.skills.map(skill => (
                <span
                  key={skill.id}
                  className="bg-green-100 text-green-800 px-3 py-1 rounded-full text-sm"
                >
                  {skill.name}
                </span>
              ))}
            </div>
          </div>

          {/* Missing Skills */}
          <div>
            <h4 className="font-medium text-gray-700 mb-2">❌ Missing Skills</h4>
            <div className="flex flex-wrap gap-2">
              {skillGap.missingSkills.map(skill => (
                <span
                  key={skill}
                  className="bg-red-100 text-red-800 px-3 py-1 rounded-full text-sm"
                >
                  {skill}
                </span>
              ))}
            </div>
          </div>

          {/* Learning Roadmap */}
          <div>
            <h4 className="font-medium text-gray-700 mb-2">🗺️ Learning Roadmap</h4>
            <div className="bg-gray-50 rounded-lg p-4">
              <div className="flex items-center mb-3">
                <span className="text-sm text-gray-600">
                  Estimated completion time: {skillGap.estimatedTime} weeks
                </span>
              </div>
              <ol className="space-y-2">
                {skillGap.roadmap.map((step, index) => (
                  <li key={index} className="flex items-start">
                    <span className="bg-blue-500 text-white text-xs rounded-full w-5 h-5 flex items-center justify-center mr-3 mt-0.5 flex-shrink-0">
                      {index + 1}
                    </span>
                    <span className="text-gray-700">{step}</span>
                  </li>
                ))}
              </ol>
            </div>
          </div>
        </div>
      )}
    </div>
  );
}