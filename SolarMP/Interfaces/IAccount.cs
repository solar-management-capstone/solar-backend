 using SolarMP.DTOs.Account;
using SolarMP.DTOs.Account.Team;
using SolarMP.Models;

namespace SolarMP.Interfaces
{
    public interface IAccount
    {
        Task<List<Account>> getAll();
        Task<Account> register(AccountRegisterDTO dto);
        Task<Account> delete(string id);
        Task<Account> deleteHardCode(string id);
        Task<List<Account>> getByName(string name);
        Task<Account> getById(string id);
        Task<Account> changePass(ChangePassDTO dto);
        Task<Account> update(AccountUpdateDTO dto);
        Task<bool> addTeam(TeamDTO dto);
        Task<List<Account>> search(string? name, string? phone, string? email);
        Task<bool> deleteMember(string? leadId);
        Task<List<Team>> getMemberStaff(string leadId);
        Task<List<Account>> getAllMember();
        Task<List<Account>> staffLeadNotTeam();
        Task<List<Account>> staffNotTeam();
        Task<List<TeamRes>> getAllMemberv2();
    }
}
