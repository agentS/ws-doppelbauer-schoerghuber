using System.Threading.Tasks;
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
    #region Members
    private readonly ITagService tagService;
    private readonly IQuestionService questionService;
    private readonly IAuthenticationService authenticationService;
    #endregion Members

    #region Construction
    public MutationType(
      ITagService tagService,
      IQuestionService questionService,
      IAuthenticationService authenticationService
    )
    {
      this.tagService = tagService;
      this.questionService = questionService;
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
      addTagField.RequirePermission("USER");
      addTagField.Description = "adds a graphoverflow tag";

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
