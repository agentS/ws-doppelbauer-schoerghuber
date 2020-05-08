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
    #endregion Members

    #region Construction
    public MutationType(
      ITagService tagService,
      IQuestionService questionService,
      IAnswerService answerService,
      IAuthenticationService authenticationService
    )
    {
      this.tagService = tagService;
      this.questionService = questionService;
      this.answerService = answerService;
      this.authenticationService = authenticationService;
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

      var upVoatQuestionField = Field<QuestionType>(
        name: "upVoatQuestion",
        arguments: new QueryArguments(
          new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "questionId" }),
        resolve: ResolveUpvoatQuestion
      );
      upVoatQuestionField.RequirePermission(UserPermissionConstants.USER_PERMISSION);
      upVoatQuestionField.Description = "upVoat question";

      var upVoatAnswerField = Field<AnswerType>(
        name: "upVoatAnswer",
        arguments: new QueryArguments(
          new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "answerId" }),
        resolve: ResolveUpvoatAnswer
      );
      upVoatAnswerField.RequirePermission(UserPermissionConstants.USER_PERMISSION);
      upVoatAnswerField.Description = "upVoat answer";

      var addQuestionField = Field<QuestionType>(
        name: "addQuestion",
        arguments: new QueryArguments(
          new QueryArgument<NonNullGraphType<QuestionInputGraphType>> { Name = "question" }
        ),
        resolve: ResolveAddQuestion
      );
      addQuestionField.RequirePermission(UserPermissionConstants.USER_PERMISSION);
      addQuestionField.Description = "adds a question";

      var addAnswerField = Field<AnswerType>(
        name: "addAnswer",
        arguments: new QueryArguments(
          new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "questionId" },
          new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "content" }
        ),
        resolve: ResolveAddAnswer
      );
      addAnswerField.RequirePermission(UserPermissionConstants.USER_PERMISSION);
      addAnswerField.Description = "adds an answer";

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

    private async Task<object> ResolveUpvoatQuestion(IResolveFieldContext<object> context)
    {
      GraphQlUserContext userContext = context.UserContext as GraphQlUserContext;
      int questionId = context.GetArgument<int>("questionId");
      QuestionDto updatedQuestion = await questionService.UpvoatQuestion(questionId, userContext.User.Id);
      return updatedQuestion;
    }

    private async Task<object> ResolveUpvoatAnswer(IResolveFieldContext<object> context)
    {
      GraphQlUserContext userContext = context.UserContext as GraphQlUserContext;
      int answerId = context.GetArgument<int>("answerId");
      AnswerDto updatedQuestion = await answerService.UpvoatAnswer(answerId, userContext.User.Id);
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
    #endregion Resolvers
  }
}
