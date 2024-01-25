using Microsoft.Data.SqlClient;
using Dapper.Contrib.Extensions;
using Blog.Repositories;
using Blog.Models;

namespace Blog.Screens.TagScreens;

public static class DeleteTagScreen
{
    public static void Load()
    {
        Delete();
    }

    public static void Delete()
    {
        var repo = new Repository<Tag>(Database.connection);
        Console.Clear();
        System.Console.WriteLine("Escolha o id da tag que deseja deletar: \n");
        DisplayTags();        
        short selectedId = short.Parse(Console.ReadLine());

        repo.Delete(selectedId);
        System.Console.WriteLine("A tag foi deletada. Lista atualizada das tags: \n");
        DisplayTags();
    }

    public static void DisplayTags()
    {
        var repo = new Repository<Tag>(Database.connection);
        var tags = repo.GetAll();
        foreach(var tag in tags)
        {
            System.Console.WriteLine($"{tag.Id}| {tag.Name} - {tag.Slug}");
        }
    }
}