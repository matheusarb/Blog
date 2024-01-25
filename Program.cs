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

        // ReadUsers(connection);
        // ReadRoles(connection);
        // UpdateUser(connection, new User
        //     {
        //         Id = 4,
        //         Name = "Matheus de Araújo Ribeiro",
        //         Email = "matheusarb@gmail.com",
        //         PasswordHash = "HASH2",
        //         Bio = "IBM Senior Engineer",
        //         Image = "http://",
        //         Slug = "se-matheus"
        //     });
        // ReadUser(connection, 4);
        // ReadCategories(connection);
        // DeleteUser(connection, 0);
        // ReadRole(connection, 2);
        // UpdateRole(connection, new Role() {Id =  1, Name = "Autores", Slug="Authors"});
        // DeleteRole(connection, 2);
        // ReadTag(connection, 1);
        // UpdateTag(connection, new Tag(){ Id=1, Name="Backend", Slug="be-database" });
        // DeleteTag(connection, 2);

        // ReadCategory(connection, 1);        
        // CreateUser(connection, new User(){
        //     Name = "joao",
        //     Email = "email",
        //     PasswordHash = "hash",
        //     Bio = "bio",
        //     Image = "image",
        //     Slug = "slug"
        // });

        ReadUsersWithRole(connection);
        connection.Close();
        
        // var userRepo = new UserRepository(connection);
        // userRepo.Get(1);
        // ReadUsers();
        // ReadUser(2);
        // CreateUser();
        // UpdateUser();
        // DeleteUser(2);
    }


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