using Blog.Models;
using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Blog.Repositories
{
    public class UserRepository
    {
        private SqlConnection _connection;
        public UserRepository(SqlConnection connection)
        {
            this._connection = connection;  
        }

        public IEnumerable<User> Get()
           => _connection.GetAll<User>();      

        public User Get(int id)                  
            => _connection.Get<User>(id);


        public void Create(User user)
        {
            user.Id = 0;
           _connection.Insert<User>(user);
        }

        public void Update(User user)
        { 
            if(user.Id != 0)
            { 
                _connection.Update<User>(user);
            }
        }

        public void Delete(User user)
        {
            if (user.Id != 0)
            {
                _connection.Delete<User>(user);
            }
        }

        public void Delete(int id)
        {
            if (id != 0)
            {
                return;                
            }

            var user = _connection.Get<User>(id);
            _connection.Delete(user);
        }

        public List<User> GetWithRole()
        {
            var query = @"
                SELECT 
                    *
                FROM
                [User]
                LEFT JOIN [UserRole] ON [UserRole].[UserId] = [User].[Id]
                LEFT JOIN [Role] ON [Role].[Id] = [UserRole].[RoleId] 
                ";

            var users = new List<User>();

            var items = _connection.Query<User, Role, User>(
                query,
                (user, role) =>
                {
                    var usr = users.FirstOrDefault(x => x.Id == user.Id);
                    if (usr == null)
                    {
                        usr = user;

                        if (role != null)
                        {
                            usr.Roles.Add(role);
                        }
                        users.Add(usr);
                    }
                    else
                    {
                        usr.Roles.Add(role);
                    }
                    return user;
                }, splitOn: "Id");
              

            return users;
        }




    }
}
