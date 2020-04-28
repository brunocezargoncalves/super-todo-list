using Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper;
using System.Linq;
using System.Data;

namespace Repository
{
    public class TodoRepository : Connection, Interfaces.ITodoRepository
    {
        public TodoRepository(IConfiguration config) : base(config) { }

        public void Add(Todo todo)
        {
            DynamicParameters p = new DynamicParameters();
            p.Add("@Task", todo.Task);

            string sql = "INSERT INTO Todo (Task) VALUES(@Task)";

            using (var con = new SqlConnection(base.GetConnection()))
            {
                con.Execute(sql, p);
            }
        }

        public Todo Get(int id)
        {
            DynamicParameters p = new DynamicParameters();
            p.Add("@Id", id, DbType.Int32);

            string sql = "SELECT * FROM Todo WHERE Id = @Id";

            using (var con = new SqlConnection(base.GetConnection()))
            {
                var r = con.Query<Todo>(sql, p);
                return r.FirstOrDefault();
            }
        }

        public IEnumerable<Todo> GetAll()
        {
            IEnumerable<Todo> todolist;
            string sql = "SELECT * FROM Todo";

            using (var con = new SqlConnection(base.GetConnection()))
            {
                todolist = con.Query<Todo>(sql);
            }

            return todolist;
        }

        public void Remove(int id)
        {
            DynamicParameters p = new DynamicParameters();
            p.Add("@Id", id, DbType.Int32);

            string sql = "DELETE FROM Todo WHERE Id = @Id";

            using (var con = new SqlConnection(base.GetConnection()))
            {
                con.Execute(sql, p);
            }
        }

        public void Edit(Todo todo)
        {
            DynamicParameters p = new DynamicParameters();
            p.Add("@Id", todo.Id);
            p.Add("@Task", todo.Task);

            string sql = @"UPDATE Todo SET Task = @Task WHERE Id = @Id";

            using (var con = new SqlConnection(base.GetConnection()))
            {
                con.Execute(sql, p);
            }
        }
    }
}
