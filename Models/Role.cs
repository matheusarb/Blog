using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Data.SqlClient;
using Blog.Repositories;

namespace Blog.Models;

[Table("[Role]")]
public class Role
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Slug { get; set; }

    public static void ReadRole(SqlConnection connection, int id)
    {
        var repo = new Repository<Role>(connection);
        var role = repo.GetOne(id);
        System.Console.WriteLine($"{role.Id}| {role.Name} - {role.Slug}");
    }

    public static void ReadRoles(SqlConnection connection)
    {
        var repo = new Repository<Role>(connection);
        var items = repo.GetAll();

        foreach(var item in items)
        {
            System.Console.WriteLine($"{item.Name} - {item.Slug}");
        }
    }

    public static void UpdateRole(SqlConnection connection, Role role)
    {
        var repo = new Repository<Role>(connection);
        
        if(role.Id != 0)
        {
            repo.Update(role);
            System.Console.WriteLine($"A role de id {role.Id} foi atualizada.");
        }
        else
        {
            System.Console.WriteLine("Id inválido.");
        }
    }

    public static void DeleteRole(SqlConnection connection, int id)
    {
        var repo = new Repository<Role>(connection);
        var roleToDelete = repo.GetOne(id);
        if(id != 0)
        {
            repo.Delete(id);
            System.Console.WriteLine($"A role {roleToDelete.Name} foi deletada.");
        }
        else
        {
            System.Console.WriteLine("Id inválido.");
        }
    }
}