using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    public class BreedMapper : Mapper
    {
        int OffsetToBreedID;
        int OffsetToBreedName;



        public BreedMapper(SqlDataReader reader)
        {
            OffsetToBreedID = reader.GetOrdinal("BreedID");
            Assert(0 == OffsetToBreedID, $"BreedID is {OffsetToBreedID} instead of 0 as expected");
            OffsetToBreedName = reader.GetOrdinal("BreedName");
            Assert(1 == OffsetToBreedName, $"BreedName is {OffsetToBreedName} instead of 1 as expected");
        }

        public BreedDAL ToBreed(SqlDataReader reader)
        {
            BreedDAL proposedReturnValue = new BreedDAL
            {
                BreedID = reader.GetInt32(OffsetToBreedID),
                BreedName = reader.GetString(OffsetToBreedName)
            };

            return proposedReturnValue;

        }
    }

}
