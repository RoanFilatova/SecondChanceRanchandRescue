using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;



namespace DataAccessLayer
{
   class UserMapper : Mapper
    {
        int OffsetToUserID;
        int OffsetToUserName;
        int OffsetToName;
        int OffsetToAddress;
        int OffsetToEmail;
        int OffsetToHash;
        int OffsetToSalt;
        int OffsetToRoleID;
        int OffsetToDonorStatus;

        public UserMapper(SqlDataReader reader)
        {
            OffsetToUserID = reader.GetOrdinal("UserID");
            Assert(0 == OffsetToUserID, $"UserID is {OffsetToUserID}, instead of 0 as expected");
            OffsetToUserName = reader.GetOrdinal("UserName");
            Assert(1 == OffsetToUserName, $"UserName is {OffsetToUserName}, instead of 1 as expected");
            OffsetToName = reader.GetOrdinal("Name");
            Assert(2 == OffsetToName, $"Name is {OffsetToName}, instead of 2 as expected");
            OffsetToAddress = reader.GetOrdinal("Address");
            Assert(3 == OffsetToAddress, $"Address is {OffsetToAddress}, instead of 3 as expected");
            OffsetToEmail = reader.GetOrdinal("Email");
            Assert(4 == OffsetToEmail, $"Email is {OffsetToEmail}, instead of 4 as expected");
            OffsetToHash = reader.GetOrdinal("Hash");
            Assert(5 == OffsetToHash, $"Hash is {OffsetToHash}, instead of 5 as expected");
            OffsetToSalt = reader.GetOrdinal("Salt");
            Assert(6 == OffsetToSalt, $"Salt is {OffsetToSalt}, instead of 6 as expected");
            OffsetToRoleID = reader.GetOrdinal("RoleID");
            Assert(7 == OffsetToRoleID, $"RoleID is {OffsetToRoleID}, instead of 7 as expected");
            OffsetToDonorStatus = reader.GetOrdinal("DonorStatus");
            Assert(8 == OffsetToDonorStatus, $"DonorStatus is {OffsetToDonorStatus}, instead of 8 as expected");
        }

        public UserDAL ToUser(SqlDataReader reader)
        {
            UserDAL proposedReturnValue = new UserDAL
            {
                UserID = reader.GetInt32(OffsetToUserID),
                UserName = reader.GetString(OffsetToUserName),
                Name = reader.GetString(OffsetToName),
                Address = reader.GetString(OffsetToAddress),
                Email = reader.GetString(OffsetToEmail),
                Hash = reader.GetString(OffsetToHash),
                Salt = reader.GetString(OffsetToSalt),
                RoleID = reader.GetInt32(OffsetToRoleID),
                DonorStatus = reader.GetString(OffsetToDonorStatus)
            };

            return proposedReturnValue;
        }
    }
}
