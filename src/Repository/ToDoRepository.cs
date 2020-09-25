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
    public class ToDoRepository : Connection, IToDoRepository
    {
        public ToDoRepository(IConfiguration configuration) : base(configuration) { }

        public IEnumerable<ToDo> GetAll()
        {
            IEnumerable<ToDo> ToDos;
            string sql = "SELECT * FROM ToDos";

            using (var con = new SqlConnection(base.GetConnection()))
            {
                ToDos = con.Query<ToDo>(sql);
            }

            return ToDos;
        }

        public ToDo GetById(Guid id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Id", id, DbType.Guid);

            string sql = "SELECT * FROM ToDos WHERE Id = @Id";

            using (var con = new SqlConnection(base.GetConnection()))
            {
                var r = con.Query<ToDo>(sql, parameters);
                return r.FirstOrDefault();
            }
        }

        public void Add(ToDo ToDo)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Id", ToDo.Id);
            parameters.Add("@Start", ToDo.Start);
            parameters.Add("@UserId", ToDo.UserId);
            parameters.Add("@Task", ToDo.Task);
            parameters.Add("@Description", ToDo.Description);
            parameters.Add("@Forecast", ToDo.Forecast);
            parameters.Add("@End", ToDo.End);

            string sql = "INSERT INTO ToDos (Id, Start, UserId, Task, Description, Forecast, \"End\") VALUES(@Id, @Start, @UserId, @Task, @Description, @Forecast, @End)";

            using (var con = new SqlConnection(base.GetConnection()))
            {
                con.Execute(sql, parameters);
            }
        }

        public void Update(ToDo ToDo)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Id", ToDo.Id);
            parameters.Add("@UserId", ToDo.UserId);
            parameters.Add("@Task", ToDo.Task);
            parameters.Add("@Description", ToDo.Description);
            parameters.Add("@Forecast", ToDo.Forecast);
            parameters.Add("@End", ToDo.End);

            string sql = @"UPDATE ToDos SET UserId = @UserId, Task = @Task, Description = @Description, Forecast = @Forecast, End = @End WHERE Id = @Id";

            using (var con = new SqlConnection(base.GetConnection()))
            {
                con.Execute(sql, parameters);
            }
        }

        public void Remove(Guid id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Id", id, DbType.Guid);

            string sql = "DELETE FROM ToDos WHERE Id = @Id";

            using (var con = new SqlConnection(base.GetConnection()))
            {
                con.Execute(sql, parameters);
            }
        }
    }
}
