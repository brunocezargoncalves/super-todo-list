using Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper;
using System.Linq;
using System.Data;
using Repository.Interfaces;

namespace Repository
{
    public class UserRepository : Connection, IUserRepository
    {
        public UserRepository(IConfiguration configuration) : base(configuration) { }

        public IEnumerable<User> GetAll()
        {
            IEnumerable<User> Users;
            string sql = "SELECT * FROM Users";

            using (var con = new SqlConnection(base.GetConnection()))
            {
                Users = con.Query<User>(sql);
            }

            return Users;
        }

        public User GetById(Guid id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Id", id, DbType.Guid);

            string sql = "SELECT * FROM Users WHERE Id = @Id";

            using (var con = new SqlConnection(base.GetConnection()))
            {
                var r = con.Query<User>(sql, parameters);
                return r.FirstOrDefault();
            }
        }

        public void Add(User User)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Registration", User.Registration);
            parameters.Add("@Name", User.Name);
            parameters.Add("@Email", User.Email);
            parameters.Add("@VerifiedEmail", User.VerifiedEmail);
            parameters.Add("@Password", User.Password);
            parameters.Add("@ChangePassword", User.ChangePassword);

            string sql = "INSERT INTO Users (Registration, Name, Email, VerifiedEmail, Password, ChangePassword) VALUES(@Registration, @Name, @Email, @VerifiedEmail, @Password, @ChangePassword)";

            using (var con = new SqlConnection(base.GetConnection()))
            {
                con.Execute(sql, parameters);
            }
        }

        public void Update(User User)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Id", User.Id);
            parameters.Add("@Registration", User.Registration);
            parameters.Add("@Name", User.Name);
            parameters.Add("@Email", User.Email);
            parameters.Add("@VerifiedEmail", User.VerifiedEmail);
            parameters.Add("@Password", User.Password);
            parameters.Add("@ChangePassword", User.ChangePassword);

            string sql = @"UPDATE Users SET Task = @Task WHERE Id = @Id";

            using (var con = new SqlConnection(base.GetConnection()))
            {
                con.Execute(sql, parameters);
            }
        }

        public void Remove(Guid id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Id", id, DbType.Guid);

            string sql = "DELETE FROM Users WHERE Id = @Id";

            using (var con = new SqlConnection(base.GetConnection()))
            {
                con.Execute(sql, parameters);
            }
        }
    }
}
