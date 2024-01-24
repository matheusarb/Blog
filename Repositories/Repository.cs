using System.Collections.Generic;
using Dapper.Contrib.Extensions;
using Microsoft.Data.SqlClient;

namespace Blog.Models;

public class Repository<TModel> where TModel : class
{
    private readonly SqlConnection _connection;

    public Repository(SqlConnection connection)
        => _connection = connection;

    public IEnumerable<TModel> GetAll()
        => _connection.GetAll<TModel>();

    public TModel Get(int id)
        => _connection.Get<TModel>(id);
}