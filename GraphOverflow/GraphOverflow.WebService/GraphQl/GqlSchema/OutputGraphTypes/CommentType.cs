using GraphOverflow.Dtos;
using GraphOverflow.Services;
using GraphOverflow.WebService.GraphQl.GqlSchema.InterfaceGraphTypes;
using GraphQL.Types;

namespace GraphOverflow.WebService.GraphQl.GqlSchema.OutputGraphTypes
{
  public class CommentType : ObjectGraphType<CommentDto>
  {
    #region Members
    private readonly IAnswerService answerService;
    private readonly IUserService userService;
    #endregion Members

    #region Construction
    public CommentType(IAnswerService answerService, IUserService userService)
    {
      this.answerService = answerService;
      this.userService = userService;
      InitializeName();
      InitializeFields();
    }

    private void InitializeName()
    {
      Name = "Comment";
    }

    private void InitializeFields()
    {
      Field<NonNullGraphType<IdGraphType>>("id");
      Field<NonNullGraphType<StringGraphType>>("content");
      Field<NonNullGraphType<DateTimeGraphType>>("createdAt");
      Field<NonNullGraphType<IntGraphType>>("upVotes");
      Field<NonNullGraphType<AnswerType>>(
        name: "answer", resolve: ResolveAnswer);
      Field<NonNullGraphType<UserGraphType>>(
        name: "user", resolve: ResolveUser);

      Interface<PostInterface>();
    }
    #endregion Construction

    #region Resolvers
    private object ResolveAnswer(IResolveFieldContext<CommentDto> context)
    {
      var comment = context.Source;
      return answerService.FindAnswerForComment(comment);
    }

    private object ResolveUser(IResolveFieldContext<CommentDto> context)
    {
      var comment = context.Source;
      return userService.FindUserById(comment.UserId);
    }
    #endregion Resolvers
  }
}
