using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using DataAccessLayer;

namespace DataAccessLayer
{
    public class DogMapper : Mapper
    {
        int OffsetToDogID;
        int OffsetToName;
        int OffsetToBreedID;
        int OffsetToIsSmallBreed;
        int OffsetToIsDogHairless;
        int OffsetToMedical;
        int OffsetToAdoptDate;
        int OffsetToSurrenderDate;

        public DogMapper(SqlDataReader reader)
        {
            OffsetToDogID = reader.GetOrdinal("DogID");
            Assert(0 == OffsetToDogID, $"DogID is {OffsetToDogID}, instead of 0 as expected");
            OffsetToName = reader.GetOrdinal("Name");
            Assert(1 == OffsetToName, $"Name is {OffsetToName}, instead of 1 as expected");
            OffsetToBreedID = reader.GetOrdinal("BreedID");
            Assert(2 == OffsetToBreedID, $"BreedID is {OffsetToBreedID}, instead of 2 as expected");
            OffsetToIsSmallBreed = reader.GetOrdinal("IsSmallBreed");
            Assert(3 == OffsetToIsSmallBreed, $"IsSmallBreed is {OffsetToIsSmallBreed}, instead of 3 as expected");
            OffsetToIsDogHairless = reader.GetOrdinal("IsDogHairless");
            Assert(4 == OffsetToIsDogHairless, $"IsDogHairless is {OffsetToIsDogHairless}, instead of 4 as expected");
            OffsetToMedical = reader.GetOrdinal("Medical");
            Assert(5 == OffsetToMedical, $"Medical is {OffsetToMedical}, instead of 5 as expected");
            OffsetToAdoptDate = reader.GetOrdinal("AdoptDate");
            Assert(6 == OffsetToAdoptDate, $"AdoptDate is {OffsetToAdoptDate}, instead of 6 as expected");
            OffsetToSurrenderDate = reader.GetOrdinal("SurrenderDate");
            Assert(7 == OffsetToSurrenderDate, $"DogID is {OffsetToSurrenderDate}, instead of 7 as expected");
        }

        public DogDAL ToDog(SqlDataReader reader)
        {
            DogDAL proposedReturnValue = new DogDAL
            {
                DogID = reader.GetInt32(OffsetToDogID),
                Name = reader.GetString(OffsetToName),
                BreedID = reader.GetInt32(OffsetToBreedID),
                IsSmallBreed = reader.GetBoolean(OffsetToIsSmallBreed),
                IsDogHairless = reader.GetBoolean(OffsetToIsDogHairless),
                Medical = reader.GetString(OffsetToMedical),
                AdoptDate = reader.GetDateTime(OffsetToAdoptDate),
                SurrenderDate = reader.GetDateTime(OffsetToSurrenderDate)
            };

            return proposedReturnValue;
        }
    }
}
