using Microsoft.Data.SqlClient;
using Dapper.Contrib.Extensions;
using Blog.Repositories;
using Blog.Models;

namespace Blog.Screens.TagScreens;

public static class UpdateTagScreen
{
    public static void Load()
    {
        Update();
    }

    public static void Update()
    {
        var repo = new Repository<Tag>(Database.connection);
        
        System.Console.WriteLine("Escolha o id da tag que você deseja atualizar: ");
        var tags = repo.GetAll();
        foreach(var tag in tags)
        {
            System.Console.WriteLine($"{tag.Id}| {tag.Name} - {tag.Slug}");
        }
        var selectedId = short.Parse(Console.ReadLine());

        Console.Clear();

        System.Console.WriteLine("Digite o novo nome da Tag: ");
        var newName = Convert.ToString(Console.ReadLine());
        System.Console.WriteLine("Digite o novo slug da Tag: ");
        var newSlug = Convert.ToString(Console.ReadLine());
        Tag updatedTag = new() 
        {
            Id = selectedId,
            Name = newName,
            Slug = newSlug
        };

        repo.Update(updatedTag);
        Tag upTag = repo.GetOne(selectedId);
        System.Console.WriteLine($"Tag atualizada.\n{upTag.Name} - {upTag.Slug}");
    }
}