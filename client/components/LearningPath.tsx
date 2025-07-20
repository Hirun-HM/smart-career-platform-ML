import { useState, useEffect } from 'react';
import { courseAPI, User } from '../utils/api';

interface Course {
  id: number;
  title: string;
  description: string;
  skills: string[];
  duration: number;
  rating: number;
  url: string;
  recommendationScore: number;
}

interface LearningPathProps {
  user: User;
}

export default function LearningPath({ user }: LearningPathProps) {
  const [courses, setCourses] = useState<Course[]>([]);
  const [loading, setLoading] = useState(true);
  const [enrolledCourses, setEnrolledCourses] = useState<number[]>([]);

  useEffect(() => {
    const fetchCourses = async () => {
      try {
        const response = await courseAPI.getRecommendations(user.id);
        setCourses(response.data);
      } catch (error) {
        console.error('Failed to fetch courses:', error);
      } finally {
        setLoading(false);
      }
    };

    fetchCourses();
  }, [user.id]);

  const handleEnrollCourse = async (courseId: number) => {
    try {
      await courseAPI.enrollCourse(courseId);
      setEnrolledCourses([...enrolledCourses, courseId]);
    } catch (error) {
      console.error('Failed to enroll in course:', error);
    }
  };

  if (loading) {
    return <div className="animate-pulse">Loading learning path...</div>;
  }

  return (
    <div className="bg-white shadow rounded-lg p-6">
      <div className="flex items-center mb-4">
        <span className="text-2xl mr-2">📚</span>
        <h3 className="text-xl font-semibold">Smart Learning Path</h3>
      </div>
      
      <p className="text-gray-600 mb-6">
        Discover courses and resources tailored to your career goals
      </p>

      <div className="space-y-4">
        {courses.map((course, index) => (
          <div key={course.id} className="border rounded-lg p-4 hover:shadow-md transition-shadow">
            <div className="flex items-start justify-between">
              <div className="flex-1">
                <div className="flex items-center mb-2">
                  <span className="bg-blue-100 text-blue-800 text-xs font-medium px-2 py-1 rounded mr-2">
                    Step {index + 1}
                  </span>
                  <h4 className="text-lg font-semibold">{course.title}</h4>
                </div>
                
                <p className="text-gray-600 mb-3">{course.description}</p>
                
                <div className="flex items-center gap-4 mb-3">
                  <span className="text-sm text-gray-500">
                    ⏱️ {course.duration} hours
                  </span>
                  <span className="text-sm text-gray-500">
                    ⭐ {course.rating}/5
                  </span>
                  <span className="text-sm text-gray-500">
                    🎯 {(course.recommendationScore * 100).toFixed(0)}% match
                  </span>
                </div>

                <div className="flex flex-wrap gap-2 mb-3">
                  {course.skills.map(skill => (
                    <span
                      key={skill}
                      className="bg-gray-100 text-gray-700 px-2 py-1 rounded text-sm"
                    >
                      {skill}
                    </span>
                  ))}
                </div>
              </div>

              <div className="ml-4">
                {enrolledCourses.includes(course.id) ? (
                  <span className="bg-green-100 text-green-800 px-4 py-2 rounded-md text-sm font-medium">
                    ✓ Enrolled
                  </span>
                ) : (
                  <button
                    onClick={() => handleEnrollCourse(course.id)}
                    className="bg-blue-600 text-white px-4 py-2 rounded-md hover:bg-blue-700 text-sm"
                  >
                    Enroll Now
                  </button>
                )}
              </div>
            </div>
          </div>
        ))}
      </div>
    </div>
  );
}