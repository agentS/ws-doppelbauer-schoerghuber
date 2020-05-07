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
    public int AddTag(Tag tag)
    {
      using var conn = new NpgsqlConnection(CONNECTION_STRING);
      conn.Open();
      string sql = "INSERT INTO tag (name) VALUES (@name) RETURNING id";
      using var cmd = new NpgsqlCommand(sql, conn);
      cmd.Parameters.AddWithValue("name", tag.Name);
      //int res = cmd.ExecuteNonQuery();
      int res = (int)cmd.ExecuteScalar();
      return res;
    }

    public IEnumerable<Tag> FindAll()
    {
      throw new NotImplementedException();
    }

    public IEnumerable<Tag> FindByAnswer(Answer answer)
    {
      throw new NotImplementedException();
    }

    public Tag FindById(int id)
    {
      throw new NotImplementedException();
    }

    public IEnumerable<Tag> FindByName(string tagName)
    {
      throw new NotImplementedException();
    }
  }
}
