import { useState } from 'react';
import Link from 'next/link';
import { useRouter } from 'next/router';

export default function Home() {
  const [features] = useState([
    {
      title: 'AI Career Prediction',
      description: 'Get personalized career recommendations based on your skills and interests',
      icon: '🎯'
    },
    {
      title: 'Smart Learning Path',
      description: 'Discover courses and resources tailored to your career goals',
      icon: '📚'
    },
    {
      title: 'Skill Gap Analysis',
      description: 'Identify missing skills and get a roadmap to bridge them',
      icon: '🔍'
    },
    {
      title: 'Progress Tracking',
      description: 'Monitor your learning progress with AI-powered insights',
      icon: '📊'
    }
  ]);

  const router = useRouter();

  const handleProfileLog = () => {
    router.push('/profile');
  };

  return (
    <div className="min-h-screen bg-gradient-to-br from-blue-900 to-purple-900">
      <div className="container mx-auto px-4 py-16">
        <div className="text-center mb-16">
          <h1 className="text-5xl font-bold text-white mb-6">
            Smart Career Platform
          </h1>
          <p className="text-xl text-blue-100 mb-8">
            AI-powered career guidance and learning recommendations
          </p>
          <div className="space-x-4">
            <Link href="/register" className="bg-white text-blue-900 px-8 py-3 rounded-lg font-semibold hover:bg-blue-50 transition-colors">
              Get Started
            </Link>
            <Link href="/login" className="border-2 border-white text-white px-8 py-3 rounded-lg font-semibold hover:bg-white hover:text-blue-900 transition-colors">
              Login
            </Link>
            <button
              onClick={handleProfileLog}
              className="bg-green-500 text-white px-8 py-3 rounded-lg font-semibold hover:bg-green-600 transition-colors ml-4"
            >
              Profile Log
            </button>
          </div>
        </div>

        <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-8">
          {features.map((feature, index) => (
            <div key={index} className="bg-white/10 backdrop-blur-sm rounded-lg p-6 text-center">
              <div className="text-4xl mb-4">{feature.icon}</div>
              <h3 className="text-xl font-semibold text-white mb-2">{feature.title}</h3>
              <p className="text-blue-100">{feature.description}</p>
            </div>
          ))}
        </div>
      </div>
    </div>
  );
}