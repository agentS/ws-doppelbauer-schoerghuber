using GraphOverflow.Domain;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
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

    public async Task<int> CreateComment(string content, int answerId, int userId)
    {
      const string STATEMENT = @"
        INSERT INTO comment(content, created_at, answer_id, user_id)
        VALUES (@content, @created_at, @answer_id, @user_id)
        RETURNING id
      ";
      using (var connection = new NpgsqlConnection(this.connectionString))
      {
        await connection.OpenAsync();
        using (var command = new NpgsqlCommand(STATEMENT, connection))
        {
          command.Parameters.AddWithValue("content", content);
          command.Parameters.AddWithValue("created_at", DateTime.Now);
          command.Parameters.AddWithValue("user_id", userId);
          command.Parameters.AddWithValue("answer_id", answerId);

          int id = ((int)(await command.ExecuteScalarAsync()));
          return id;
        }
      }
    }

    public async Task<IEnumerable<Comment>> FindCommentsByAnswerId(int answerId)
    {
      IList<Comment> comments = new List<Comment>();
      string sql = @"
        SELECT c.id, c.content, c.created_at, c.answer_id, c.user_id, a.question_id 
        FROM answer AS a
        INNER JOIN comment c on a.id = c.answer_id
        WHERE a.id = @answId
      ";
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
              var answerIdResult = (int)reader["answer_id"];
              var questionId = (int)reader["question_id"];
              comments.Add(new Comment
              {
                Id = id,
                Content = content,
                CreatedAt = createdAt,
                AnswerId = answerIdResult,
                UserId = userId,
                QuestionId = questionId
              });
            }
          }
        }
      }
      return comments;
    }

    public async Task<Comment> FindCommentsById(int commentId)
    {
      IList<Comment> comments = new List<Comment>();
      string sql = @"
        SELECT c.id, c.answer_id, c.created_at, c.content, c.user_id, a.question_id
        FROM comment AS c
        INNER JOIN answer a ON c.answer_id = a.id
        WHERE c.id = @id
      ";
      await using (var conn = new NpgsqlConnection(this.connectionString))
      {
        await conn.OpenAsync();
        await using (var cmd = new NpgsqlCommand(sql, conn))
        {
          cmd.Parameters.AddWithValue("id", commentId);
          await using (NpgsqlDataReader reader = await cmd.ExecuteReaderAsync())
          {
            while (await reader.ReadAsync())
            {
              var id = (int)reader["id"];
              var userId = (int)reader["user_id"];
              var content = (string)reader["content"];
              var createdAt = (DateTime)reader["created_at"];
              var answId = (int)reader["answer_id"];
              var questionId = (int)reader["question_id"];
              comments.Add(new Comment
              {
                Id = id,
                Content = content,
                CreatedAt = createdAt,
                UserId = userId,
                AnswerId = answId,
                QuestionId = questionId
              });
            }
          }
        }
      }
      return comments.FirstOrDefault();
    }
  }
}
