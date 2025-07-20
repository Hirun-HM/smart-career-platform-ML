import { useState, useEffect } from 'react';
import { mlAPI, User } from '../utils/api';

interface CareerPredictionProps {
  user: User;
}

interface CareerPrediction {
  id: number;
  title: string;
  description: string;
  requiredSkills: string[];
  averageSalary: number;
  growthRate: number;
  confidence: number;
}

export default function CareerPrediction({ user }: CareerPredictionProps) {
  const [prediction, setPrediction] = useState<CareerPrediction | null>(null);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState('');

  const handlePredictCareer = async () => {
    setLoading(true);
    setError('');
    
    try {
      const response = await mlAPI.predictCareer({
        skills: user.skills.map(s => s.name),
        interests: user.interests,
        experience: user.experience
      });
      setPrediction(response.data);
    } catch (err) {
      setError('Failed to predict career. Please try again.');
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="bg-white shadow rounded-lg p-6">
      <div className="flex items-center mb-4">
        <span className="text-2xl mr-2">🎯</span>
        <h3 className="text-xl font-semibold">AI Career Prediction</h3>
      </div>
      
      <p className="text-gray-600 mb-4">
        Get personalized career recommendations based on your skills and interests
      </p>

      <button
        onClick={handlePredictCareer}
        disabled={loading}
        className="bg-blue-600 text-white px-6 py-2 rounded-md hover:bg-blue-700 disabled:opacity-50 mb-4"
      >
        {loading ? 'Analyzing...' : 'Predict My Career'}
      </button>

      {error && (
        <div className="bg-red-100 text-red-700 p-3 rounded-md mb-4">
          {error}
        </div>
      )}

      {prediction && (
        <div className="border rounded-lg p-4">
          <h4 className="text-lg font-semibold text-blue-600 mb-2">
            {prediction.title}
          </h4>
          <p className="text-gray-700 mb-4">{prediction.description}</p>
          
          <div className="grid grid-cols-1 md:grid-cols-3 gap-4 mb-4">
            <div className="text-center">
              <div className="text-2xl font-bold text-green-600">
                ${prediction.averageSalary.toLocaleString()}
              </div>
              <div className="text-sm text-gray-500">Average Salary</div>
            </div>
            <div className="text-center">
              <div className="text-2xl font-bold text-blue-600">
                {(prediction.growthRate * 100).toFixed(1)}%
              </div>
              <div className="text-sm text-gray-500">Growth Rate</div>
            </div>
            <div className="text-center">
              <div className="text-2xl font-bold text-purple-600">
                {(prediction.confidence * 100).toFixed(1)}%
              </div>
              <div className="text-sm text-gray-500">Confidence</div>
            </div>
          </div>

          <div>
            <h5 className="font-medium mb-2">Required Skills:</h5>
            <div className="flex flex-wrap gap-2">
              {prediction.requiredSkills.map(skill => (
                <span
                  key={skill}
                  className={`px-3 py-1 rounded-full text-sm ${
                    user.skills.some(s => s.name === skill)
                      ? 'bg-green-100 text-green-800'
                      : 'bg-red-100 text-red-800'
                  }`}
                >
                  {skill}
                </span>
              ))}
            </div>
          </div>
        </div>
      )}
    </div>
  );
}