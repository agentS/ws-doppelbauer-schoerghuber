using GraphOverflow.Domain;
using Npgsql;
using System;
using System.Threading.Tasks;

namespace GraphOverflow.Dal.Implementation
{
  public class UserDao : IUserDao
  {
    private readonly string connectionString;

    public UserDao(string connectionString)
    {
      this.connectionString = connectionString;
    }

    public async Task<User> FindById(int id)
    {
      User user = null;
      string sql = "select id, name, password_hash from app_user where id = @id";
      await using (var conn = new NpgsqlConnection(this.connectionString))
      {
        await conn.OpenAsync();
        await using (var cmd = new NpgsqlCommand(sql, conn))
        {
          cmd.Parameters.AddWithValue("id", id);
          await using (NpgsqlDataReader reader = await cmd.ExecuteReaderAsync())
          {
            if (await reader.ReadAsync())
            {
              var userId = (int)reader["id"];
              var userName = (string)reader["name"];
              var passwordHash = (string)reader["password_hash"];
              user = new User { Id = userId, Name = userName, PasswordHash = passwordHash };
            }
          }
        }
      }
      return user;
    }

    public async Task<User> FindByName(string userName)
    {
      User user = null;
      string sql = "select id, name, password_hash from app_user where name = @name";
      await using (var conn = new NpgsqlConnection(this.connectionString))
      {
        await conn.OpenAsync();
        await using (var cmd = new NpgsqlCommand(sql, conn))
        {
          cmd.Parameters.AddWithValue("name", userName);
          await using (NpgsqlDataReader reader = await cmd.ExecuteReaderAsync())
          {
            if (await reader.ReadAsync())
            {
              var userId = (int)reader["id"];
              var name = (string)reader["name"];
              var passwordHash = (string)reader["password_hash"];
              user = new User { Id = userId, Name = name, PasswordHash = passwordHash };
            }
          }
        }
      }
      return user;
    }
  }
}
