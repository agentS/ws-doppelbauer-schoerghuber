using GraphOverflow.Domain;
using Npgsql;
using System;
using System.Collections.Generic;

namespace GraphOverflow.Dal.Implementation
{
  public class TagDao : ITagDao
  {
    private const string CONNECTION_STRING = 
      "Host=localhost;Username=postgres;Password=postgres;Database=graphoverflow";
    public int Add(Tag tag)
    {
      using var conn = new NpgsqlConnection(CONNECTION_STRING);
      conn.Open();
      string sql = "INSERT INTO tag (name) VALUES (@name) RETURNING id";
      using var cmd = new NpgsqlCommand(sql, conn);
      cmd.Parameters.AddWithValue("name", tag.Name);
      int res = (int)cmd.ExecuteScalar();
      return res;
    }

    public IEnumerable<Tag> FindAll()
    {
      IList<Tag> tags = new List<Tag>();
      string sql = "select id, name from tag";
      using (var conn = new NpgsqlConnection(CONNECTION_STRING))
      {
        conn.Open();
        using (var cmd = new NpgsqlCommand(sql, conn))
        using (NpgsqlDataReader reader = cmd.ExecuteReader())
        {
          while (reader.Read())
          {
            var id = (int)reader["id"];
            var name = (string)reader["name"];
            tags.Add(new Tag { Id = id, Name = name });
          }
        }
      }
      return tags;
    }

    public IEnumerable<Tag> FindByAnswer(Answer answer)
    {
      throw new NotImplementedException();
    }

    public Tag FindById(int id)
    {
      Tag tag = null;
      string sql = "select id, name from tag where id = @id";
      using (var conn = new NpgsqlConnection(CONNECTION_STRING))
      {
        conn.Open();
        using (var cmd = new NpgsqlCommand(sql, conn))
        {
          cmd.Parameters.AddWithValue("id", id);
          using (NpgsqlDataReader reader = cmd.ExecuteReader())
          {
            if (reader.Read())
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

    public IEnumerable<Tag> FindByName(string tagName)
    {
      IList<Tag> tags = new List<Tag>();
      string sql = "select id, name from tag where name LIKE '%@name%'";
      using (var conn = new NpgsqlConnection(CONNECTION_STRING))
      {
        conn.Open();
        using (var cmd = new NpgsqlCommand(sql, conn))
        {
          cmd.Parameters.AddWithValue("name", tagName);
          using (NpgsqlDataReader reader = cmd.ExecuteReader())
          {
            while (reader.Read())
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
