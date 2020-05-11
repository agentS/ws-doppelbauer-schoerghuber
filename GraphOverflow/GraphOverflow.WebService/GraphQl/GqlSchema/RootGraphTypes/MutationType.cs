using System;
using System.Threading.Tasks;
using GraphOverflow.Dtos;
using GraphOverflow.Dtos.Input;
using GraphOverflow.GraphQl.InputGraphTypes;
using GraphOverflow.Services;
using GraphOverflow.WebService.Constants;
using GraphOverflow.WebService.GraphQl.Extensions;
using GraphOverflow.WebService.GraphQl.GqlSchema.InputGraphTypes;
using GraphOverflow.WebService.GraphQl.GqlSchema.OutputGraphTypes;
using GraphQL;
using GraphQL.Types;

namespace GraphOverflow.WebService.GraphQl.GqlSchema.RootGraphTypes
{
  public class MutationType : ObjectGraphType
  {
    #region Members
    private readonly ITagService tagService;
    private readonly IQuestionService questionService;
    private readonly IAnswerService answerService;
    private readonly IAuthenticationService authenticationService;
    private readonly ICommentService commentService;
    #endregion Members

    #region Construction
    public MutationType(
      ITagService tagService,
      IQuestionService questionService,
      IAnswerService answerService,
      IAuthenticationService authenticationService,
      ICommentService commentService
    )
    {
      this.tagService = tagService;
      this.questionService = questionService;
      this.answerService = answerService;
      this.authenticationService = authenticationService;
      this.commentService = commentService;
      InitializeTypeName();
      InitializeFields();
    }

    private void InitializeTypeName()
    {
      Name = "Mutation";
    }

    private void InitializeFields()
    {
      var addTagField = Field<TagType>(
        name: "addTag",
        arguments: new QueryArguments(
          new QueryArgument<NonNullGraphType<StringGraphType>>{ Name = "tagName" }),
        resolve: ResolveAddTag
      );
      addTagField.RequirePermission(UserPermissionConstants.USER_PERMISSION);
      addTagField.Description = "adds a graphoverflow tag";

      var upVoteQuestionField = Field<QuestionType>(
        name: "upVoteQuestion",
        arguments: new QueryArguments(
          new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "questionId" }),
        resolve: ResolveUpvoteQuestion
      );
      upVoteQuestionField.RequirePermission(UserPermissionConstants.USER_PERMISSION);
      upVoteQuestionField.Description = "upVote question";

      var upVoteAnswerField = Field<AnswerType>(
        name: "upVoteAnswer",
        arguments: new QueryArguments(
          new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "answerId" }),
        resolve: ResolveUpvoteAnswer
      );
      upVoteAnswerField.RequirePermission(UserPermissionConstants.USER_PERMISSION);
      upVoteAnswerField.Description = "upVote answer";

      var addQuestionField = Field<QuestionType>(
        name: "addQuestion",
        arguments: new QueryArguments(
          new QueryArgument<NonNullGraphType<QuestionInputGraphType>> { Name = "question" }
        ),
        resolve: ResolveAddQuestion
      );
      addQuestionField.RequirePermission(UserPermissionConstants.USER_PERMISSION);
      addQuestionField.Description = "adds a question";

      var answerQuestionField = Field<AnswerType>(
        name: "answerQuestion",
        arguments: new QueryArguments(
          new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "questionId" },
          new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "content" }
        ),
        resolve: ResolveAddAnswer
      );
      answerQuestionField.RequirePermission(UserPermissionConstants.USER_PERMISSION);
      answerQuestionField.Description = "adds an answer";

      var commentAnswerField = Field<CommentType>(
        name: "commentAnswer",
        arguments: new QueryArguments(
          new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "answerId" },
          new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "content" }
        ),
        resolve: ResolveAddComment
      );
      commentAnswerField.RequirePermission(UserPermissionConstants.USER_PERMISSION);
      commentAnswerField.Description = "adds a comment";

      Field<AuthPayloadGraphType>(
       name: "login",
       arguments: new QueryArguments(
         new QueryArgument<NonNullGraphType<UserLoginInputGraphType>> { Name = "loginData" }
       ),
       resolve: ResolveUserLogin
     ).Description = "user login";
    }
    #endregion Construction

    #region Resolvers
    public object ResolveAddTag(IResolveFieldContext<object> context)
    {
      var tagName = (string)context.Arguments["tagName"];
      var createdTag = tagService.AddTag(tagName);
      return createdTag;
    }

    private async Task<object> ResolveUpvoteQuestion(IResolveFieldContext<object> context)
    {
      GraphQlUserContext userContext = context.UserContext as GraphQlUserContext;
      int questionId = context.GetArgument<int>("questionId");
      QuestionDto updatedQuestion = await questionService.UpvoteQuestion(questionId, userContext.User.Id);
      return updatedQuestion;
    }

    private async Task<object> ResolveUpvoteAnswer(IResolveFieldContext<object> context)
    {
      GraphQlUserContext userContext = context.UserContext as GraphQlUserContext;
      int answerId = context.GetArgument<int>("answerId");
      AnswerDto updatedQuestion = await answerService.UpvoteAnswer(answerId, userContext.User.Id);
      return updatedQuestion;
    }

    public async Task<object> ResolveAddQuestion(IResolveFieldContext<object> context)
    {
      GraphQlUserContext userContext = context.UserContext as GraphQlUserContext;
      QuestionInputDto question = context.GetArgument<QuestionInputDto>("question");
      QuestionDto createdQuestion = await questionService.CreateQuestion(question, userContext.User.Id);
      return createdQuestion;
    }

    private object ResolveUserLogin(IResolveFieldContext<object> context)
    {
      var userLoginData = context.GetArgument<UserLoginInputDto>("loginData");
      var result = authenticationService.Authenticate(userLoginData);
      return result;
    }

    private async Task<object> ResolveAddAnswer(IResolveFieldContext<object> context)
    {
      GraphQlUserContext userContext = context.UserContext as GraphQlUserContext;
      var questionId = context.GetArgument<int>("questionId");
      var content = context.GetArgument<string>("content");
      AnswerDto answer = await answerService.CreateAnswer(content, questionId, userContext.User.Id);
      return answer;

    }

    private async Task<object> ResolveAddComment(IResolveFieldContext<object> context)
    {
      GraphQlUserContext userContext = context.UserContext as GraphQlUserContext;
      var answerId = context.GetArgument<int>("answerId");
      var content = context.GetArgument<string>("content");
      CommentDto comment = await commentService.CreateComment(content, answerId, userContext.User.Id);
      return comment;
    }
    #endregion Resolvers
  }
}
