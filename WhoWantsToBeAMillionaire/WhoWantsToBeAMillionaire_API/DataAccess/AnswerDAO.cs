using WhoWantsToBeAMillionaire_API.DatabaseContext;
using WhoWantsToBeAMillionaire_API.Models;

namespace WhoWantsToBeAMillionaire_API.DataAccess
{
    public class AnswerDAO
    {
        public List<Answers> GetAnswers()
        {
            var list = new List<Answers>();
            try
            {
                using (var ctx = new ApplicationContext())
                {
                    list = ctx.Answers.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return list;
        }

        public List<Answers> FindByQuesId(int ques_id)
        {
            var p = new List<Answers>();
            try
            {
                using (var ctx = new ApplicationContext())
                {
                    p = ctx.Answers.Where<Answers>(Answers => (ques_id == Answers.question_id)).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return p;
        }

        public Answers FindByQues_Ans_Id(int ques_id, string answer_id)
        {
            var p = new Answers();
            try
            {
                using (var ctx = new ApplicationContext())
                {
                    p = ctx.Answers.FirstOrDefault<Answers>(Answers => (ques_id == Answers.question_id && answer_id == Answers.answer_id));
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return p;
        }

        public void AddAnswers(Answers p)
        {
            try
            {
                using (var ctx = new ApplicationContext())
                {
                    ctx.Answers.Add(p);
                    ctx.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void UpdateAnswers(int id, string answer_id, Answers p)
        {
            try
            {
                using (var ctx = new ApplicationContext())
                {
                    if (FindByQues_Ans_Id(id, answer_id) != null)
                    {
                        ctx.Answers.Attach(p);
                        ctx.Entry<Answers>(p).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        ctx.SaveChanges();
                    }

                    //p.AnswersId = id;
                    //ctx.Answers.Update(p);
                    //ctx.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeleteAnswers(int ques_id, string answer_id)
        {
            try
            {
                using (var ctx = new ApplicationContext())
                {
                    var answer = FindByQues_Ans_Id(ques_id, answer_id);
                    ctx.Answers.Remove(answer);

                    ctx.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
