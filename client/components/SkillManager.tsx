import { useState, useEffect } from 'react';
import { skillAPI, Skill } from '../utils/api';

interface SkillManagerProps {
  userId: number;
  userSkills: Skill[];
  onSkillsUpdate: (skills: Skill[]) => void;
}

export default function SkillManager({ userId, userSkills, onSkillsUpdate }: SkillManagerProps) {
  const [allSkills, setAllSkills] = useState<Skill[]>([]);
  const [loading, setLoading] = useState(false);
  const [message, setMessage] = useState('');

  useEffect(() => {
    const fetchSkills = async () => {
      try {
        const response = await skillAPI.getAll();
        setAllSkills(response.data);
      } catch (error) {
        setMessage('Failed to load skills');
      }
    };
    fetchSkills();
  }, []);

  const handleSkillToggle = async (skill: Skill) => {
    setLoading(true);
    setMessage('');
    
    try {
      const hasSkill = userSkills.some(s => s.id === skill.id);
      
      if (hasSkill) {
        await skillAPI.removeUserSkill(userId, skill.id);
        onSkillsUpdate(userSkills.filter(s => s.id !== skill.id));
        setMessage('Skill removed successfully');
      } else {
        await skillAPI.addUserSkill(userId, skill.id);
        onSkillsUpdate([...userSkills, skill]);
        setMessage('Skill added successfully');
      }
    } catch (error) {
      setMessage('Failed to update skill');
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="mt-8">
      <h3 className="text-xl font-semibold mb-4">Skills</h3>
      
      {message && (
        <div className={`mb-4 p-3 rounded-md ${
          message.includes('successfully') ? 'bg-green-100 text-green-700' : 'bg-red-100 text-red-700'
        }`}>
          {message}
        </div>
      )}

      {/* Current Skills */}
      <div className="mb-6">
        <h4 className="text-lg font-medium mb-2">Your Skills</h4>
        <div className="flex flex-wrap gap-2">
          {userSkills.map(skill => (
            <span 
              key={skill.id}
              className="bg-blue-100 text-blue-800 px-3 py-1 rounded-full text-sm flex items-center gap-1"
            >
              {skill.name}
              <button
                onClick={() => handleSkillToggle(skill)}
                disabled={loading}
                className="text-blue-600 hover:text-blue-800 ml-1"
              >
                ×
              </button>
            </span>
          ))}
          {userSkills.length === 0 && (
            <span className="text-gray-500">No skills added yet</span>
          )}
        </div>
      </div>

      {/* Available Skills */}
      <div>
        <h4 className="text-lg font-medium mb-2">Available Skills</h4>
        <div className="flex flex-wrap gap-2">
          {allSkills
            .filter(skill => !userSkills.some(s => s.id === skill.id))
            .map(skill => (
              <button
                key={skill.id}
                onClick={() => handleSkillToggle(skill)}
                disabled={loading}
                className="bg-gray-100 text-gray-700 px-3 py-1 rounded-full text-sm hover:bg-gray-200 disabled:opacity-50"
              >
                + {skill.name}
              </button>
            ))}
        </div>
      </div>
    </div>
  );
}