using SmartCareerPlatform.Models;

namespace SmartCareerPlatform.Services
{
    public interface IMLService
    {
        Task<CareerPredictionResponse> PredictCareerAsync(CareerPredictionRequest request);
        Task<List<CourseRecommendationResponse>> RecommendCoursesAsync(CourseRecommendationRequest request);
        Task<SkillGapAnalysisResponse> AnalyzeSkillGapAsync(SkillGapAnalysisRequest request);
        Task<LearningPathResponse> GenerateLearningPathAsync(int userId, string targetCareer);
    }
}