using GraphOverflow.Domain;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphOverflow.Dal.Implementation
{
  public class AnswerDao : IAnswerDao
  {
    private readonly string connectionString;

    public AnswerDao(string connectionString)
    {
      this.connectionString = connectionString;
    }

    public async Task<IEnumerable<Answer>> FindQuestionsByTagId(int tagId)
    {
      IList<Answer> tags = new List<Answer>();
      string sql = "select a.id, a.title, a.content, a.created_at, a.question_id, a.up_votes " +
        "from tag " +
        "inner join tag_answer ta on tag.id = ta.tag_id " +
        "inner join answer a on ta.answer_id = a.id " +
        "where tag.id = @id and a.question_id IS NULL";
      await using (var conn = new NpgsqlConnection(this.connectionString))
      {
        await conn.OpenAsync();
        await using (var cmd = new NpgsqlCommand(sql, conn))
        {
          cmd.Parameters.AddWithValue("id", tagId);
          await using (NpgsqlDataReader reader = await cmd.ExecuteReaderAsync())
          {
            while (await reader.ReadAsync())
            {
              var id = (int)reader["id"];
              var title = (string)reader["title"];
              var content = (string)reader["content"];
              var createdAt = (DateTime)reader["created_at"];
              var upVotes = (int)reader["up_votes"];
              tags.Add(new Answer
              {
                Id = id,
                Title = title,
                Content = content,
                CreatedAt = createdAt,
                UpVoats = upVotes
              });
            }
          }
        }
      }
      return tags;
    }

    public async Task<IEnumerable<Answer>> FindAnswersByQuestionId(int questionId)
    {
      IList<Answer> answers = new List<Answer>();
      string sql = "select id, content, question_id, created_at, up_votes from answer where question_id = @questId";
      await using (var conn = new NpgsqlConnection(this.connectionString))
      {
        await conn.OpenAsync();
        await using (var cmd = new NpgsqlCommand(sql, conn))
        {
          cmd.Parameters.AddWithValue("questId", questionId);
          await using (NpgsqlDataReader reader = await cmd.ExecuteReaderAsync())
          {
            while (await reader.ReadAsync())
            {
              var id = (int)reader["id"];
              var content = (string)reader["content"];
              var createdAt = (DateTime)reader["created_at"];
              var upVotes = (int)reader["up_votes"];
              var questId = (int)reader["question_id"];
              answers.Add(new Answer
              {
                Id = id,
                Content = content,
                CreatedAt = createdAt,
                UpVoats = upVotes,
                QuestionId = questId
              });
            }
          }
        }
      }
      return answers;
    }

    public async Task<Answer> FindAnswerById(int answerId)
    {
      IList<Answer> answers = new List<Answer>();
      string sql = "select id, content, question_id, created_at, up_votes from answer where id = @id";
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
              var content = (string)reader["content"];
              var createdAt = (DateTime)reader["created_at"];
              var upVotes = (int)reader["up_votes"];
              var questId = (int)reader["question_id"];
              answers.Add(new Answer
              {
                Id = id,
                Content = content,
                CreatedAt = createdAt,
                UpVoats = upVotes,
                QuestionId = questId,
              });
            }
          }
        }
      }
      return answers.FirstOrDefault();
    }

    public async Task<Answer> FindQuestionById(int questionId)
    {
      IList<Answer> answers = new List<Answer>();
      string sql = "select id, title, content, question_id, created_at, up_votes from answer where id = @id";
      await using (var conn = new NpgsqlConnection(this.connectionString))
      {
        await conn.OpenAsync();
        await using (var cmd = new NpgsqlCommand(sql, conn))
        {
          cmd.Parameters.AddWithValue("id", questionId);
          await using (NpgsqlDataReader reader = await cmd.ExecuteReaderAsync())
          {
            while (await reader.ReadAsync())
            {
              var id = (int)reader["id"];
              var content = (string)reader["content"];
              var title = (string)reader["title"];
              var createdAt = (DateTime)reader["created_at"];
              var upVotes = (int)reader["up_votes"];
              answers.Add(new Answer
              {
                Id = id,
                Content = content,
                CreatedAt = createdAt,
                UpVoats = upVotes,
                Title = title
              });
            }
          }
        }
      }
      return answers.FirstOrDefault();
    }

    public async Task<int> CreateQuestion(Answer question)
    {
      const string STATEMENT = @"
        INSERT INTO answer(title, content, created_at, up_votes)
        VALUES (@title, @content, @created_at, @up_votes)
        RETURNING id
      ";
      using (var connection = new NpgsqlConnection(this.connectionString))
      {
        await connection.OpenAsync();
        using (var command = new NpgsqlCommand(STATEMENT, connection))
        {
          command.Parameters.AddWithValue("title", question.Title);
          command.Parameters.AddWithValue("content", question.Content);
          command.Parameters.AddWithValue("created_at", DateTime.Now);
          command.Parameters.AddWithValue("up_votes", 0);

          int id = ((int) (await command.ExecuteScalarAsync()));
          return id;
        }
      }
    }
  }
}
