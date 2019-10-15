using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;


namespace DataAccessLayer
{

    public class ContextDAL : IDisposable
    {
        #region Context
        SqlConnection sec = new SqlConnection();

        public void Dispose()
        {
            sec.Dispose();
        }

        void EnsureConnected()
        {
            switch (sec.State)
            {
                case
                (System.Data.ConnectionState.Closed):
                    sec.Open();
                    break;
                case
               (System.Data.ConnectionState.Broken):
                    sec.Close();
                    sec.Open();
                    //"Have you tried unplugging it and plugging it back in?"
                    break;
                case
                (System.Data.ConnectionState.Open):
                    //Shouldn't have to do anything as the connection is open
                    break;

            }
        }
        public string ConnectionString
        {
            get
            {
                return sec.ConnectionString;
            }
            set
            {
                sec.ConnectionString = value;
            }
        }
        #endregion Context  

        #region Role Items
        public RoleDAL RoleFindbyID(int RoleID)
        {
            RoleDAL proposedReturnValue = null;
            EnsureConnected();
            using (SqlCommand command = new SqlCommand("RoleFindByID", sec))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("RoleID", @RoleID);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    RoleMapper rm = new RoleMapper(reader);
                    int count = 0;
                    while (reader.Read())
                    {
                        proposedReturnValue = rm.ToRole(reader);
                        count++;
                    }
                    if (count > 1)
                    {
                        throw new Exception($"{ count } many roles were found for ID { RoleID}");
                    }

                }
            }
            return proposedReturnValue;
        }

        public List<RoleDAL> RoleGetAll(int Skip, int Take)
        {
            List<RoleDAL> proposedReturnValue = new List<RoleDAL>();
            EnsureConnected();
            using (SqlCommand command = new SqlCommand("RoleGetAll", sec))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@skip", Skip);
                command.Parameters.AddWithValue("@take", Take);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    RoleMapper rm = new RoleMapper(reader);

                    while (reader.Read())
                    {
                        RoleDAL item = rm.ToRole(reader);
                        proposedReturnValue.Add(item);
                    }
                }
            }
            return proposedReturnValue;
        }
        public int RoleObtainCount()
        {
            int proposedReturnValue = 0;
            EnsureConnected();
            using (SqlCommand command = new SqlCommand("RoleObtainCount", sec))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                proposedReturnValue = (int)command.ExecuteScalar();
            }
            return proposedReturnValue;
        }
        public int RoleCreate(string RoleName)
        {
            int proposedReturnValue = 0;
            EnsureConnected();
            using (SqlCommand command = new SqlCommand("RoleCreate", sec))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@RoleName", RoleName);
                command.Parameters.AddWithValue("@RoleID", 0);
                command.Parameters["@RoleID"].Direction = System.Data.ParameterDirection.Output;
                command.ExecuteNonQuery();
                proposedReturnValue = (int)command.Parameters["@RoleID"].Value;
            }
            return proposedReturnValue;
        }
        public void RoleDelete(int RoleID)
        {
            EnsureConnected();
            using (SqlCommand command = new SqlCommand("RoleDelete", sec))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@RoleID", RoleID);
                command.ExecuteNonQuery();

            }
        }
        public int RoleUpdateSafe(int RoleID, string OldRoleName, string NewRoleName)
        {
            EnsureConnected();
            using (SqlCommand command = new SqlCommand("RoleUpdateSafe", sec))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@RoleID", RoleID);
                command.Parameters.AddWithValue("@OldRoleName", OldRoleName);
                command.Parameters.AddWithValue("@NewRoleName", NewRoleName);
                return command.ExecuteNonQuery();
            }
        }
        #endregion

        #region Users

        public int UserCreate(string UserName, string Name, string Address, string Email, string Hash, string Salt, int RoleID, string DonorStatus)
        {
            int proposedReturnValue = 0;
            EnsureConnected();
            using (SqlCommand command = new SqlCommand("UserCreate", sec))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@UserID", 0);
                command.Parameters.AddWithValue("@UserName", UserName);
                command.Parameters.AddWithValue("@Name", Name);
                command.Parameters.AddWithValue("@Address", Address);
                command.Parameters.AddWithValue("@Email", Email);
                command.Parameters.AddWithValue("@Hash", Hash);
                command.Parameters.AddWithValue("@Salt", Salt);
                command.Parameters.AddWithValue("@RoleID", RoleID);
                command.Parameters.AddWithValue("@DonorStatus", DonorStatus);
                command.Parameters["@UserID"].Direction = System.Data.ParameterDirection.Output;
                command.ExecuteNonQuery();
                proposedReturnValue = (int)command.Parameters["@UserID"].Value;
            }
            return proposedReturnValue;
        }
        public void UserDelete(int UserID)
        {
            EnsureConnected();
            using (SqlCommand command = new SqlCommand("UserDelete", sec))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@UserID", UserID);
                command.ExecuteNonQuery();
            }
        }
        public UserDAL UserFindByID(int UserID)
        {
            UserDAL proposedReturnValue = null;
            EnsureConnected();
            using (SqlCommand command = new SqlCommand("UserFindByID", sec))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@UserID", UserID);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    UserMapper um = new UserMapper(reader);
                    int count = 0;
                    while (reader.Read())
                    {
                        proposedReturnValue = um.ToUser(reader);
                        count++;
                    }
                    if (count > 1)
                    {
                        throw new Exception($"{count} multiple users for ID {UserID}.");
                    }
                }
            }
            return proposedReturnValue;
        }
        public List<UserDAL> UserGetAll(int Skip, int Take)
        {
            List<UserDAL> proposedReturnValue = new List<UserDAL>();
            EnsureConnected();
            using (SqlCommand command = new SqlCommand("UserGetAll", sec))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@skip", Skip);
                command.Parameters.AddWithValue("@take", Take);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    UserMapper um = new UserMapper(reader);
                    while (reader.Read())
                    {
                        UserDAL item = um.ToUser(reader);
                        proposedReturnValue.Add(item);
                    }
                }
            }
            return proposedReturnValue;
        }

        public int UserUpdateSafe(int UserID, string OldUserName, string NewUserName, string OldEmail, string NewEmail, string OldAddress, string NewAddress, string OldHash, string NewHash, string OldSalt, string NewSalt, int OldRoleID, int NewRoleID)
        {
            EnsureConnected();
            using (SqlCommand command = new SqlCommand("UserUpdateSafe", sec))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@UserID", UserID);
                command.Parameters.AddWithValue("@OldUserName", OldUserName);
                command.Parameters.AddWithValue("@NewUserName", NewUserName);
                command.Parameters.AddWithValue("@OldEmail", OldEmail);
                command.Parameters.AddWithValue("@NewEmail", NewEmail);
                command.Parameters.AddWithValue("@OldAddress", OldAddress);
                command.Parameters.AddWithValue("@NewAddress", NewAddress);
                command.Parameters.AddWithValue("@OldHash", OldHash);
                command.Parameters.AddWithValue("@NewHash", NewHash);
                command.Parameters.AddWithValue("@OldSalt", OldSalt);
                command.Parameters.AddWithValue("@NewSalt", NewSalt);
                command.Parameters.AddWithValue("@OldRoleID", OldRoleID);
                command.Parameters.AddWithValue("@NewRoldID", NewRoleID);
                return command.ExecuteNonQuery();
            }
        }
        public List<UserDAL> UsersGetRelatedToRoleID(int Skip, int Take, int RoleID)
        {
            List<UserDAL> proposedReturnValue = new List<UserDAL>();
            EnsureConnected();
            using (SqlCommand command = new SqlCommand("UsersGetRelatedToRoleID"))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Skip", Skip);
                command.Parameters.AddWithValue("@Take", Take);
                command.Parameters.AddWithValue("@RoleID", RoleID);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    UserMapper um = new UserMapper(reader);
                    while (reader.Read())
                    {
                        UserDAL item = um.ToUser(reader);
                        proposedReturnValue.Add(item);
                    }
                }

            }
            return proposedReturnValue;
        }
        public int UserObtainCount()
        {
            int proposedReturnValue = 0;
            EnsureConnected();
            using (SqlCommand command = new SqlCommand("UserObtainCount", sec))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                proposedReturnValue = (int)command.ExecuteScalar();
            }
            return proposedReturnValue;
        }

        public UserDAL UserFindByEmail(string Email)
        {
            UserDAL proposedReturnValue = null;
            EnsureConnected();
            using (SqlCommand command = new SqlCommand("UserFindByID", sec))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("Email", @Email);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    UserMapper um = new UserMapper(reader);
                    int count = 0;
                    while (reader.Read())
                    {
                        proposedReturnValue = um.ToUser(reader);
                        count++;
                    }
                    if (count > 1)
                    {
                        throw new Exception($"{ count } many users were found for email {Email}");
                    }

                }
            }
            return proposedReturnValue;

        }
        #endregion Users

        #region Breed
        public int BreedCreate(int BreedID, string BreedName)
        {
            int proposedReturnvalue = 0;
            EnsureConnected();
            using (SqlCommand command = new SqlCommand("BreedCreate", sec))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@BreedID", BreedID);
                command.Parameters.AddWithValue("@BreedName", BreedName);
                command.ExecuteNonQuery();
                proposedReturnvalue = (int)command.Parameters["@BreedID"].Value;
            }
            return proposedReturnvalue;
        }
        #endregion

        public int DogCreate(int DogID, string Name, int BreedID, bool IsSmallBreed, bool IsDogHairless, string Medical, DateTime AdoptDate, DateTime SurrenderDate)
        {
            int proposedReturnValue = 0;
            EnsureConnected();
            using (SqlCommand command = new SqlCommand("DogCreate", sec))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@DogID", DogID);
                command.Parameters.AddWithValue("@Name", Name);
                command.Parameters.AddWithValue("@BreedID", BreedID);
                command.Parameters.AddWithValue("@IsSmallBreed", IsSmallBreed);
                command.Parameters.AddWithValue("@IsDogHairless", IsDogHairless);
                command.Parameters.AddWithValue("@Medical", Medical);
                command.Parameters.AddWithValue("@AdoptDate", AdoptDate);
                command.Parameters.AddWithValue("@SurrenderDate", SurrenderDate);
                command.ExecuteNonQuery();
                proposedReturnValue = (int)command.Parameters["@DogID"].Value;
            }
            return proposedReturnValue;
        }

        public void DogDelete(int DogID)
        {
            EnsureConnected();
            using (SqlCommand command = new SqlCommand("DogDelete", sec))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@DogID", DogID);
                command.ExecuteNonQuery();
            }
        }

        public List<DogDAL> DogsGetAll(int Skip, int Take)
        {
            List<DogDAL> proposedReturnValue = new List<DogDAL>();
            EnsureConnected();
            using (SqlCommand command = new SqlCommand("DogsGetAll"))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Skip", Skip);
                command.Parameters.AddWithValue("@Take", Take);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DogMapper dm = new DogMapper(reader);
                    while (reader.Read())
                    {
                        DogDAL item = dm.ToDog(reader);
                        proposedReturnValue.Add(item);
                    }
                }

            }
            return proposedReturnValue;
        }
        public int DogObtainCount()
        {
            int proposedReturnValue = 0;
            EnsureConnected();
            using (SqlCommand command = new SqlCommand("DogObtainCount", sec))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                proposedReturnValue = (int)command.ExecuteScalar();
            }
            return proposedReturnValue;

        }
        public DogDAL DogFindByBreed(int BreedID)
        {
            DogDAL proposedReturnValue = null;
            EnsureConnected();
            using(SqlCommand command = new SqlCommand("DogFindByBreed", sec))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@BreedID", BreedID);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DogMapper dm = new DogMapper(reader);
                    while (reader.Read())
                    {
                        proposedReturnValue = dm.ToDog(reader);
                    }
                }
            }
            return proposedReturnValue;
        }
        public DogDAL DogFindByID(int DogID)
        {
            DogDAL proposedReturnValue = null;
            EnsureConnected();
            using (SqlCommand command = new SqlCommand("DogFindByID", sec))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@DogID", DogID);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DogMapper dm = new DogMapper(reader);
                    int count = 0;
                    while (reader.Read())
                    {
                        proposedReturnValue = dm.ToDog(reader);
                        count++;
                    }
                    if ( count > 1)
                    {
                        throw new Exception($"{count} many users found for ID {DogID}");
                    }
                }
            }
            return proposedReturnValue;
        }
        public int DogUpdateSafe(int DogID, DateTime SurrenderDate, DateTime AdoptDate, string OldMedical, string NewMedical, string Medical)
        {
            EnsureConnected();
            using (SqlCommand command = new SqlCommand("DogUpdateSafe", sec))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@DogID", DogID);
                command.Parameters.AddWithValue("@SurrenderDate", SurrenderDate);
                command.Parameters.AddWithValue("@AdoptDate", AdoptDate);
                command.Parameters.AddWithValue("@OldMedical", OldMedical);
                command.Parameters.AddWithValue("@NewMedical", NewMedical);
                command.Parameters.AddWithValue("@Medical", Medical);
                return command.ExecuteNonQuery();
            }
        }
    }
}

     


