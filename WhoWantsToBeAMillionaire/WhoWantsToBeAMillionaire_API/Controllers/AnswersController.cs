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
    public class AnswersController : ControllerBase
    {
        readonly AnswerDAO answerDAO = new AnswerDAO();

        //Get: api/Product
        [HttpGet, AllowAnonymous]
        public ActionResult<IEnumerable<Answers>> GetAnswers() => answerDAO.GetAnswers();

        //Get: api/Product/5
        [HttpGet("{ques_id:int}"), AllowAnonymous]
        public IEnumerable<Answers> GetAnswerById(int ques_id)
        {
            return answerDAO.FindByQuesId(ques_id);
        }

        //Post: api/Product
        [HttpPost]
        public ActionResult PostAnswer(Answers p)
        {
            answerDAO.AddAnswers(p);
            return Ok();
        }

        //Delete: api/Product/5
        [HttpDelete]
        public ActionResult DeleteAnswer(int ques_id, string answer_id)
        {
            if (answerDAO.FindByQues_Ans_Id(ques_id, answer_id) != null)
            {

                answerDAO.DeleteAnswers(ques_id, answer_id);
                return Ok();
            }
            return NotFound();

        }

        //Put: api/Product/5
        [HttpPut]
        public ActionResult UpdateAnswer(int ques_id, string answer_id, Answers p)
        {

            if (answerDAO.FindByQues_Ans_Id(ques_id, answer_id) != null)
            {

                answerDAO.UpdateAnswers(ques_id, answer_id, p);
                return Ok();
            }
            return NotFound();

        }
    }
}
