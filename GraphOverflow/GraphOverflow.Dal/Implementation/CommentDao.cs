using GraphOverflow.Domain;
using Npgsql;
using System;
using System.Collections.Generic;

namespace GraphOverflow.Dal.Implementation
{
  public class CommentDao : ICommentDao
  {
    private const string CONNECTION_STRING =
      "Host=localhost;Username=postgres;Password=postgres;Database=graphoverflow";

    public IEnumerable<Comment> FindCommentsByAnswerId(int answerId)
    {
      IList<Comment> comments = new List<Comment>();
      string sql = "select c.id, c.content, c.created_at, c.answer_id " +
        "from answer " +
        "inner join comment c on answer.id = c.answer_id " +
        "where answer.id = @answId";
      using (var conn = new NpgsqlConnection(CONNECTION_STRING))
      {
        conn.Open();
        using (var cmd = new NpgsqlCommand(sql, conn))
        {
          cmd.Parameters.AddWithValue("answId", answerId);
          using (NpgsqlDataReader reader = cmd.ExecuteReader())
          {
            while (reader.Read())
            {
              var id = (int)reader["id"];
              var content = (string)reader["content"];
              var createdAt = (DateTime)reader["created_at"];
              var questId = (int)reader["answer_id"];
              comments.Add(new Comment
              {
                Id = id,
                Content = content,
                CreatedAt = createdAt,
                AnswerId = questId
              });
            }
          }
        }
      }
      return comments;
    }
  }
}
