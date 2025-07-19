import { useState, useEffect } from 'react';
import { skillAPI, User, Skill } from '../utils/api';

interface SkillAssessmentProps {
  user: User;
}

export default function SkillAssessment({ user }: SkillAssessmentProps) {
  const [allSkills, setAllSkills] = useState<Skill[]>([]);
  const [selectedSkillIds, setSelectedSkillIds] = useState<number[]>(
    user.skills ? user.skills.map(skill => skill.id) : []
  );
  const [skillLevels, setSkillLevels] = useState<{[key: number]: number}>({});
  const [loading, setLoading] = useState(false);
  const [message, setMessage] = useState('');

  useEffect(() => {
    const fetchSkills = async () => {
      try {
        const response = await skillAPI.getAll();
        setAllSkills(response.data);
      } catch (error) {
        console.error('Failed to load skills:', error);
      }
    };
    fetchSkills();
  }, []);

  const handleSkillToggle = (skillId: number) => {
    if (selectedSkillIds.includes(skillId)) {
      setSelectedSkillIds(selectedSkillIds.filter(id => id !== skillId));
    } else {
      setSelectedSkillIds([...selectedSkillIds, skillId]);
    }
  };

  const handleLevelChange = (skillId: number, level: number) => {
    setSkillLevels({ ...skillLevels, [skillId]: level });
  };

  const handleSave = async () => {
    setLoading(true);
    setMessage('');

    try {
      await skillAPI.updateUserSkills(user.id, selectedSkillIds);
      setMessage('Skills updated successfully!');
    } catch (error) {
      setMessage('Failed to update skills. Please try again.');
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="bg-white shadow rounded-lg">
      <div className="px-6 py-4 border-b border-gray-200">
        <h3 className="text-lg leading-6 font-medium text-gray-900">
          Skill Assessment
        </h3>
        <p className="mt-1 text-sm text-gray-500">
          Select your skills and rate your proficiency level
        </p>
      </div>

      <div className="p-6">
        {message && (
          <div className={`mb-4 p-3 rounded-md ${
            message.includes('successfully') 
              ? 'bg-green-100 text-green-700' 
              : 'bg-red-100 text-red-700'
          }`}>
            {message}
          </div>
        )}

        <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-4">
          {allSkills.map((skill) => (
            <div key={skill.id} className="border border-gray-200 rounded-lg p-4">
              <div className="flex items-center justify-between mb-2">
                <label className="flex items-center">
                  <input
                    type="checkbox"
                    checked={selectedSkillIds.includes(skill.id)}
                    onChange={() => handleSkillToggle(skill.id)}
                    className="mr-2 h-4 w-4 text-blue-600 focus:ring-blue-500 border-gray-300 rounded"
                  />
                  <span className="text-sm font-medium text-gray-900">{skill.name}</span>
                </label>
              </div>

              {selectedSkillIds.includes(skill.id) && (
                <div className="mt-2">
                  <label className="block text-xs font-medium text-gray-700 mb-1">
                    Proficiency Level
                  </label>
                  <select
                    value={skillLevels[skill.id] || 1}
                    onChange={(e) => handleLevelChange(skill.id, parseInt(e.target.value))}
                    className="w-full text-sm border-gray-300 rounded-md focus:ring-blue-500 focus:border-blue-500"
                  >
                    <option value={1}>Beginner</option>
                    <option value={2}>Intermediate</option>
                    <option value={3}>Advanced</option>
                    <option value={4}>Expert</option>
                  </select>
                </div>
              )}
            </div>
          ))}
        </div>

        <div className="mt-6 flex justify-end">
          <button
            onClick={handleSave}
            disabled={loading}
            className="bg-blue-600 text-white px-6 py-2 rounded-md hover:bg-blue-700 focus:outline-none focus:ring-2 focus:ring-blue-500 disabled:opacity-50"
          >
            {loading ? 'Saving...' : 'Save Skills'}
          </button>
        </div>
      </div>
    </div>
  );
}