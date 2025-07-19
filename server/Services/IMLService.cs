using SmartCareerPlatform.Models;

namespace SmartCareerPlatform.Services
{
    public interface IMLService
    {
        CareerPredictionResponse PredictCareer(CareerPredictionRequest request);
        List<CourseRecommendationResponse> RecommendCourses(CourseRecommendationRequest request);
        SkillGapAnalysisResponse AnalyzeSkillGap(SkillGapAnalysisRequest request);
    }
}