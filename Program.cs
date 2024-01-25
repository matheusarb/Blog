using Blog;
using Blog.Models;
using Blog.Repositories;
using Blog.Screens.TagScreens;
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
        Database.connection = new SqlConnection(CONNECTION_STRING);
        // var connection = new SqlConnection(CONNECTION_STRING);

        Database.connection.Open();
                MenuTagScreen.Load();
                Console.ReadKey();
        Database.connection.Close();
    }

    public static void Load()
    {
        System.Console.WriteLine("Meu Blog:");
        System.Console.WriteLine("----------");
        System.Console.WriteLine("O que deseja fazer?");
        System.Console.WriteLine("4- Listar Tags");
        System.Console.WriteLine();
        System.Console.WriteLine();
        var option = short.Parse(Console.ReadLine()!);

        switch(option)
        {
            case 4:
                ListTagScreen.Load();
                break;
        }
    }
}