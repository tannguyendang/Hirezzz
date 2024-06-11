using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Hirezzz.Models
{
    public class MemberRepository : BaseRepository
    {
        public MemberRepository(MusicContext context) : base(context)
        {
        }
        public List<Member> GetMembers()
        {
            return context.Members.ToList();
        }
        public Member? GetMember(string id)
        {
            return context.Members.Find(id);
        }
        public int Add(MemberInRole obj)
        {
            return context.Database.ExecuteSqlRaw("EXEC AddMemberInRole @MemberId, @RoleId", new SqlParameter[]{
            new SqlParameter("@MemberId", obj.MemberId),
            new SqlParameter("@RoleId", obj.RoleId)
        });
        }
        public List<RoleChecked> GetRoleByMember(string id)
        {
            return context.RoleCheckeds.FromSqlRaw("EXEC GetRolesByMember @Id", new SqlParameter("@Id", id)).AsEnumerable().ToList();
        }
        public Member? Login(LoginModel obj)
        {
            return context.Members.FromSqlRaw("EXEC LoginMember @Usr, @Pwd", new SqlParameter[]
            {
                new SqlParameter("@Usr", obj.Usr),
                new SqlParameter("@Pwd", Helper.Hash(obj.Pwd))
            }).AsEnumerable().FirstOrDefault();
        }
        public int Add(Member obj)
        {
            if (!context.Members.Any(p => p.Username == obj.Username))
            {
                context.Members.Add(obj);
                return context.SaveChanges();
            }
            return -1;
        }
        public int Add(RegisterModel obj)
        {
            Member member = new Member
            {
                Id = Helper.RandomString(32),
                Username = obj.Usr,
                Email = obj.Eml,
                Fullname = obj.Fullname,
                Gender = obj.Gen,
                MemberPasswords = new List<MemberPassword>{
                //Native
                new MemberPassword{ Password = Helper.Hash(obj.Pwd)}
                },
                memberStringPasswords = new List<MemberStringPassword>{
                new MemberStringPassword{ Password = Helper.HashString(obj.Pwd)}
                }
            };
            return Add(member);
            // return context.Database.ExecuteSqlRaw("EXEC RegisterMember @Id ,@Usr, @Pwd, @Eml, @Fullname, @Gen", new SqlParameter[]{
            //     new SqlParameter("@Id", Helper.RandomString(32)),
            //     new SqlParameter("@Usr", obj.Usr),
            //     new SqlParameter("@Pwd", Helper.Hash(obj.Pwd)),
            //     new SqlParameter("@Eml", obj.Eml),
            //     new SqlParameter("@Fullname", obj.Fullname),
            //     new SqlParameter("@Gen", obj.Gen)
            // });
        }
        public int Delete(string id)
        {
            return context.Database.ExecuteSqlRaw("EXEC DeleteMember @MemberId", new SqlParameter[]{
            new SqlParameter("@MemberId", id)
            });
        }
        public int Edit(Member obj)
        {
            context.Members.Update(obj);
            return context.SaveChanges();
        }
        public int Change(ChangeModel obj)
        {
            return context.Database.ExecuteSqlRaw("EXEC ChangePassword @Id, @OldPwd, @NewPwd", new SqlParameter[]
            {
                new SqlParameter("@Id", obj.Id),
                new SqlParameter("@OldPwd", Helper.Hash(obj.OldPwd)),
                new SqlParameter("@NewPwd", Helper.Hash(obj.NewPwd))
            });
        }
    }
}