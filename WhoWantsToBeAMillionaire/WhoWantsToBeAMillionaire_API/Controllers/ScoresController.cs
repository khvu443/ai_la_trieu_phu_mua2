using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WhoWantsToBeAMillionaire_API.DataAccess;
using WhoWantsToBeAMillionaire_API.Models;

namespace WhoWantsToBeAMillionaire_API.Controllers
{
    [EnableCors("AllowAllHeaders")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ScoresController : ControllerBase
    {
        readonly ScoreDAO scoreDAO = new ScoreDAO();

        //Get: api/Product
        [HttpGet, AllowAnonymous]
        public ActionResult<IEnumerable<Scores>> GetScores() => scoreDAO.GetScores();

        //Get: api/Product/5
        [HttpGet("{id:int}"), AllowAnonymous]
        public Scores GetScoreById(int id)
        {
            return scoreDAO.FindById(id);
        }

        [HttpGet("{id:int}"), AllowAnonymous]
        public Scores GetScoreByUserId(int id)
        {
            return scoreDAO.FindByUserId(id);
        }

        //Post: api/Product
        [HttpPost]
        public ActionResult PostScore(Scores p)
        {
            scoreDAO.AddScores(p);
            return Ok();
        }

        //Delete: api/Product/5
        [HttpDelete("{id:int}")]
        public ActionResult DeleteScore(int id)
        {
            if (scoreDAO.FindById(id) != null)
            {

                scoreDAO.DeleteScores(id);
                return Ok();
            }
            return NotFound();

        }

        //Put: api/Product/5
        [HttpPut("{id:int}")]
        public ActionResult UpdateScore(int id, Scores p)
        {

            if (scoreDAO.FindByUserId(id) != null)
            {
                scoreDAO.UpdateScores(id, p);
                return Ok();
            }
            return NotFound();

        }
    }
}
