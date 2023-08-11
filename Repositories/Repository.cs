using Blog.Models;
using Dapper.Contrib.Extensions;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;


namespace Blog.Repositories
{
    public class Repository<T> where T : class
    {
        private SqlConnection _connection;
        public Repository(SqlConnection connection)
        {
            this._connection = connection;
        }

        public IEnumerable<T> Get()
             => _connection.GetAll<T>();


        public void Create(T TModel)        {
            
            _connection.Insert<T>(TModel);
        }

        public void Update(T TModel)
        {    
            _connection.Update<T>(TModel);           
        }

        public void Delete(T TModel)
        {           
           _connection.Delete<T>(TModel);            
        }

        public void Delete(int id )
        {
            var model = _connection.Get<T>(id);
            _connection.Delete(model);
        }
    }
}
