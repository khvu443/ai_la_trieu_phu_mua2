using WhoWantsToBeAMillionaire_API.DatabaseContext;
using WhoWantsToBeAMillionaire_API.Models;

namespace WhoWantsToBeAMillionaire_API.DataAccess
{
    public class RoleDAO
    {
        public List<Roles> GetRoles()
        {
            var list = new List<Roles>();
            try
            {
                using (var ctx = new ApplicationContext())
                {
                    list = ctx.Roles.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return list;
        }

        public Roles GetRoleById(int id)
        {
            var p = new Roles();
            try
            {
                using (var ctx = new ApplicationContext())
                {
                    p = ctx.Roles.FirstOrDefault(role => id == role.role_id);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return p;
        }
    }
}
