using Microsoft.Extensions.Configuration;
using NLog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using UsersAPI.Model;

namespace UsersAPI.Provider
{
    public class DbProvider
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private readonly SqlConnection Connection;
        private readonly SqlConnection ConnectionToCreateDb;

        public DbProvider(IConfiguration configuration)
        {
            Connection = new SqlConnection(configuration.GetConnectionString("Connection"));
            ConnectionToCreateDb = new SqlConnection(configuration.GetConnectionString("ConnectionToCreateDb"));
        }

        private string DataBaseUsers = @"CREATE DATABASE ListUsers;";
        private string TableUsers = @"
            CREATE TABLE Users
            (
	            Id UNIQUEIDENTIFIER PRIMARY KEY default NEWID(),
	            FIO NVARCHAR(50),
	            UserLogin NVARCHAR(50),
	            DateAddUser DATETIME,
	            DeleteFlag BIT
            )";

        private void DataBaseExist()
        {
            try
            {
                SqlCommand cmd = new SqlCommand(DataBaseUsers, ConnectionToCreateDb);
                if (!Database.Exists(Connection))
                {
                    ConnectionToCreateDb.Open();
                    cmd.ExecuteNonQuery();

                    cmd.Connection = Connection;
                    Connection.Open();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = TableUsers;
                    cmd.ExecuteNonQuery();
                    Connection.Close();
                    cmd.Dispose();
                }
            }
            catch (Exception ex)
            {
                logger.Trace(ex);
            }
            finally
            {
                if (ConnectionToCreateDb.State == ConnectionState.Open)
                {
                    ConnectionToCreateDb.Close();
                }
            }
        }

        private void Query(string sqlQ, SqlCommand cmd)
        {
            try
            {
                using (Connection)
                {
                    cmd.Connection = Connection;
                    Connection.Open();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sqlQ;
                    cmd.ExecuteNonQuery();
                    Connection.Close();
                    cmd.Dispose();
                }
            }
            catch (Exception ex)
            {
                logger.Trace(ex);
            }
        }

        public List<User> SelectUsers()
        {
            DataBaseExist();
            try
            {
                using (Connection)
                {
                    string query = "SELECT Id, FIO, UserLogin, DateAddUser FROM Users WHERE DeleteFlag = 1";
                    List<User> Result = new();
                    Connection.Open();
                    SqlCommand cmd = new(query, Connection);
                    SqlDataReader Read = cmd.ExecuteReader();
                    if (Read.HasRows)
                    {
                        while (Read.Read())
                        {
                            User obj = new()
                            {
                                Id = Read[0] == DBNull.Value ? null : Read[0].ToString(),
                                FIO = Read[1] == DBNull.Value ? null : Read[1].ToString(),
                                UserLogin = Read[2] == DBNull.Value ? null : Read[2].ToString(),
                                DateAddUser = Read[3] == DBNull.Value ? null : (DateTime)Read[3]
                            };
                            Result.Add(obj);
                        }
                    }
                    Connection.Close();
                    return Result;
                }
            }
            catch
            {
                throw;
            }
        }

        public void Insert(User user)
        {
            DataBaseExist();
            try
            {
                string query = @"INSERT INTO Users (FIO, UserLogin, DateAddUser, DeleteFlag) VALUES 
                                (@FIOIns, @UserLoginIns, GETDATE(), 1)";

                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add("@FIOIns", SqlDbType.NVarChar, 50).Value = user.FIO;
                cmd.Parameters.Add("@UserLoginIns", SqlDbType.NVarChar, 50).Value = user.UserLogin;

                Query(query, cmd);
            }
            catch
            {
                throw;
            }
        }

        public void Update(User user)
        {
            DataBaseExist();
            try
            {
                string query = @"UPDATE Users SET FIO=N@FIOUp, UserLogin=N@UserLoginUp 
                            WHERE Id=@IdUp";

                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add("@IdUp", SqlDbType.UniqueIdentifier).Value = Guid.Parse(user.Id);
                cmd.Parameters.Add("@FIOUp", SqlDbType.NVarChar, 50).Value = user.FIO;
                cmd.Parameters.Add("@UserLoginUp", SqlDbType.NVarChar, 50).Value = user.UserLogin;

                Query(query, cmd);
            }
            catch
            {
                throw;
            }
        }

        public void Delete(Guid id)
        {
            DataBaseExist();
            try
            {
                string query = @"UPDATE Users SET DeleteFlag = 0 WHERE Id = @IdDel";

                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add("@IdDel", SqlDbType.UniqueIdentifier).Value = id;

                Query(query, cmd);
            }
            catch
            {
                throw;
            }
        }

        public void DeleteAll()
        {
            DataBaseExist();
            try
            {
                string query = @"UPDATE Users SET DeleteFlag = 0";

                SqlCommand cmd = new SqlCommand();

                Query(query, cmd);
            }
            catch
            {
                throw;
            }
        }
    }
}
