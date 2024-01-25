using Microsoft.Data.SqlClient;
using Dapper.Contrib.Extensions;
using Blog.Repositories;
using Blog.Models;

namespace Blog.Screens.TagScreens;

public static class ListTagScreen
{
    public static void Load()
    {
        Console.Clear();
        System.Console.WriteLine("Lista de Tags");
        System.Console.WriteLine("--------------");
        //só chama os métodos
        List();
        Console.ReadKey();
    }

    public static void List()
    {
        var repo = new Repository<Tag>(Database.connection);
        var tags = repo.GetAll();
        foreach(var tag in tags)
        {
            System.Console.WriteLine($"{tag.Id}| {tag.Name} - {tag.Slug}");
        }
    }
}