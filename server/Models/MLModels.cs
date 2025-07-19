namespace SmartCareerPlatform.Models;

public class CareerPredictionRequest
{
    public List<string> Skills { get; set; } = new();
    public List<string> Interests { get; set; } = new();
    public int Experience { get; set; }
}

public class CareerPredictionResponse
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<string> RequiredSkills { get; set; } = new();
    public decimal AverageSalary { get; set; }
    public decimal GrowthRate { get; set; }
    public decimal Confidence { get; set; }
}

public class CourseRecommendationRequest
{
    public int UserId { get; set; }
}

public class CourseRecommendationResponse
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<string> Skills { get; set; } = new();
    public int Duration { get; set; }
    public decimal Rating { get; set; }
    public string Url { get; set; } = string.Empty;
    public decimal RecommendationScore { get; set; }
}

public class SkillGapAnalysisRequest
{
    public int UserId { get; set; }
    public string TargetCareer { get; set; } = string.Empty;
}

public class SkillGapAnalysisResponse
{
    public List<string> MissingSkills { get; set; } = new();
    public List<string> Roadmap { get; set; } = new();
    public int EstimatedTime { get; set; }
}