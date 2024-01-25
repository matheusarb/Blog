using Blog.Models;
using Blog.Repositories;
using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;

internal class Program
{
    private const string CONNECTION_STRING = @"
        Server=localhost,1433;
        Database=Blog;
        User ID=sa;
        Password=1q2w3e4r@#$;
        TrustServerCertificate=True";

    private static void Main(string[] args)
    {
        var connection = new SqlConnection(CONNECTION_STRING);

        connection.Open();

        // ReadUsers(connection);
        // ReadRoles(connection);
        UpdateUser(connection, new User
            {
                Id = 4,
                Name = "Matheus de Araújo Ribeiro",
                Email = "matheusarb@gmail.com",
                PasswordHash = "HASH2",
                Bio = "IBM Senior Engineer",
                Image = "http://",
                Slug = "se-matheus"
            });
        ReadUser(connection, 4);

        connection.Close();
        // var userRepo = new UserRepository(connection);
        // userRepo.Get(1);
        // ReadUsers();
        // ReadUser(2);
        // CreateUser();
        // UpdateUser();
        // DeleteUser(2);
    }


    public static void ReadUser(SqlConnection connection, int id)
    {
        var repository = new Repository<User>(connection);
        var user = repository.GetOne(id);

        System.Console.WriteLine(user.Name);
    }
    public static void ReadUsers(SqlConnection connection)
    {
        var repository = new Repository<User>(connection);
        var users = repository.GetAll();

        foreach (var user in users)
            System.Console.WriteLine(user.Name);
    }

    public static void UpdateUser(SqlConnection connection, User user)
    {
        var repo = new Repository<User>(connection);

        if (user.Id != 0)
        {
            connection.Update(user);
        }

        System.Console.WriteLine($"{user.Name}, {user.Email}");
    }
}