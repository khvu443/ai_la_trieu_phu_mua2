using WhoWantsToBeAMillionaire_API.DatabaseContext;
using WhoWantsToBeAMillionaire_API.Models;

namespace WhoWantsToBeAMillionaire_API.DataAccess
{
    public class UserDAO
    {
        public Users GetUser(UserLogin login)
        {
            var user = new Users();
            try
            {
                using (var ctx = new ApplicationContext())
                {
                    user = ctx.Users.FirstOrDefault(o => o.username == login.username && o.password == login.password);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return user;
        }

        public void CreateUser(Users user)
        {
            try
            {
                using (var ctx = new ApplicationContext())
                {
                    ctx.Users.Add(user);
                    ctx.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Users GetUserByUid(int id)
        {
            var user = new Users();
            try
            {
                using (var ctx = new ApplicationContext())
                {
                    user = ctx.Users.FirstOrDefault(o => o.user_id == id);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return user;
        }

        public void UpdateUser(int id, Users user)
        {
            try
            {
                using (var ctx = new ApplicationContext())
                {
                    if (GetUserByUid(id) != null)
                    {
                        ctx.Users.Add(user);
                        ctx.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        ctx.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }

    public class UserLogin
    {
        public string username { get; set; }

        public string password { get; set; }
    }
}
