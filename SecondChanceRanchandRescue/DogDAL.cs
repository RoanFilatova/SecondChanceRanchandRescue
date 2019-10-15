using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class DogDAL
    {
        #region Primary Columns
        public int DogID { get; set; }
        public string Name { get; set; }
        public bool IsSmallBreed { get; set; }
        public bool IsDogHairless { get; set; }
        public string Medical { get; set; }
        public DateTime AdoptDate { get; set; }
        public DateTime SurrenderDate { get; set; }

        #endregion

        #region Foreign Key relation
        public int BreedID { get; set; }
        #endregion

        public override string ToString()
        {
            return $"DogID{DogID,0}, Name{Name,1}, BreedID{BreedID,2}, IsSmallBreed{IsSmallBreed,3}, IsDogHairless{IsDogHairless,4},Medical{Medical,5},AdoptDate{AdoptDate,6} SurrenderDate{SurrenderDate,7}";  
        }

    }
}
