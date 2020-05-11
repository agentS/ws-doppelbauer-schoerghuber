using System;
using System.Linq;
using GraphOverflow.Services;
using GraphOverflow.WebService.Constants;
using GraphOverflow.WebService.GraphQl.Extensions;
using System.Threading.Tasks;
using GraphOverflow.WebService.GraphQl.GqlSchema.OutputGraphTypes;
using GraphQL.Types;

namespace GraphOverflow.WebService.GraphQl.GqlSchema.RootGraphTypes
{
  public class QueryType : ObjectGraphType
  {
    #region Members
    private readonly ITagService tagService;
    private readonly IQuestionService questionService;
    private readonly IAnswerService answerService;
    private readonly IUserService userService;
    #endregion Members

    #region Construction
    public QueryType(ITagService tagService, IQuestionService questionService, IAnswerService answerService, IUserService userService)
    {
      this.tagService = tagService;
      this.questionService = questionService;
      this.answerService = answerService;
      this.userService = userService;
      InitializeTypeName();
      InitializeFields();
    }

    private void InitializeTypeName()
    {
      Name = "Query";
    }

    private void InitializeFields()
    {
      var meField = Field<UserGraphType>(name: "me", resolve: ResolveUser);
      meField.RequirePermission(UserPermissionConstants.USER_PERMISSION);

      Field<NonNullGraphType<ListGraphType<NonNullGraphType<TagType>>>>(
        name: "allTags", resolve: ResolveAllTags
      );

      Field<NonNullGraphType<ListGraphType<NonNullGraphType<TagType>>>>(
        name: "tags",
        resolve: ResolveTags,
        arguments: new QueryArguments(new QueryArgument<StringGraphType>() 
          { 
            Name = "tagName",
            DefaultValue = string.Empty 
          }
        )
      ).Description = "get all tags that match the %tagName%";

      Field<NonNullGraphType<ListGraphType<NonNullGraphType<QuestionType>>>>(
        name: "latestQuestions",
        resolve: ResolveLatestQuestions,
        arguments: new QueryArguments(new QueryArgument<IntGraphType>
        {
          Name = "userId",
          DefaultValue = (-1),
          Description = "ID of the user to fetch questions for"
        })
      ).Description = "load all questions";

      Field<NonNullGraphType<QuestionType>>(
        name: "question",
        arguments: new QueryArguments(
          new QueryArgument<IntGraphType>
          {
            Name = "id",
            Description = "Question ID"
          }
        ),
        resolve: ResolveQuestion
      ).Description = "load a question";
    }
    #endregion Construction

    #region Resolvers

    public async Task<object> ResolveUser(IResolveFieldContext<object> context)
    {
      GraphQlUserContext userContext = context.UserContext as GraphQlUserContext;
      var userId = userContext.User.Id;
      return await userService.FindUserById(userId);
    }

    public object ResolveAllTags(IResolveFieldContext<object> context)
    {
      return tagService.FindAllTags();
    }

    private object ResolveTags(IResolveFieldContext<object> context)
    {
      var tagName = context.Arguments["tagName"] as string;
      return tagService.FindAllTagsByName(tagName);
    }

    private async Task<object> ResolveLatestQuestions(IResolveFieldContext<object> context)
    {
      int userId = (int) context.Arguments["userId"];
      if (userId != (-1))
      {
        return await questionService.FindLatestQuestions();
      }
      else
      {
        return await questionService.FindLatestQuestionsByUserId(userId);
      }
    }

    private async Task<object> ResolveQuestion(IResolveFieldContext<object> context)
    {
      int questionId = (int)context.Arguments["id"];
      var question = await questionService.FindQuestionById(questionId);
      return question;
    }
    #endregion Resolvers
  }
}
