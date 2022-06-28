using KolokwiumPoprawa.Models;
using KolokwiumPoprawa.Models.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace KolokwiumPoprawa.Services
{
    public class DbService : IDbService
    {
        private readonly MainDbContext _dbContext;

        public DbService(MainDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<int> AddMemberToTeam(int MemberId, int TeamId)
        {
            var result = await _dbContext.Teams 
                .Where(e => e.TeamID == TeamId).FirstOrDefaultAsync();
            if (result == null)
            {
                return 1;
            }

            var result2 = await _dbContext.Members.Where(e => e.MemberID == MemberId).FirstOrDefaultAsync();
            if (result2 == null)
            {
                return 2;
            }

            if(result.OrganizationID != result2.OrganizationID)
            {
                return 3;
            }

            var result4 = await _dbContext.Memberships.Where(e => e.Team.TeamID == TeamId && e.Member.MemberID == MemberId).FirstOrDefaultAsync();
            if (result4 != null)
            {
                return 4;
            }
 
            



        var now = new DateTime();
            var newMembership = new Membership
            {
                MemberID = MemberId,
                TeamID = TeamId,
                MembershipDate = now

            };
            await _dbContext.Memberships.AddAsync(newMembership);
            await _dbContext.SaveChangesAsync();
            return 0;

        }

        public async Task<SomeSortOfTeam> GetTeam(int Id)
        {

            return await _dbContext.Teams
                .Include(e => e.Organization)
                .Where(e => e.TeamID == Id)
                .Select(e => new SomeSortOfTeam
                {
                    TeamName = e.TeamName,
                    TeamDescription = e.TeamDescription,
                    OrganizationName = e.Organization.OrganizationName,
                    Members = e.Memberships.Select(e => new SomeSortOfMember
                    {
                        MemberName = e.Member.MemberName,
                        MemberSurname = e.Member.MemberSurname,
                        MembershipDate = e.MembershipDate
                    }).OrderBy(e => e.MembershipDate).ToList()
                }).FirstOrDefaultAsync();
        }


    }
}
