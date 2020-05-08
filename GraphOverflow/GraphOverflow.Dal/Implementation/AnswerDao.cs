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

    public IEnumerable<Answer> FindQuestionsByTagId(int tagId)
    {
      IList<Answer> tags = new List<Answer>();
      string sql = "select a.id, a.title, a.content, a.created_at, a.question_id, a.up_votes " +
        "from tag " +
        "inner join tag_answer ta on tag.id = ta.tag_id " +
        "inner join answer a on ta.answer_id = a.id " +
        "where tag.id = @id and a.question_id IS NULL";
      using (var conn = new NpgsqlConnection(this.connectionString))
      {
        conn.Open();
        using (var cmd = new NpgsqlCommand(sql, conn))
        {
          cmd.Parameters.AddWithValue("id", tagId);
          using (NpgsqlDataReader reader = cmd.ExecuteReader())
          {
            while (reader.Read())
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

    public IEnumerable<Answer> FindAnswersByQuestionId(int questionId)
    {
      IList<Answer> answers = new List<Answer>();
      string sql = "select id, content, question_id, created_at, up_votes from answer where question_id = @questId";
      using (var conn = new NpgsqlConnection(this.connectionString))
      {
        conn.Open();
        using (var cmd = new NpgsqlCommand(sql, conn))
        {
          cmd.Parameters.AddWithValue("questId", questionId);
          using (NpgsqlDataReader reader = cmd.ExecuteReader())
          {
            while (reader.Read())
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

    public Answer FindAnswerById(int answerId)
    {
      IList<Answer> answers = new List<Answer>();
      string sql = "select id, content, question_id, created_at, up_votes from answer where id = @id";
      using (var conn = new NpgsqlConnection(this.connectionString))
      {
        conn.Open();
        using (var cmd = new NpgsqlCommand(sql, conn))
        {
          cmd.Parameters.AddWithValue("id", answerId);
          using (NpgsqlDataReader reader = cmd.ExecuteReader())
          {
            while (reader.Read())
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

    public Answer FindQuestionById(int questionId)
    {
      IList<Answer> answers = new List<Answer>();
      string sql = "select id, title, content, question_id, created_at, up_votes from answer where id = @id";
      using (var conn = new NpgsqlConnection(this.connectionString))
      {
        conn.Open();
        using (var cmd = new NpgsqlCommand(sql, conn))
        {
          cmd.Parameters.AddWithValue("id", questionId);
          using (NpgsqlDataReader reader = cmd.ExecuteReader())
          {
            while (reader.Read())
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
        connection.Open();
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
