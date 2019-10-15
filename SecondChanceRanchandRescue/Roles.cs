using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class RoleDAL
    {
        public int RoleID { get; set; }
        public string RoleName { get; set; }

        public override string ToString()
        {
            return $"{RoleID, 0} {RoleName}";
        }

    }
}
