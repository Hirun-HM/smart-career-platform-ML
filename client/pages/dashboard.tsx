import { useState, useEffect } from 'react';
import { useRouter } from 'next/router';
import Dashboard from '../components/Dashboard';
import { authAPI } from '../utils/api';

export default function DashboardPage() {
  const [user, setUser] = useState(null);
  const [loading, setLoading] = useState(true);
  const router = useRouter();

  useEffect(() => {
    const fetchUser = async () => {
      try {
        const token = localStorage.getItem('token');
        if (!token) {
          router.push('/login');
          return;
        }

        
        const username = localStorage.getItem('username');
        if (!username) {
          localStorage.removeItem('token');
          router.push('/login');
          return;
        }
        const response = await authAPI.getProfile();
        setUser(response.data);
      } catch (error) {
        localStorage.removeItem('token');
        router.push('/login');
      } finally {
        setLoading(false);
      }
    };

    fetchUser();
  }, [router]);

  if (loading) {
    return (
      <div className="min-h-screen bg-gray-100 flex items-center justify-center">
        <div className="text-xl">Loading...</div>
      </div>
    );
  }

  return user ? <Dashboard user={user} /> : null;
}