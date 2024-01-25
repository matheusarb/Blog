using Blog.Models;
using Blog.Repositories;
using Microsoft.Data.SqlClient;

namespace Blog;

public class UserRepository : Repository<User>
{
    private readonly SqlConnection _connection;
    public UserRepository(SqlConnection connection)
    :base(connection)
    {
        _connection = connection;
    }
}