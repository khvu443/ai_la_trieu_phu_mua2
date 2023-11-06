using WhoWantsToBeAMillionaire_API.DatabaseContext;
using WhoWantsToBeAMillionaire_API.Models;

namespace WhoWantsToBeAMillionaire_API.DataAccess
{
    public class ScoreDAO
    {
        public List<Scores> GetScores()
        {
            var list = new List<Scores>();
            try
            {
                using (var ctx = new ApplicationContext())
                {
                    list = ctx.Scores.OrderBy(s => s.score).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return list;
        }

        public Scores FindById(int id)
        {
            var p = new Scores();
            try
            {
                using (var ctx = new ApplicationContext())
                {
                    p = ctx.Scores.FirstOrDefault<Scores>(Scores => id == Scores.score_id);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return p;
        }


        public Scores FindByUserId(int user_id)
        {
            var p = new Scores();
            try
            {
                using (var ctx = new ApplicationContext())
                {
                    p = ctx.Scores.FirstOrDefault<Scores>(Scores => user_id == Scores.user_id);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return p;
        }

        public void AddScores(Scores p)
        {
            try
            {
                using (var ctx = new ApplicationContext())
                {
                    ctx.Scores.Add(p);
                    ctx.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void UpdateScores(int id, Scores p)
        {
            try
            {
                using (var ctx = new ApplicationContext())
                {
                    if (FindByUserId(id) != null)
                    {
                        ctx.Scores.Attach(p);
                        ctx.Entry<Scores>(p).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        ctx.SaveChanges();
                    }

                    //p.ScoresId = id;
                    //ctx.Scores.Update(p);
                    //ctx.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeleteScores(int id)
        {
            try
            {
                using (var ctx = new ApplicationContext())
                {
                    var p = ctx.Scores.FirstOrDefault<Scores>(x => id == x.score_id);
                    ctx.Scores.Remove(p);
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
