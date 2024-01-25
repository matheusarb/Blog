using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Data.SqlClient;
using Blog.Repositories;

namespace Blog.Models;

[Table("[Tag]")]
public class Tag
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Slug { get; set; }

    public static void ReadTags(SqlConnection connection)
    {
        var repo = new Repository<Tag>(connection);
        var items = repo.GetAll();

        foreach(var item in items)
        {
            System.Console.WriteLine($"{item.Name} - {item.Slug}");
        }
    }
    public static void ReadTag(SqlConnection connection, int id)
    {
        var repo = new Repository<Tag>(connection);
        var item = repo.GetOne(id);
        if(item == null)
        {
            System.Console.WriteLine("Essa tag não existe");
            return;
        }

        System.Console.WriteLine($"{item.Id}| {item.Name} - {item.Slug}");
    }

    public static void UpdateTag(SqlConnection connection, Tag tag)
    {
        var repo = new Repository<Tag>(connection);
        repo.Update(tag);
        System.Console.WriteLine($"A tag {tag.Name} foi atualizada.");
    }
    
    public static void DeleteTag(SqlConnection connection, int id)
    {
        var repo = new Repository<Tag>(connection);
        Tag tagToDelete = repo.GetOne(id);
        if(id != 0)
        {
            repo.Delete(id);
            System.Console.WriteLine($"A tag {tagToDelete.Name} foi deletada.");
        }
        else
        {
            System.Console.WriteLine("Id da tag inválido.");
        }
    }
}