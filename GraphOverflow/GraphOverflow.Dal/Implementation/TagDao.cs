using GraphOverflow.Domain;
using Npgsql;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GraphOverflow.Dal.Implementation
{
  public class TagDao : ITagDao
  {
    private readonly string connectionString;

    public TagDao(string connectionString)
    {
      this.connectionString = connectionString;
    }
    
    public async Task<int> Add(Tag tag)
    {
      using var conn = new NpgsqlConnection(this.connectionString);
      await conn.OpenAsync();
      string sql = "INSERT INTO tag (name) VALUES (@name) RETURNING id";
      using var cmd = new NpgsqlCommand(sql, conn);
      cmd.Parameters.AddWithValue("name", tag.Name);
      int res = (int) await cmd.ExecuteScalarAsync();
      return res;
    }

    public async Task<IEnumerable<Tag>> FindAll()
    {
      IList<Tag> tags = new List<Tag>();
      string sql = "select id, name from tag";
      await using (var conn = new NpgsqlConnection(this.connectionString))
      {
        await conn.OpenAsync();
        await using (var cmd = new NpgsqlCommand(sql, conn))
        await using (NpgsqlDataReader reader = await cmd.ExecuteReaderAsync())
        {
          while (await reader.ReadAsync())
          {
            var id = (int)reader["id"];
            var name = (string)reader["name"];
            tags.Add(new Tag { Id = id, Name = name });
          }
        }
      }
      return tags;
    }

    public async Task<IEnumerable<Tag>> FindByAnswer(int answerId)
    {
      IList<Tag> tags = new List<Tag>();
      string sql = "select t.id, t.name " +
        "from tag_answer " +
        "inner join tag t on tag_answer.tag_id = t.id " +
        "where tag_answer.answer_id = @id";
      await using (var conn = new NpgsqlConnection(this.connectionString))
      {
        await conn.OpenAsync();
        await using (var cmd = new NpgsqlCommand(sql, conn))
        {
          cmd.Parameters.AddWithValue("id", answerId);
          await using (NpgsqlDataReader reader = await cmd.ExecuteReaderAsync())
          {
            while (await reader.ReadAsync())
            {
              var id = (int)reader["id"];
              var name = (string)reader["name"];
              tags.Add(new Tag { Id = id, Name = name });
            }
          }
        }
      }
      return tags;
    }

    public async Task<Tag> FindById(int id)
    {
      Tag tag = null;
      string sql = "select id, name from tag where id = @id";
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
              var tagId = (int)reader["id"];
              var tagName = (string)reader["name"];
              tag = new Tag { Id = tagId, Name = tagName };
            }
          }
        }
      }
      return tag;
    }

    public async Task<IEnumerable<Tag>> FindByPartialName(string tagName)
    {
      IList<Tag> tags = new List<Tag>();
      string sql = $"select id, name from tag where name LIKE '%{tagName}%'";
      await using (var conn = new NpgsqlConnection(this.connectionString))
      {
        await conn.OpenAsync();
        await using (var cmd = new NpgsqlCommand(sql, conn))
        {
          await using (NpgsqlDataReader reader = await cmd.ExecuteReaderAsync())
          {
            while (await reader.ReadAsync())
            {
              var id = (int)reader["id"];
              var name = (string)reader["name"];
              tags.Add(new Tag { Id = id, Name = name });
            }
          }
        }
      }
      return tags;
    }
  }
}
