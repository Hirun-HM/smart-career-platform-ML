import axios from 'axios';

const API_BASE_URL = process.env.NEXT_PUBLIC_API_URL;
const ML_BASE_URL = process.env.NEXT_PUBLIC_ML_URL;

const apiClient = axios.create({
  baseURL: API_BASE_URL,
  headers: {
    'Content-Type': 'application/json',
  },
});

const mlClient = axios.create({
  baseURL: ML_BASE_URL,
  headers: {
    'Content-Type': 'application/json',
  },
});


apiClient.interceptors.request.use((config) => {
  const token = localStorage.getItem('token');
  if (token) {
    config.headers.Authorization = `Bearer ${token}`;
  }
  return config;
});

export interface Skill {
  id: number;
  name: string;
  category: string;
  description: string;
  relatedSkills: string[];
  createdAt: string;
}

export interface User {
  id: number;
  username: string;
  email: string;
  skills: Skill[]; // Update to use Skill objects instead of strings
  interests: string[];
  experience: number;
}

export interface Course {
  id: number;
  title: string;
  description: string;
  skills: string[];
  duration: number;
  rating: number;
  url: string;
}

export interface CareerPath {
  id: number;
  title: string;
  description: string;
  requiredSkills: string[];
  averageSalary: number;
  growthRate: number;
}

export interface SkillGap {
  missingSkills: string[];
  roadmap: string[];
  estimatedTime: number;
}

export const authAPI = {
  login: (credentials: { username: string; password: string }) =>
    apiClient.post('/auth/login', credentials),

  register: (userData: { username: string; email: string; password: string }) =>
    apiClient.post('/auth/register', userData),

  getProfile: () => apiClient.get('/auth/profile'),
};

export const userAPI = {
  updateProfile: (userData: Partial<User>) =>
    apiClient.put('/user/update', userData),
  
  getProgress: () => apiClient.get('/user/progress'),
  

};

export const courseAPI = {
  getAll: () => apiClient.get('/courses'),
  
  getRecommendations: (userId: number) =>
    mlClient.post('/recommend/courses', { userId }),
  
  enrollCourse: (courseId: number) =>
    apiClient.post(`/courses/${courseId}/enroll`),
};

export const mlAPI = {
  predictCareer: (userProfile: any) =>
    mlClient.post('/predict/career', userProfile),
  
  analyzeSkillGap: (userId: number, targetCareer: string) =>
    mlClient.post('/analyze/skill-gap', { userId, targetCareer }),
  
  forecastProgress: (userId: number) =>
    mlClient.post('/forecast/progress', { userId }),
};

export const skillAPI = {
  getAll: () => apiClient.get('/skills'),
  
  getUserSkills: (userId: number) =>
    apiClient.get(`/skills/user/${userId}`),
  
  addUserSkill: (userId: number, skillId: number) =>
    apiClient.post(`/skills/user/${userId}`, { skillId }),
  
  removeUserSkill: (userId: number, skillId: number) =>
    apiClient.delete(`/skills/user/${userId}/skill/${skillId}`),
  
  updateUserSkills: (userId: number, skillIds: number[]) =>
    apiClient.put(`/skills/user/${userId}`, { skillIds }),
  
  createSkill: (skill: Omit<Skill, 'id' | 'createdAt'>) =>
    apiClient.post('/skills', skill),
};