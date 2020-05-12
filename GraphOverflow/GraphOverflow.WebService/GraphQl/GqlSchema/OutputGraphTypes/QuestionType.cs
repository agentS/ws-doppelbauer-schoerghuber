using GraphOverflow.Dtos;
using GraphOverflow.Services;
using GraphOverflow.WebService.GraphQl.GqlSchema.InterfaceGraphTypes;
using GraphQL.Types;
using System;
using GraphQL.DataLoader;

namespace GraphOverflow.WebService.GraphQl.GqlSchema.OutputGraphTypes
{
  public class QuestionType : ObjectGraphType<QuestionDto>
  {
    #region Members
    private readonly ITagService tagService;
    private readonly IAnswerService answerService;
    private readonly IUpVoteUsersService upVoteUsersService;
    private readonly IUserService userService;
    private readonly IDataLoaderContextAccessor dataLoaderContextAccessor;
    #endregion Members

    #region Construction
    public QuestionType(
      ITagService tagService,
      IAnswerService answerService,
      IUpVoteUsersService upVoteUsersService,
      IUserService userService,
      IDataLoaderContextAccessor dataLoaderContextAccessor
    )
    {
      this.tagService = tagService;
      this.answerService = answerService;
      this.upVoteUsersService = upVoteUsersService;
      this.userService = userService;
      this.dataLoaderContextAccessor = dataLoaderContextAccessor;

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
      Field<NonNullGraphType<ListGraphType<NonNullGraphType<UserGraphType>>>>(
        "upVoteUsers", resolve: ResolveUpVoteUsers);
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

    private object ResolveUpVoteUsers(IResolveFieldContext<QuestionDto> context)
    {
      var question = context.Source;
      return upVoteUsersService.FindUpVoteUsersForQuestion(question);
    }
    
    private object ResolveTags(IResolveFieldContext<QuestionDto> context)
    {
      var question = context.Source;
      return tagService.FindByQuestion(question);
    }

    private object ResolveAnswers(IResolveFieldContext<QuestionDto> context)
    {
      var question = context.Source;
      var answersLoader = this.dataLoaderContextAccessor.Context
        .GetOrAddCollectionBatchLoader<QuestionDto, AnswerDto>(
          "FindAnswersForQuestions",
          answerService.FindAnswersForQuestions
        );
      return answersLoader.LoadAsync(question);
    }

    private object ResolveUser(IResolveFieldContext<QuestionDto> context)
    {
      var question = context.Source;
      return userService.FindUserById(question.UserId);
    }
    #endregion Resolvers
  }
}
