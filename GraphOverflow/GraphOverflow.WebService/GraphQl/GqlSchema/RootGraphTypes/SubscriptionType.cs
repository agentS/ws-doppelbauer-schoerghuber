using GraphOverflow.Dtos;
using GraphOverflow.Services;
using GraphOverflow.WebService.GraphQl.GqlSchema.OutputGraphTypes;
using GraphQL.Resolvers;
using GraphQL.Subscription;
using GraphQL.Types;
using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;

namespace GraphOverflow.WebService.GraphQl.GqlSchema.RootGraphTypes
{
  /// <summary>
  /// reference implementation:
  /// https://github.com/graphql-dotnet/graphql-dotnet/blob/master/src/GraphQL.Tests/Subscription/SubscriptionSchema.cs
  /// </summary>
  public class SubscriptionType : ObjectGraphType
  {
    #region Members
    private readonly ICommentService commentService;
    private readonly IAnswerService answerService;
    #endregion Members

    #region Construction
    public SubscriptionType(IAnswerService answerService, ICommentService commentService)
    {
      this.answerService = answerService;
      this.commentService = commentService;
      InitializeTypeName();
      InitializeFields();
    }

    private void InitializeTypeName()
    {
      Name = "Subscription";
    }

    private void InitializeFields()
    {
      AddField(new EventStreamFieldType
      {
        Name = "answerAdded",
        Type = typeof(AnswerType),
        Arguments = new QueryArguments(
          new QueryArgument<NonNullGraphType<IntGraphType>>
          {
            Name = "questionId",
            Description = "ID of the question to listen to new answers for"
          }
        ),
        Resolver = new FuncFieldResolver<AnswerDto>(ResolveAnswerAdded),
        Subscriber = new EventStreamResolver<AnswerDto>(SubscribeForNewAnswers)
      });
      AddField(new EventStreamFieldType
      {
        Name = "commentAddedToAnswerOfQuestion",
        Type = typeof(CommentType),
        Arguments = new QueryArguments(
          new QueryArgument<NonNullGraphType<IntGraphType>>
          {
            Name = "questionId",
            Description = "ID of the question to listen to new comments to answers for"
          }
        ),
        Resolver = new FuncFieldResolver<CommentDto>(ResolveCommentAdded),
        Subscriber = new EventStreamResolver<CommentDto>(SubscribeForNewCommentsToAnswersOfQuestion)
      });
    }

    #endregion Construction

    #region Resolvers
    private AnswerDto ResolveAnswerAdded(IResolveFieldContext context)
    {
      var answer = context.Source as AnswerDto;
      return answer;
    }
    
    private CommentDto ResolveCommentAdded(IResolveFieldContext context)
    {
      var comment = context.Source as CommentDto;
      return comment;
    }
    #endregion Resolvers

    #region Subscription
    private IObservable<AnswerDto> SubscribeForNewAnswers(IResolveFieldContext context)
    {
      int questionId = (int)context.Arguments["questionId"];
      return answerService.Answers()
        .Where(answer => answer.QuestionId == questionId);
    }

    private IObservable<CommentDto> SubscribeForNewCommentsToAnswersOfQuestion(IResolveFieldContext context)
    {
      int questionId = (int)context.Arguments["questionId"];
      return commentService.Comments()
        .Where(comment => comment.QuestionId == questionId);
    }
    #endregion Subscription
  }
}
