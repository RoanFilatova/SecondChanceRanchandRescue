using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class BreedDAL
    {
        public int BreedID { get; set; }
        public string BreedName { get; set; }

        public override string ToString()
        {
            return $"BreedID{BreedID,0}, BreedName{BreedName,1}";
        }
    }
}
