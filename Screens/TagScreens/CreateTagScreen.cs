using Microsoft.Data.SqlClient;
using Dapper.Contrib.Extensions;
using Blog.Repositories;
using Blog.Models;

namespace Blog.Screens.TagScreens;

public static class CreateTagScreen
{
    public static void Load()
    {
        Create();
    }
    public static void Create()
    {
        Console.Clear();
        Console.WriteLine("Digite o nome da tag: ");
        string tagName = Convert.ToString(Console.ReadLine());
        Console.WriteLine("Digite o slug da tag: ");
        string tagSlug = Convert.ToString(Console.ReadLine());
        Tag tag = new Tag
        {
            Name = tagName,
            Slug = tagSlug
        };

        var repo = new Repository<Tag>(Database.connection);
        repo.Create(tag);
        Tag insertedTag = repo.GetOne(tag.Id);
        
        System.Console.WriteLine("-----");
        System.Console.WriteLine($"A tag {insertedTag.Name} foi inserida.");
    }
}