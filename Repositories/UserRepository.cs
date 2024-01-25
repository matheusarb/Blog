using Blog.Models;
using Dapper;
using Microsoft.Data.SqlClient;

namespace Blog.Repositories;

public class UserRepository : Repository<User>
{
    private readonly SqlConnection _connection;
    public UserRepository(SqlConnection connection)
    : base(connection)
        => _connection = connection;

    public List<User> ReadWithRoles()
    {
        var query = @"
            SELECT
                [User].*,
                [Role].*
            FROM
                [User]
                LEFT JOIN [UserRole] ON [UserRole].[UserId] = [User].[Id]
                LEFT JOIN [Role] ON [UserRole].[RoleId] = [Role].[Id]";
        
        var users = new List<User>();

        var items = _connection.Query<User, Role, User>(
            query,
            (user, role) =>
            {
                var tempUsr = users.FirstOrDefault(x=>x.Id == user.Id);
                if(tempUsr == null)
                {
                    tempUsr = user;
                    if(role != null)
                        tempUsr.Roles.Add(role);
                    
                    users.Add(tempUsr);
                }
                else
                {
                    tempUsr.Roles.Add(role);
                }
                return user;
            }, splitOn: "Id");

        return users;
    }
}