using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WhoWantsToBeAMillionaire_API.DataAccess;
using WhoWantsToBeAMillionaire_API.Models;

namespace WhoWantsToBeAMillionaire_API.Controllers
{
    [EnableCors("AllowAllHeaders")]
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        readonly QuestionDAO questionDAO = new QuestionDAO();

        //Get: api/Product
        [HttpGet, AllowAnonymous]
        public ActionResult<IEnumerable<Questions>> GetQuestions() => questionDAO.GetQuestions();

        //Get: api/Product/5
        [HttpGet("{id:int}"), AllowAnonymous]
        public Questions GetQuestionById(int id)
        {
            return questionDAO.FindById(id);
        }

        //Post: api/Product
        [HttpPost, Authorize(Roles = "Adminstrator")]
        public ActionResult PostQuestion(Questions p)
        {
            questionDAO.AddQuestions(p);
            return Ok();
        }

        //Delete: api/Product/5
        [HttpDelete("{id:int}"), Authorize(Roles = "Adminstrator")]
        public ActionResult DeleteQuestion(int id)
        {
            if (questionDAO.FindById(id) != null)
            {

                questionDAO.DeleteQuestions(id);
                return Ok();
            }
            return NotFound();

        }

        //Put: api/Product/5
        [HttpPut("{id:int}"), Authorize(Roles = "Adminstrator")]
        public ActionResult UpdateQuestion(int id, Questions p)
        {

            if (questionDAO.FindById(id) != null)
            {
                questionDAO.UpdateQuestions(id, p);
                return Ok();
            }
            return NotFound();

        }
    }
}
