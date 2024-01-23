﻿using Blog.Models;
using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.Data.SqlClient;

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
        // ReadUsers();
        ReadUser(2);
    }

    public static void ReadUsers()
    {
        using(var connection = new SqlConnection(CONNECTION_STRING))
        {
            var users = connection.GetAll<User>();

            foreach(var user in users)
            {
                System.Console.WriteLine(user.Name);
            }
        }
    }
    public static void ReadUser(int id)
    {
        using(var connection = new SqlConnection(CONNECTION_STRING))
        {
            var user = connection.Get<User>(id);

            System.Console.WriteLine($"{user.Id} - {user.Name}");
        }
    }

    public static void CreateUser()
    {
        
    }
}