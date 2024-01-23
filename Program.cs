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

        ReadUsers(connection);
        ReadRoles(connection);
        
        connection.Close();
        // var userRepo = new UserRepository(connection);
        // userRepo.Get(1);
        // ReadUsers();
        // ReadUser(2);
        // CreateUser();
        // UpdateUser();
        // DeleteUser(2);
    }

    public static void ReadUsers(SqlConnection connection)
    {
        var repository = new UserRepository(connection);
        var users = repository.GetAll();

        foreach(var user in users)
            System.Console.WriteLine(user.Name);
    }

    public static void ReadRoles(SqlConnection connection)
    {
        var repository = new RoleRepository(connection);
        var roles = repository.GetAll();

        foreach(var role in roles)
            System.Console.WriteLine(role.Name);
    }
}