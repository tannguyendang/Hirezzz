using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Hirezzz.Models;
public class RoleRepository : BaseRepository
{
    public RoleRepository(MusicContext context) : base(context)
    {
    }
    public int Delete(int id)
    {
        context.Roles.Remove(new Role { Id = id });
        return context.SaveChanges();
    }
    public Role? GetRole(int id)
    {
        return context.Roles.Find(id);
    }
    public List<Role> GetRoles()
    {
        return context.Roles.ToList();
    }
    public int Add(Role obj)
    {
        context.Roles.Add(obj);
        return context.SaveChanges();
    }
    public int Edit(Role obj)
    {
        context.Roles.Update(obj);
        return context.SaveChanges();
    }
    public List<Role> GetRolesByMember(string id)
    {
        string sql = "SELECT Role.* FROM Role JOIN MemberInRole ON Role.RoleId = MemberInRole.RoleId AND MemberId = @Id";
        return context.Roles.FromSqlRaw(sql, new SqlParameter("@Id", id)).ToList();
    }
}