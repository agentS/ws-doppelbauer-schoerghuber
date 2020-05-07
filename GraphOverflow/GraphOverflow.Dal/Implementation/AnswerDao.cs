using GraphOverflow.Domain;
using Npgsql;
using System;
using System.Collections.Generic;

namespace GraphOverflow.Dal.Implementation
{
  public class AnswerDao : IAnswerDao
  {
    private const string CONNECTION_STRING =
      "Host=localhost;Username=postgres;Password=postgres;Database=graphoverflow";

    public IEnumerable<Answer> FindQuestionsByTagId(int tagId)
    {
      IList<Answer> tags = new List<Answer>();
      string sql = "select a.id, a.title, a.content, a.created_at, a.question_id, a.up_votes " +
        "from tag " +
        "inner join tag_answer ta on tag.id = ta.tag_id " +
        "inner join answer a on ta.answer_id = a.id " +
        "where tag.id = @id and a.question_id IS NULL";
      using (var conn = new NpgsqlConnection(CONNECTION_STRING))
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
  }
}
