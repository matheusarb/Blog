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

    public static void ReadUser(int id)
    {
        using(var connection = new SqlConnection(CONNECTION_STRING))
        {
            var user = connection.Get<User>(id);

            System.Console.WriteLine($"{user.Id} - {user.Name}");
        }
    }

    public static void CreateUser()
    {
        var user = new User()
        {
            Name = "Matheus Ribeiro",
            Email = "email@email.com",
            PasswordHash = "Hash",
            Bio = "IBM Tech Leader",
            Image = "https://",
            Slug = "matheus-ribeiro"
        };

        using(var connection = new SqlConnection(CONNECTION_STRING))
        {
            var userId = connection.Insert<User>(user);

            System.Console.WriteLine($"O usuário de id {userId} foi cadastrado");
        }
    }

    public static void UpdateUser()
    {
        var updatedUser = new User
        {
            Id = 2,
            Name = "Matheus de Araújo Ribeiro",
            Email = "matheus@email.com",
            PasswordHash = "Hash",
            Bio = "IBM Lead Software Engineer",
            Image = "https://...",
            Slug = "matheus-ribeiro-lead-software-engineer"
        };

        using(var connection = new SqlConnection(CONNECTION_STRING))
        {         
            connection.Update<User>(updatedUser);
            System.Console.WriteLine("Atualização realizada com sucesso");
        }
    }
    
    public static void DeleteUser(int id)
    {
        using(var connection = new SqlConnection(CONNECTION_STRING))
        {
            var userToBeDeleted = connection.Get<User>(id);
            var deletedUser = connection.Delete<User>(userToBeDeleted);
            System.Console.WriteLine($"O usuário {userToBeDeleted.Name} foi excluído com sucesso.");  
        }
    }

}