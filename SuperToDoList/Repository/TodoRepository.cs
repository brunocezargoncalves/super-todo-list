using Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper;

namespace Repository
{
    public class TodoRepository : Connection, Interfaces.ITodoRepository
    {
        public TodoRepository(IConfiguration config) : base(config) { }

        public void Add()
        {
            throw new NotImplementedException();
        }

        public Todo Get(int id)
        {
            throw new NotImplementedException();
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

        public void Remove(Todo entity)
        {
            throw new NotImplementedException();
        }

        public void Update(Todo entity)
        {
            throw new NotImplementedException();
        }
    }
}
