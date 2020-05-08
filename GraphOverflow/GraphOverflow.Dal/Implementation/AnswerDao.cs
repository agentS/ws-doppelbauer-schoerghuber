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
      string sql = @"
        select a.id, a.title, a.content, a.created_at, a.question_id,
        count(*) AS up_votes
        from tag
        inner join tag_answer ta on tag.id = ta.tag_id
        inner join answer a on ta.answer_id = a.id
        inner join answer_up_vote auv on a.id = auv.answer_id
        where tag.id = @id and a.question_id IS NULL
        group by a.id, a.title, a.content, a.created_at, a.question_id
        order by up_votes desc;
      "; 

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
              var upVotes = (long)reader["up_votes"];
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
      string sql = @"
        select id, content, question_id, created_at,
        count(*) AS up_votes
        from answer
        inner join answer_up_vote auv on answer.id = auv.answer_id
        where question_id = @questId
        group by id, content, question_id, created_at
        order by up_votes desc;
      ";
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
              var upVotes = (long)reader["up_votes"];
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
      string sql = "select id, content, question_id, created_at, " +
        "(select count(*) from answer_up_vote where answer_id = @id) AS up_votes " +
        "from answer where id = @id";
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
              var upVotes = (long)reader["up_votes"];
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
      string sql = "select id, title, content, question_id, created_at, " +
        "(select count(*) from answer_up_vote where answer_id = @id) AS up_votes " +
        "from answer where id = @id";
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
              var upVotes = (long)reader["up_votes"];
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
        VALUES (@title, @content, @created_at)
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

          int id = ((int) (await command.ExecuteScalarAsync()));
          return id;
        }
      }
    }

    public async Task<bool> Update(Answer question)
    {
      const string STATEMENT = @"
        UPDATE answer
        SET title = @title, content = @content
        WHERE id = @id
      ";
      using (var connection = new NpgsqlConnection(this.connectionString))
      {
        await connection.OpenAsync();
        using (var command = new NpgsqlCommand(STATEMENT, connection))
        {
          command.Parameters.AddWithValue("title", (object)question.Title ?? DBNull.Value);
          command.Parameters.AddWithValue("content", question.Content);
          command.Parameters.AddWithValue("id", question.Id);

          int res = await command.ExecuteNonQueryAsync();
          return res > 0;
        }
      }
    }

    public async Task<bool> AddUpVoat(Answer question, User user)
    {
      const string STATEMENT = @"
        INSERT INTO answer_up_vote(user_id, answer_id)
        VALUES (@userId, @answerId)
      ";
      using (var connection = new NpgsqlConnection(this.connectionString))
      {
        await connection.OpenAsync();
        using (var command = new NpgsqlCommand(STATEMENT, connection))
        {
          command.Parameters.AddWithValue("userId", user.Id);
          command.Parameters.AddWithValue("answerId", question.Id);

          int res = await command.ExecuteNonQueryAsync();
          return res > 0;
        }
      }
    }
  }
}
