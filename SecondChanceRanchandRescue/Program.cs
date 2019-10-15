using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;

namespace SecondChanceRanchandRescue
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ContextDAL ctx = new ContextDAL())
            {
                ctx.ConnectionString = "Data Source=.\\sqlexpress;Initial Catalog=SecondChance;Integrated Security=True";

                #region roles

                var a = ctx.RoleGetAll(0, 100);
                foreach (var i in a)
                {
                    Console.WriteLine(i);
                }
                Console.WriteLine(ctx.RoleObtainCount());

                Console.WriteLine(ctx.RoleUpdateSafe(1, "Admin", "Administrator"));


                #endregion

                #region Users
                //int number = ctx.UserObtainCount();

                //ctx.UserCreate("GarretC", "Garrett Council", "557 Autumn PL Fountain CO 80817", "garrett@secondchance.org", "XXX", "YYY", 4, "True");

                //foreach (var u in ctx.UserGetAll(0, 100))
                //{
                //    Console.WriteLine(u);
                //}

                #endregion

            }   
        }
    }
}
