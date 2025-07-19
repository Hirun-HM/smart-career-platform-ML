import { useState } from 'react';
import { useRouter } from 'next/router';
import { authAPI } from '../utils/api';

export default function Register() {
  const [userData, setUserData] = useState({
    username: '',
    email: '',
    password: '',
    confirmPassword: ''
  });
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState('');
  const [fieldErrors, setFieldErrors] = useState({
    username: '',
    email: '',
    password: '',
    confirmPassword: ''
  });
  const router = useRouter();

  const validateEmail = (email: string) => {
    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    return emailRegex.test(email);
  };

  const validatePassword = (password: string) => {
    return password.length >= 8 && 
           /[A-Z]/.test(password) && 
           /[a-z]/.test(password) && 
           /\d/.test(password);
  };

  const validateUsername = (username: string) => {
    return username.length >= 3 && /^[a-zA-Z0-9_]+$/.test(username);
  };

  const validateField = (field: string, value: string) => {
    let errorMessage = '';

    switch (field) {
      case 'username':
        if (!value) errorMessage = 'Username is required';
        else if (!validateUsername(value)) errorMessage = 'Username must be at least 3 characters and contain only letters, numbers, and underscores';
        break;
      case 'email':
        if (!value) errorMessage = 'Email is required';
        else if (!validateEmail(value)) errorMessage = 'Please enter a valid email address';
        break;
      case 'password':
        if (!value) errorMessage = 'Password is required';
        else if (!validatePassword(value)) errorMessage = 'Password must be at least 8 characters with uppercase, lowercase, and number';
        break;
      case 'confirmPassword':
        if (!value) errorMessage = 'Please confirm your password';
        else if (value !== userData.password) errorMessage = 'Passwords do not match';
        break;
    }

    setFieldErrors(prev => ({ ...prev, [field]: errorMessage }));
    return errorMessage === '';
  };

  const handleInputChange = (field: string, value: string) => {
    setUserData(prev => ({ ...prev, [field]: value }));
    validateField(field, value);
  };

  const isFormValid = () => {
    return Object.values(fieldErrors).every(error => error === '') &&
           Object.values(userData).every(value => value !== '');
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();

    // Validate all fields
    const isUsernameValid = validateField('username', userData.username);
    const isEmailValid = validateField('email', userData.email);
    const isPasswordValid = validateField('password', userData.password);
    const isConfirmPasswordValid = validateField('confirmPassword', userData.confirmPassword);

    if (!isUsernameValid || !isEmailValid || !isPasswordValid || !isConfirmPasswordValid) {
      setError('Please fix the errors above');
      return;
    }

    setLoading(true);
    setError('');
    console.log('Registering user:', userData);

    try {
      const response = await authAPI.register({
        username: userData.username,
        email: userData.email,
        password: userData.password
      });
      console.log('Registration response:', response);
      router.push('/login');
    } catch (err: any) {
      console.log('Registration error:', err);
      setError(err.response?.data?.message || 'Registration failed');
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="min-h-screen bg-gradient-to-br from-blue-900 to-purple-900 flex items-center justify-center">
      <div className="bg-white rounded-lg shadow-xl p-8 w-full max-w-md">
        <h2 className="text-2xl font-bold text-center mb-6">Create Account</h2>
        
        {error && (
          <div className="bg-red-100 border border-red-400 text-red-700 px-4 py-3 rounded mb-4">
            {error}
          </div>
        )}

        <form onSubmit={handleSubmit}>
          <div className="mb-4">
            <label className="block text-gray-700 text-sm font-bold mb-2">
              Username
            </label>
            <input
              type="text"
              value={userData.username}
              onChange={(e) => handleInputChange('username', e.target.value)}
              className={`w-full px-3 py-2 border rounded-md focus:outline-none focus:ring-2 ${
                fieldErrors.username 
                  ? 'border-red-400 focus:ring-red-500' 
                  : 'border-gray-300 focus:ring-blue-500'
              }`}
              required
            />
            {fieldErrors.username && (
              <p className="text-red-500 text-xs mt-1">{fieldErrors.username}</p>
            )}
          </div>

          <div className="mb-4">
            <label className="block text-gray-700 text-sm font-bold mb-2">
              Email
            </label>
            <input
              type="email"
              value={userData.email}
              onChange={(e) => handleInputChange('email', e.target.value)}
              className={`w-full px-3 py-2 border rounded-md focus:outline-none focus:ring-2 ${
                fieldErrors.email 
                  ? 'border-red-400 focus:ring-red-500' 
                  : 'border-gray-300 focus:ring-blue-500'
              }`}
              required
            />
            {fieldErrors.email && (
              <p className="text-red-500 text-xs mt-1">{fieldErrors.email}</p>
            )}
          </div>

          <div className="mb-4">
            <label className="block text-gray-700 text-sm font-bold mb-2">
              Password
            </label>
            <input
              type="password"
              value={userData.password}
              onChange={(e) => handleInputChange('password', e.target.value)}
              className={`w-full px-3 py-2 border rounded-md focus:outline-none focus:ring-2 ${
                fieldErrors.password 
                  ? 'border-red-400 focus:ring-red-500' 
                  : 'border-gray-300 focus:ring-blue-500'
              }`}
              required
            />
            {fieldErrors.password && (
              <p className="text-red-500 text-xs mt-1">{fieldErrors.password}</p>
            )}
          </div>

          <div className="mb-6">
            <label className="block text-gray-700 text-sm font-bold mb-2">
              Confirm Password
            </label>
            <input
              type="password"
              value={userData.confirmPassword}
              onChange={(e) => handleInputChange('confirmPassword', e.target.value)}
              className={`w-full px-3 py-2 border rounded-md focus:outline-none focus:ring-2 ${
                fieldErrors.confirmPassword 
                  ? 'border-red-400 focus:ring-red-500' 
                  : 'border-gray-300 focus:ring-blue-500'
              }`}
              required
            />
            {fieldErrors.confirmPassword && (
              <p className="text-red-500 text-xs mt-1">{fieldErrors.confirmPassword}</p>
            )}
          </div>

          <button
            type="submit"
            disabled={loading || !isFormValid()}
            className="w-full bg-blue-600 text-white py-2 px-4 rounded-md hover:bg-blue-700 focus:outline-none focus:ring-2 focus:ring-blue-500 disabled:opacity-50"
          >
            {loading ? 'Creating Account...' : 'Create Account'}
          </button>
        </form>

        <p className="text-center text-gray-600 mt-4">
          Already have an account? <a href="/login" className="text-blue-600 hover:underline">Login</a>
        </p>
      </div>
    </div>
  );
}