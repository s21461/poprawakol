using KolokwiumPoprawa.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace KolokwiumPoprawa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private IDbService _dbService;

        public MemberController(IDbService dbService)
        {
            _dbService = dbService;
        }

        [HttpPost]
        [Route("{idTeam}/{idMember}")]
        public async Task<IActionResult> AddMemberToTeam([FromRoute] int idTeam, int idMember)
        {
            var result = await _dbService.AddMemberToTeam(idMember, idTeam);
            switch (result)
            {
                case 0:
                    return Ok("Member added");
                case 1:
                    return NotFound("Team not found");
                case 2:
                    return NotFound("Member not found");
                case 3:
                    return BadRequest("Member not in the same organization that team");
                case 4:
                    return BadRequest("Already exist");
                default:
                    return BadRequest(result);
            }
        } 

    }
}
