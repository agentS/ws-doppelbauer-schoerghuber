using GraphOverflow.Dtos;
using GraphOverflow.Services;
using GraphOverflow.WebService.GraphQl.GqlSchema.InterfaceGraphTypes;
using GraphQL.Types;
using System;

namespace GraphOverflow.WebService.GraphQl.GqlSchema.OutputGraphTypes
{
  public class AnswerType : ObjectGraphType<AnswerDto>
  {
    #region Members

    private readonly IQuestionService questionService;
    private readonly ICommentService commentService;
    private readonly IUpVoteUsersService upVoteUsersService;
    private readonly IUserService userService;
    #endregion Members

    #region Construction
    public AnswerType(
      IQuestionService questionService,
      ICommentService commentService,
      IUpVoteUsersService upVoteUsersService,
      IUserService userService
    )
    {
      this.questionService = questionService;
      this.commentService = commentService;
      this.upVoteUsersService = upVoteUsersService;
      this.userService = userService;
      InitializeName();
      InitializeFields();
    }

    private void InitializeName()
    {
      Name = "Answer";
    }

    private void InitializeFields()
    {
      Field<NonNullGraphType<IdGraphType>>("id");
      Field<NonNullGraphType<StringGraphType>>("content");
      Field<NonNullGraphType<DateTimeGraphType>>("createdAt");
      Field<NonNullGraphType<LongGraphType>>("upVotes");
      Field<NonNullGraphType<ListGraphType<NonNullGraphType<UserGraphType>>>>(
        "upVoteUsers", resolve: ResolveUpVoteUsers
      );
      Field<NonNullGraphType<QuestionType>>(
        name: "question", resolve: ResolveQuestion);
      Field<NonNullGraphType<ListGraphType<NonNullGraphType<CommentType>>>>(
        name: "comments", resolve: ResolveComments);
      Field<NonNullGraphType<UserGraphType>>(
        name: "user", resolve: ResolveUser);

      Interface<PostInterface>();
    }
    #endregion Construction

    #region Resolvers

    private object ResolveUpVoteUsers(IResolveFieldContext<AnswerDto> context)
    {
      var answer = context.Source;
      return upVoteUsersService.FindUpVoteUsersForAnswer(answer);
    }
    
    private object ResolveComments(IResolveFieldContext<AnswerDto> context)
    {
      var answer = context.Source;
      return commentService.FindCommentsForAnswer(answer);
    }

    private object ResolveQuestion(IResolveFieldContext<AnswerDto> context)
    {
      var answer = context.Source;
      return questionService.FindQuestionForAnswer(answer);
    }

    private object ResolveUser(IResolveFieldContext<AnswerDto> context)
    {
      var answer = context.Source;
      return userService.FindUserById(answer.UserId);
    }
    #endregion Resolvers
  }
}
