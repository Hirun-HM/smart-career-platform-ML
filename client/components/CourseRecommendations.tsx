import { useState } from 'react';
import { Course } from '../utils/api';
import { courseAPI } from '../utils/api';

interface CourseRecommendationsProps {
  courses: Course[];
}

export default function CourseRecommendations({ courses }: CourseRecommendationsProps) {
  const [enrolledCourses, setEnrolledCourses] = useState<number[]>([]);
  const [loading, setLoading] = useState<number[]>([]);

  const handleEnroll = async (courseId: number) => {
    setLoading([...loading, courseId]);
    try {
      await courseAPI.enrollCourse(courseId);
      setEnrolledCourses([...enrolledCourses, courseId]);
    } catch (error) {
      console.error('Enrollment failed:', error);
    } finally {
      setLoading(loading.filter(id => id !== courseId));
    }
  };

  return (
    <div className="bg-white shadow rounded-lg">
      <div className="px-6 py-4 border-b border-gray-200">
        <h3 className="text-lg leading-6 font-medium text-gray-900">
          Recommended Courses
        </h3>
        <p className="mt-1 text-sm text-gray-500">
          AI-powered course recommendations based on your profile
        </p>
      </div>

      <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6 p-6">
        {courses.map((course) => (
          <div key={course.id} className="border border-gray-200 rounded-lg p-4">
            <div className="flex justify-between items-start mb-3">
              <h4 className="text-lg font-semibold text-gray-900">{course.title}</h4>
              <div className="flex items-center">
                <span className="text-yellow-400">★</span>
                <span className="ml-1 text-sm text-gray-600">{course.rating}</span>
              </div>
            </div>

            <p className="text-gray-700 mb-3">{course.description}</p>
            <div className="mb-2">
              <span className="text-xs font-semibold text-gray-500">Skills:</span>
              <div className="flex flex-wrap gap-1 mt-1">
                {course.skills.map((skill) => (
                  <span key={skill} className="bg-blue-100 text-blue-800 px-2 py-1 rounded text-xs">{skill}</span>
                ))}
              </div>
            </div>
            <div className="mb-2 text-sm text-gray-500">
              Duration: {course.duration} hrs
            </div>
            <div className="mb-2 text-sm text-gray-500">
              <a href={course.url} target="_blank" rel="noopener noreferrer" className="text-blue-600 hover:underline">
                View Course
              </a>
            </div>
            <button
              onClick={() => handleEnroll(course.id)}
              disabled={loading.includes(course.id) || enrolledCourses.includes(course.id)}
              className={`mt-3 w-full px-4 py-2 rounded ${
                enrolledCourses.includes(course.id)
                  ? 'bg-green-500 text-white'
                  : 'bg-blue-600 text-white hover:bg-blue-700'
              }`}
            >
              {enrolledCourses.includes(course.id)
                ? 'Enrolled'
                : loading.includes(course.id)
                ? 'Enrolling...'
                : 'Enroll'}
            </button>
          </div>
        ))}
      </div>
    </div>
  );
}