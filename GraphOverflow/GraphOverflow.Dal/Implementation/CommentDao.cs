using GraphOverflow.Domain;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GraphOverflow.Dal.Implementation
{
  public class CommentDao : ICommentDao
  {
    private readonly string connectionString;

    public CommentDao(string connectionString)
    {
      this.connectionString = connectionString;
    }
    
    public async Task<IEnumerable<Comment>> FindCommentsByAnswerId(int answerId)
    {
      IList<Comment> comments = new List<Comment>();
      string sql = "select c.id, c.content, c.created_at, c.answer_id, c.user_id " +
        "from answer " +
        "inner join comment c on answer.id = c.answer_id " +
        "where answer.id = @answId";
      await using (var conn = new NpgsqlConnection(this.connectionString))
      {
        await conn.OpenAsync();
        await using (var cmd = new NpgsqlCommand(sql, conn))
        {
          cmd.Parameters.AddWithValue("answId", answerId);
          await using (NpgsqlDataReader reader = await cmd.ExecuteReaderAsync())
          {
            while (await reader.ReadAsync())
            {
              var id = (int)reader["id"];
              var userId = (int)reader["user_id"];
              var content = (string)reader["content"];
              var createdAt = (DateTime)reader["created_at"];
              var questId = (int)reader["answer_id"];
              comments.Add(new Comment
              {
                Id = id,
                Content = content,
                CreatedAt = createdAt,
                AnswerId = questId,
                UserId = userId
              });
            }
          }
        }
      }
      return comments;
    }
  }
}
