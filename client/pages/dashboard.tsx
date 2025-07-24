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
      console.log('Dashboard: Starting to fetch user data...');
      try {
        const token = localStorage.getItem('token');
        console.log('Dashboard: Token found:', !!token);
        if (!token) {
          console.log('Dashboard: No token, redirecting to login');
          router.push('/login');
          return;
        }
        
        console.log('Dashboard: Calling getProfile API...');
        const response = await authAPI.getProfile();
        console.log('Dashboard: Profile response:', response.data);
        setUser(response.data);
      } catch (error) {
        console.log('Dashboard: Error fetching profile:', error);
        localStorage.removeItem('token');
        localStorage.removeItem('username');
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