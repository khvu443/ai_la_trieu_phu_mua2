
using WhoWantsToBeAMillionaire_API.DatabaseContext;
using WhoWantsToBeAMillionaire_API.Models;

namespace WhoWantsToBeAMillionaire_API.DataAccess
{
    public class QuestionDAO
    {
        public List<Questions> GetQuestions()
        {
            var list = new List<Questions>();
            try
            {
                using (var ctx = new ApplicationContext())
                {
                    list = ctx.Questions.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return list;
        }

        public Questions FindById(int id)
        {
            var p = new Questions();
            try
            {
                using (var ctx = new ApplicationContext())
                {
                    p = ctx.Questions.FirstOrDefault<Questions>(Questions => id == Questions.question_id);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return p;
        }

        public void AddQuestions(Questions p)
        {
            try
            {
                using (var ctx = new ApplicationContext())
                {
                    ctx.Questions.Add(p);
                    ctx.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void UpdateQuestions(int id, Questions p)
        {
            try
            {
                using (var ctx = new ApplicationContext())
                {
                    if (FindById(id) != null)
                    {
                        ctx.Questions.Attach(p);
                        ctx.Entry<Questions>(p).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        ctx.SaveChanges();
                    }

                    //p.QuestionsId = id;
                    //ctx.Questions.Update(p);
                    //ctx.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeleteQuestions(int id)
        {
            try
            {
                using (var ctx = new ApplicationContext())
                {
                    var p = ctx.Questions.FirstOrDefault<Questions>(x => id == x.question_id);
                    ctx.Questions.Remove(p);
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
