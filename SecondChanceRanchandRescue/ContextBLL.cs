using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using System.Configuration;


namespace BusinessLogicLayer
{
    public class ContextBLL : IDisposable
    {
        ContextDAL _context = new ContextDAL();
        public ContextBLL()
        {
            _context.ConnectionString= 
            System.Configuration.ConnectionStrings["DefaultConn"}.ConnectionString;
        }

    }
}
