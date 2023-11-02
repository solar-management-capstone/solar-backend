using SolarMP.DTOs.Acceptances;
using SolarMP.DTOs.Survey;
using SolarMP.Models;

namespace SolarMP.Interfaces
{
    public interface ISurvey
    {
        Task<List<Survey>> GetSurveyById(string? surveyId);
        Task<List<Survey>> GetSurveyByIdstaff(string surveyId);
        Task<List<Survey>> GetAllSurveys();
        Task<bool> UpdateSurvey(SurveyUpdateDTO upSurvey);
        Task<bool> DeleteSurvey(string surveyId);
        Task<bool> InsertSurvey(SurveyDTO survey);
    }
}
