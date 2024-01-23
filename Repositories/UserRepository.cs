using System.Data.Common;
using Blog.Models;
using Dapper.Contrib.Extensions;
using Microsoft.Data.SqlClient;

namespace Blog.Repositories;

public class UserRepository
{
    private readonly SqlConnection _connection; 
 
    public UserRepository(SqlConnection connection)
    {
        _connection = connection;
    }

    public User Get(int id)
        => _connection.Get<User>(id);
    
    public IEnumerable<User> GetAll()
        => _connection.GetAll<User>();
    
    public void Create(User user)
    {
        user.Id = 0;
        _connection.Insert<User>(user);
    }

    public void Update(User user)
    {
        if (user.Id != 0)
            _connection.Update<User>(user);
    }

    public void Delete(User user)
    {
        if(user.Id != 0)
            _connection.Delete<User>(user);
    }
    public void Delete(int id)
    {
        var userId = _connection.Get<User>(id);
        _connection.Delete<User>(userId);
    }
}