using Dapper.Contrib.Extensions;
using Microsoft.Data.SqlClient;
using Blog.Repositories;

namespace Blog.Models;

[Table("[User]")]
public class User
{
    public User()
        => Roles = new List<Role>();
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public string Bio { get; set; }
    public string Image { get; set; }
    public string Slug { get; set; }
    
    [Write(false)]
    public List<Role> Roles { get; set; }

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
        {
            System.Console.WriteLine(user.Name);
            foreach(var role in user.Roles)
            {
                System.Console.WriteLine(role.Name);
            }
        }

    }

    public static void ReadUsersWithRole(SqlConnection connection)
    {
        var repo = new UserRepository(connection);
        var items = repo.ReadWithRoles();

        foreach(var item in items)
        {
            System.Console.WriteLine(item.Name);
            foreach(var role in item.Roles)
                System.Console.WriteLine($" - {role.Name}");
        }
    }

    public static void CreateUser(SqlConnection connection, User user)
    {
        var repo = new Repository<User>(connection);
        repo.Create(user);
        System.Console.WriteLine($"O usuário {user.Name} foi adicionado.");
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

    public static void DeleteUser(SqlConnection connection, int id)
    {
        var repo = new Repository<User>(connection);
        User user = new();

        if(id != 0)
        {
            user = repo.GetOne(id);
            repo.Delete(id);
            System.Console.WriteLine($"O usuário {user.Name} foi deletado.");
        } 
        else
        {
            System.Console.WriteLine("Id inválido.");
        }
        
    }
}