using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Data.SqlClient;
using Blog.Repositories;

namespace Blog.Models;

[Table("[Category]")]
public class Category
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Slug { get; set; }
    public List<Post> Posts { get; set; }

    public static void ReadCategory(SqlConnection connection, int id)
    {
        var repo = new Repository<Category>(connection);
        var item = repo.GetOne(id);

        System.Console.WriteLine($"{item.Id}| {item.Name} - {item.Slug}");
    }

    public static void ReadCategories(SqlConnection connection)
    {
        var repo = new Repository<Category>(connection);
        var items = repo.GetAll();

        foreach(var item in items)
        {
            System.Console.WriteLine($"{item.Name} - {item.Slug}");
        }
    }
}