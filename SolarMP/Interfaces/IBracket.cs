using SolarMP.DTOs.Bracket;
using SolarMP.DTOs.Survey;
using SolarMP.Models;

namespace SolarMP.Interfaces
{
    public interface IBracket
    {
        Task<List<Bracket>> GetBracketById(string? bracketId);
        Task<List<Bracket>> GetAllBrackets();
        Task<bool> UpdateBracket(BracketUpdateDTO upBracket);
        Task<bool> DeleteBracket(string bracketId);
        Task<Bracket> InsertBracket(BracketDTO bracket);
        Task<List<Bracket>> GetAllBracketsAdmin();
    }
}
