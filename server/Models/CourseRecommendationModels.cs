namespace SmartCareerPlatform.Models
{
    public class CourseRecommendationRequest
    {
        public int UserId { get; set; }
        public List<string> TargetSkills { get; set; } = new();
        public string? CareerGoal { get; set; }
        public string? PreferredLevel { get; set; } 
        public int? MaxDuration { get; set; }
        public decimal? MinRating { get; set; }
        public bool IncludeExternalCourses { get; set; } = true;
        public int Limit { get; set; } = 20;
    }

    public class CourseRecommendationResponse
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Instructor { get; set; } = string.Empty;
        public List<string> Skills { get; set; } = new();
        public int Duration { get; set; }
        public decimal Rating { get; set; }
        public string Url { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Level { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Provider { get; set; } = string.Empty;
        public decimal RecommendationScore { get; set; }
        public string RecommendationReason { get; set; } = string.Empty;
        public List<string> MatchingSkills { get; set; } = new();
        public List<string> NewSkillsToLearn { get; set; } = new();
        public bool IsExternal { get; set; }
        public string? ExternalId { get; set; }
    }

    public class MLRecommendationRequest
    {
        public int UserId { get; set; }
        public List<string> UserSkills { get; set; } = new();
        public List<string> UserInterests { get; set; } = new();
        public int Experience { get; set; }
        public List<CourseData> AvailableCourses { get; set; } = new();
        public List<string>? TargetSkills { get; set; }
        public string? CareerGoal { get; set; }
    }

    public class MLRecommendationResponse
    {
        public List<CourseRecommendationItem> Recommendations { get; set; } = new();
        public decimal ModelConfidence { get; set; }
        public string RecommendationStrategy { get; set; } = string.Empty;
    }

    public class CourseRecommendationItem
    {
        public int CourseId { get; set; }
        public decimal Score { get; set; }
        public string Reason { get; set; } = string.Empty;
        public List<string> RelevantSkills { get; set; } = new();
    }

    public class CourseData
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<string> Skills { get; set; } = new();
        public string Category { get; set; } = string.Empty;
        public string Level { get; set; } = string.Empty;
        public decimal Rating { get; set; }
        public int Duration { get; set; }
        public string Provider { get; set; } = string.Empty;
    }

    public class UserLearningProfile
    {
        public int UserId { get; set; }
        public List<string> Skills { get; set; } = new();
        public List<string> Interests { get; set; } = new();
        public int Experience { get; set; }
        public List<string> CompletedCourses { get; set; } = new();
        public Dictionary<string, decimal> SkillProficiency { get; set; } = new();
        public string? PreferredLearningStyle { get; set; }
        public List<string> CareerGoals { get; set; } = new();
    }
}