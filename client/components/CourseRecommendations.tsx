import { useState, useEffect } from 'react';
import { courseAPI } from '../utils/api';

interface CourseRecommendation {
  id: number;
  title: string;
  description: string;
  instructor: string;
  skills: string[];
  duration: number;
  rating: number;
  url: string;
  category: string;
  level: string;
  price: number;
  provider: string;
  recommendationScore: number;
  recommendationReason: string;
  matchingSkills: string[];
  newSkillsToLearn: string[];
  isExternal: boolean;
  externalId?: string;
}

interface CourseRecommendationsProps {
  readonly userId?: number;
  readonly userSkills?: readonly string[];
  readonly targetSkills?: readonly string[];
  readonly maxRecommendations?: number;
}

export default function CourseRecommendations({ 
  userId, 
  userSkills = [], 
  targetSkills = [],
  maxRecommendations = 12 
}: CourseRecommendationsProps) {
  const [recommendations, setRecommendations] = useState<CourseRecommendation[]>([]);
  const [loading, setLoading] = useState(true);
  const [enrolledCourses, setEnrolledCourses] = useState<number[]>([]);
  const [enrollingCourses, setEnrollingCourses] = useState<number[]>([]);
  const [interactionLoading, setInteractionLoading] = useState<number[]>([]);

  useEffect(() => {
    fetchRecommendations();
  }, [userId, userSkills, targetSkills]);

  const fetchRecommendations = async () => {
    if (!userId) return;
    
    setLoading(true);
    try {
      const response = await fetch(`/api/course-recommendations/user/${userId}`);
      if (response.ok) {
        const data = await response.json();
        setRecommendations(data.recommendations || []);
      }
    } catch (error) {
      console.error('Failed to fetch recommendations:', error);
    } finally {
      setLoading(false);
    }
  };

  const handleEnroll = async (courseId: number) => {
    setEnrollingCourses([...enrollingCourses, courseId]);
    try {
      // Record interaction
      await recordInteraction(courseId, 'enroll');
      
      // Enroll in course
      await courseAPI.enrollCourse(courseId);
      setEnrolledCourses([...enrolledCourses, courseId]);
    } catch (error) {
      console.error('Enrollment failed:', error);
    } finally {
      setEnrollingCourses(enrollingCourses.filter(id => id !== courseId));
    }
  };

  const recordInteraction = async (courseId: number, interactionType: string) => {
    if (!userId) return;
    
    try {
      await fetch('/api/course-recommendations/interaction', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({
          userId,
          courseId,
          interactionType,
        }),
      });
    } catch (error) {
      console.error('Failed to record interaction:', error);
    }
  };

  const handleCourseView = async (courseId: number) => {
    if (interactionLoading.includes(courseId)) return;
    
    setInteractionLoading([...interactionLoading, courseId]);
    await recordInteraction(courseId, 'view');
    setInteractionLoading(interactionLoading.filter(id => id !== courseId));
  };

  const getRecommendationScoreColor = (score: number) => {
    if (score >= 0.8) return 'text-green-600';
    if (score >= 0.6) return 'text-yellow-600';
    return 'text-gray-600';
  };

  if (loading) {
    return (
      <div className="bg-white shadow rounded-lg p-6">
        <div className="animate-pulse">
          <div className="h-6 bg-gray-200 rounded w-1/3 mb-4"></div>
          <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
            {Array.from({length: 6}, (_, i) => (
              <div key={`skeleton-placeholder-${i}`} className="border border-gray-200 rounded-lg p-4">
                <div className="h-4 bg-gray-200 rounded w-3/4 mb-2"></div>
                <div className="h-3 bg-gray-200 rounded w-full mb-2"></div>
                <div className="h-3 bg-gray-200 rounded w-5/6"></div>
              </div>
            ))}
          </div>
        </div>
      </div>
    );
  }

  return (
    <div className="bg-white shadow rounded-lg">
      <div className="px-6 py-4 border-b border-gray-200">
        <div className="flex justify-between items-center">
          <div>
            <h3 className="text-lg leading-6 font-medium text-gray-900">
              AI-Powered Course Recommendations
            </h3>
            <p className="mt-1 text-sm text-gray-500">
              Personalized course suggestions based on your skills and career goals
            </p>
          </div>
          <button
            onClick={fetchRecommendations}
            className="inline-flex items-center px-3 py-1 border border-gray-300 rounded-md text-xs font-medium text-gray-700 bg-white hover:bg-gray-50"
          >
            🔄 Refresh
          </button>
        </div>
      </div>

      {recommendations.length === 0 ? (
        <div className="p-6 text-center">
          <div className="text-gray-500 mb-4">
            <svg className="mx-auto h-12 w-12" fill="none" viewBox="0 0 24 24" stroke="currentColor">
              <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M12 6.253v13m0-13C10.832 5.477 9.246 5 7.5 5S4.168 5.477 3 6.253v13C4.168 18.477 5.754 18 7.5 18s3.332.477 4.5 1.253m0-13C13.168 5.477 14.754 5 16.5 5c1.746 0 3.332.477 4.5 1.253v13C19.832 18.477 18.246 18 16.5 18c-1.746 0-3.332.477-4.5 1.253" />
            </svg>
          </div>
          <h3 className="text-lg font-medium text-gray-900">No recommendations yet</h3>
          <p className="text-gray-500">Complete your profile to get personalized course recommendations.</p>
        </div>
      ) : (
        <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6 p-6">
          {recommendations.slice(0, maxRecommendations).map((course) => (
            <button 
              key={course.id} 
              type="button"
              className="border border-gray-200 rounded-lg p-4 hover:shadow-md transition-shadow text-left w-full"
              onClick={() => handleCourseView(course.id)}
              onKeyDown={(e) => {
                if (e.key === 'Enter' || e.key === ' ') {
                  e.preventDefault();
                  handleCourseView(course.id);
                }
              }}
            >
              {/* Course Header */}
              <div className="flex justify-between items-start mb-3">
                <div className="flex-1">
                  <h4 className="text-lg font-semibold text-gray-900 line-clamp-2">{course.title}</h4>
                  <p className="text-sm text-gray-600">{course.provider}</p>
                </div>
                <div className="flex flex-col items-end ml-2">
                  <div className="flex items-center">
                    <span className="text-yellow-400">★</span>
                    <span className="ml-1 text-sm text-gray-600">{course.rating.toFixed(1)}</span>
                  </div>
                  <span className={`text-xs font-semibold ${getRecommendationScoreColor(course.recommendationScore)}`}>
                    {Math.round(course.recommendationScore * 100)}% match
                  </span>
                </div>
              </div>

              {/* Course Description */}
              <p className="text-gray-700 text-sm mb-3 line-clamp-3">{course.description}</p>

              {/* Course Details */}
              <div className="space-y-2 mb-4">
                <div className="flex justify-between text-xs text-gray-500">
                  <span>Duration: {course.duration} hrs</span>
                  <span>Level: {course.level}</span>
                </div>
                
                {/* Skills */}
                <div>
                  <span className="text-xs font-semibold text-gray-500">Skills:</span>
                  <div className="flex flex-wrap gap-1 mt-1">
                    {course.skills.slice(0, 3).map((skill) => (
                      <span 
                        key={skill} 
                        className={`px-2 py-1 rounded text-xs ${
                          course.matchingSkills.includes(skill) 
                            ? 'bg-green-100 text-green-800' 
                            : 'bg-blue-100 text-blue-800'
                        }`}
                      >
                        {skill}
                      </span>
                    ))}
                    {course.skills.length > 3 && (
                      <span className="text-xs text-gray-500">+{course.skills.length - 3} more</span>
                    )}
                  </div>
                </div>

                {/* Recommendation Reason */}
                <div className="text-xs text-gray-600 bg-gray-50 p-2 rounded">
                  💡 {course.recommendationReason}
                </div>
              </div>

              {/* Actions */}
              <div className="flex gap-2">
                {course.url && (
                  <a 
                    href={course.url} 
                    target="_blank" 
                    rel="noopener noreferrer" 
                    className="flex-1 text-center px-3 py-2 border border-gray-300 rounded text-sm text-gray-700 hover:bg-gray-50"
                    onClick={(e) => e.stopPropagation()}
                  >
                    View Course
                  </a>
                )}
                <button
                  onClick={(e) => {
                    e.stopPropagation();
                    handleEnroll(course.id);
                  }}
                  disabled={enrollingCourses.includes(course.id) || enrolledCourses.includes(course.id)}
                  className={`flex-1 px-3 py-2 rounded text-sm font-medium ${
                    enrolledCourses.includes(course.id)
                      ? 'bg-green-500 text-white cursor-not-allowed'
                      : 'bg-blue-600 text-white hover:bg-blue-700'
                  }`}
                >
                  {(() => {
                    if (enrolledCourses.includes(course.id)) return '✓ Enrolled';
                    if (enrollingCourses.includes(course.id)) return 'Enrolling...';
                    return 'Enroll';
                  })()}
                </button>
              </div>
            </button>
          ))}
        </div>
      )}

      {/* Load More Button */}
      {recommendations.length > maxRecommendations && (
        <div className="px-6 py-4 border-t border-gray-200 text-center">
          <button
            onClick={() => setRecommendations(recommendations.slice(0, maxRecommendations + 6))}
            className="inline-flex items-center px-4 py-2 border border-gray-300 rounded-md text-sm font-medium text-gray-700 bg-white hover:bg-gray-50"
          >
            Load More Recommendations
          </button>
        </div>
      )}
    </div>
  );
}