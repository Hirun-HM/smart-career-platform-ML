import { useState, useEffect } from 'react';
import { useRouter } from 'next/router';
import { authAPI, userAPI, User, Skill } from '../utils/api';
import SkillAssessment from '../components/SkillAssessment';
import SkillManager from '../components/SkillManager';

export default function ProfilePage() {
  const [user, setUser] = useState<User | null>(null);
  const [loading, setLoading] = useState(true);
  const [editMode, setEditMode] = useState(false);
  const [form, setForm] = useState<Partial<User>>({});
  const [message, setMessage] = useState('');
  const router = useRouter();

  useEffect(() => {
    const fetchProfile = async () => {
      try {
        const token = localStorage.getItem('token');
        console.log('Token in profile page:', token);
        if (!token) {
          router.push('/login');
          return;
        }
        const response = await authAPI.getProfile();
        setUser(response.data);
        setForm(response.data);
      } catch (err) {
        localStorage.removeItem('token');
        router.push('/login');
      } finally {
        setLoading(false);
      }
    };
    fetchProfile();
  }, [router]);

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setForm({ ...form, [e.target.name]: e.target.value });
  };

  const handleSave = async () => {
    setLoading(true);
    setMessage('');
    try {
      await userAPI.updateProfile(form);
      setMessage('Profile updated successfully!');
      setEditMode(false);
      setUser({ ...user!, ...form });
    } catch {
      setMessage('Failed to update profile.');
    } finally {
      setLoading(false);
    }
  };

  const handleSkillsUpdate = (skills: Skill[]) => {
    setUser(prev => prev ? { ...prev, skills } : null);
  };

  if (loading) {
    return (
      <div className="min-h-screen flex items-center justify-center bg-gray-100">
        <div className="text-xl">Loading profile...</div>
      </div>
    );
  }

  if (!user) return null;

  return (
    <div className="min-h-screen bg-gray-100 py-10">
      <div className="max-w-2xl mx-auto bg-white shadow rounded-lg p-8">
        <h2 className="text-2xl font-bold mb-6">Profile</h2>
        {message && (
          <div className={`mb-4 p-3 rounded-md ${
            message.includes('successfully') ? 'bg-green-100 text-green-700' : 'bg-red-100 text-red-700'
          }`}>
            {message}
          </div>
        )}
        
        {/* Basic Profile Information */}
        <div className="space-y-4">
          <div>
            <label className="block text-gray-700 font-medium mb-1">Username</label>
            {editMode ? (
              <input
                name="username"
                value={form.username || ''}
                onChange={handleChange}
                className="w-full px-3 py-2 border rounded"
              />
            ) : (
              <div className="text-gray-900">{user.username}</div>
            )}
          </div>
          <div>
            <label className="block text-gray-700 font-medium mb-1">Email</label>
            {editMode ? (
              <input
                name="email"
                value={form.email || ''}
                onChange={handleChange}
                className="w-full px-3 py-2 border rounded"
              />
            ) : (
              <div className="text-gray-900">{user.email}</div>
            )}
          </div>
          <div>
            <label className="block text-gray-700 font-medium mb-1">Experience (years)</label>
            {editMode ? (
              <input
                name="experience"
                type="number"
                value={form.experience ?? 0}
                onChange={handleChange}
                className="w-full px-3 py-2 border rounded"
              />
            ) : (
              <div className="text-gray-900">{user.experience}</div>
            )}
          </div>
        </div>
        
        <div className="mt-6 flex space-x-4">
          {editMode ? (
            <>
              <button
                onClick={handleSave}
                className="bg-blue-600 text-white px-4 py-2 rounded hover:bg-blue-700"
                disabled={loading}
              >
                {loading ? 'Saving...' : 'Save'}
              </button>
              <button
                onClick={() => { setEditMode(false); setForm(user); }}
                className="bg-gray-200 text-gray-700 px-4 py-2 rounded hover:bg-gray-300"
              >
                Cancel
              </button>
            </>
          ) : (
            <button
              onClick={() => setEditMode(true)}
              className="bg-blue-600 text-white px-4 py-2 rounded hover:bg-blue-700"
            >
              Edit Profile
            </button>
          )}
        </div>

        {/* Skills Management */}
        <SkillManager 
          userId={user.id} 
          userSkills={user.skills || []} 
          onSkillsUpdate={handleSkillsUpdate}
        />
        
        <div className="mt-10">
          <SkillAssessment user={user} />
        </div>
      </div>
    </div>
  );
}