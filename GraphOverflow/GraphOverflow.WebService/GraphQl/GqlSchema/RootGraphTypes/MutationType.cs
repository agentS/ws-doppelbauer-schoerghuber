﻿using System.Threading.Tasks;
using GraphOverflow.Dtos;
using GraphOverflow.Dtos.Input;
using GraphOverflow.GraphQl.InputGraphTypes;
using GraphOverflow.Services;
using GraphOverflow.WebService.GraphQl.Extensions;
using GraphOverflow.WebService.GraphQl.GqlSchema.InputGraphTypes;
using GraphOverflow.WebService.GraphQl.GqlSchema.OutputGraphTypes;
using GraphQL;
using GraphQL.Types;

namespace GraphOverflow.WebService.GraphQl.GqlSchema.RootGraphTypes
{
  public class MutationType : ObjectGraphType
  {
    #region Constants
    private const string USER_PERMISSION = "USER";
    #endregion Constants

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
      addTagField.RequirePermission(USER_PERMISSION);
      addTagField.Description = "adds a graphoverflow tag";

      var upVoatQuestionField = Field<QuestionType>(
        name: "upVoatQuestion",
        arguments: new QueryArguments(
          new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "questionId" }),
        resolve: ResolveUpvoatQuestion
      );
      upVoatQuestionField.Description = "upVoat question";

      var upVoatAnswerField = Field<AnswerType>(
        name: "upVoatAnswer",
        arguments: new QueryArguments(
          new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "answerId" }),
        resolve: ResolveUpvoatAnswer
      );
      upVoatAnswerField.Description = "upVoat answer";

      Field<QuestionType>(
        name: "addQuestion",
        arguments: new QueryArguments(
          new QueryArgument<NonNullGraphType<QuestionInputGraphType>> { Name = "question" }
        ),
        resolve: ResolveAddQuestion
      ).Description = "adds a question";

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
      int questionId = context.GetArgument<int>("questionId");
      QuestionDto updatedQuestion = await questionService.UpvoatQuestion(questionId);
      return updatedQuestion;
    }

    private async Task<object> ResolveUpvoatAnswer(IResolveFieldContext<object> context)
    {
      int answerId = context.GetArgument<int>("answerId");
      AnswerDto updatedQuestion = await answerService.UpvoatAnswer(answerId);
      return updatedQuestion;
    }

    public async Task<object> ResolveAddQuestion(IResolveFieldContext<object> context)
    {
      QuestionInputDto question = context.GetArgument<QuestionInputDto>("question");
      QuestionDto createdQuestion = await questionService.CreateQuestion(question);
      return createdQuestion;
    }

    private object ResolveUserLogin(IResolveFieldContext<object> context)
    {
      var userLoginData = context.GetArgument<UserLoginInputDto>("loginData");
      var result = authenticationService.Authenticate(userLoginData);
      return result;
    }
    #endregion Resolvers
  }
}
