using KolokwiumPoprawa.Models;
using KolokwiumPoprawa.Models.DTO;
using System.Threading.Tasks;

namespace KolokwiumPoprawa.Services
{
    public interface IDbService
    {
        Task<SomeSortOfTeam> GetTeam(int Id);
        Task<int> AddMemberToTeam(int MemberId, int TeamId);
    }
}
