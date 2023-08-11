using Blog.Models;
using Blog.Repositories;
using Blog.Screens.TagScreens;
using Dapper.Contrib.Extensions;
using Microsoft.Data.SqlClient;
using System;

using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Blog
{
    internal class Program
    {
        private static readonly string CONNECTION_STRING = "Server=localhost,1433;Database=BLOG;User ID=sa;Password=1q2w3e4r@#$;TrustServerCertificate=True;";
        static void Main(string[] args)
        {
            var connection = new SqlConnection(CONNECTION_STRING);

            connection.Open();

            ReadUsers(connection);
            ReadRoles(connection);
            ReadTags(connection);
            connection.Close();
            Console.ReadKey();
        }

        public static void ReadUsers(SqlConnection connection)
        {
            var repository = new Repository<User>(connection);
            var items = repository.Get();
              
            foreach (var item in items)
            {
                Console.WriteLine(item.Name);
            }   
        }

        public static void ReadRoles(SqlConnection connection)
        {
            var repository = new Repository<Role>(connection);
            var items = repository.Get();

            foreach (var item in items)
            {
                Console.WriteLine(item.Name);
            }
        }

        public static void ReadTags(SqlConnection connection)
        {
            var repository = new Repository<Tag>(connection);
            var items = repository.Get();

            foreach (var item in items)
            {
                Console.WriteLine(item.Name);
            }
        }


    }
}
