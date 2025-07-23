using SmartCareerPlatform.Models;

namespace SmartCareerPlatform.Services
{
    public interface ICourseRecommendationService
    {
        Task<List<CourseRecommendationResponse>> GetPersonalizedRecommendationsAsync(int userId);
        Task<List<CourseRecommendationResponse>> GetRecommendationsWithPreferencesAsync(CourseRecommendationRequest request);
        Task<List<CourseRecommendationResponse>> GetSkillBasedRecommendationsAsync(int userId, List<string> targetSkills);
        Task<List<CourseRecommendationResponse>> GetCareerPathRecommendationsAsync(int userId, string careerGoal);
        Task<MLRecommendationResponse> GetMLRecommendationsAsync(MLRecommendationRequest request);
        Task<bool> RecordUserInteractionAsync(int userId, int courseId, string interactionType);
        Task<UserLearningProfile> GetUserLearningProfileAsync(int userId);
        Task TrainRecommendationModelAsync();
        Task<List<CourseData>> SyncCourseraCoursesAsync();
    }
}