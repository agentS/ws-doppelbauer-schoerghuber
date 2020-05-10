using GraphOverflow.Dtos;
using GraphOverflow.Services;
using GraphOverflow.WebService.GraphQl.GqlSchema.InterfaceGraphTypes;
using GraphQL.Types;
using System;

namespace GraphOverflow.WebService.GraphQl.GqlSchema.OutputGraphTypes
{
  public class QuestionType : ObjectGraphType<QuestionDto>
  {
    #region Members
    private readonly ITagService tagService;
    private readonly IAnswerService answerService;
    private readonly IUserService userService;
    #endregion Members

    #region Construction
    public QuestionType(ITagService tagService,
      IAnswerService answerService,
      IUserService userService)
    {
      this.tagService = tagService;
      this.answerService = answerService;
      this.userService = userService;

      InitializeName();
      InitializeFields();
    }

    private void InitializeName()
    {
      Name = "Question";
    }

    private void InitializeFields()
    {
      Field<NonNullGraphType<IdGraphType>>("id");
      Field<NonNullGraphType<StringGraphType>>("content");
      Field<NonNullGraphType<DateTimeGraphType>>("createdAt");
      Field<NonNullGraphType<StringGraphType>>("title");
      Field<NonNullGraphType<LongGraphType>>("upVotes");
      Field<NonNullGraphType<ListGraphType<NonNullGraphType<UserGraphType>>>>("upVoteUsers");
      Field<NonNullGraphType<ListGraphType<NonNullGraphType<TagType>>>>(
        name: "tags", resolve: ResolveTags);
      Field<NonNullGraphType<ListGraphType<NonNullGraphType<AnswerType>>>>(
        name: "answers", resolve: ResolveAnswers);
      Field<NonNullGraphType<UserGraphType>>(
        name: "user", resolve: ResolveUser);

      Interface<PostInterface>();
    }
    #endregion Construction

    #region Resolvers
    private object ResolveTags(IResolveFieldContext<QuestionDto> context)
    {
      var question = context.Source;
      return tagService.FindByQuestion(question);
    }

    private object ResolveAnswers(IResolveFieldContext<QuestionDto> context)
    {
      var question = context.Source;
      return answerService.FindAnswersForQuestion(question);

    }

    private object ResolveUser(IResolveFieldContext<QuestionDto> context)
    {
      var question = context.Source;
      return userService.FindUserById(question.UserId);
    }
    #endregion Resolvers
  }
}
