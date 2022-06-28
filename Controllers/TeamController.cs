using KolokwiumPoprawa.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace KolokwiumPoprawa.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private IDbService _dbService;

        public TeamController(IDbService dbService)
        {
            _dbService = dbService;
        }


        [HttpGet]
        [Route("{idTeam}")]
        public async Task<IActionResult> GetTeam([FromRoute] int idTeam)
        {
            var result = await _dbService.GetTeam(idTeam);
            if(result == null)
                return NotFound("Team not exist");
            return Ok(result);
        }

    }
}
