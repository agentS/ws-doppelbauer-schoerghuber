using System.Threading.Tasks;
using GraphOverflow.Dtos;
using GraphOverflow.Dtos.Input;
using GraphOverflow.GraphQl.InputGraphTypes;
using GraphOverflow.Services;
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
    #endregion Members

    #region Construction
    public MutationType(
      ITagService tagService,
      IQuestionService questionService
    )
    {
      this.tagService = tagService;
      this.questionService = questionService;
      InitializeTypeName();
      InitializeFields();
    }

    private void InitializeTypeName()
    {
      Name = "Mutation";
    }

    private void InitializeFields()
    {
      Field<TagType>(
        name: "addTag",
        arguments: new QueryArguments(
          new QueryArgument<NonNullGraphType<StringGraphType>>{ Name = "tagName" }),
        resolve: ResolveAddTag
      ).Description = "adds a graphoverflow tag";

      Field<QuestionType>(
        name: "addQuestion",
        arguments: new QueryArguments(
          new QueryArgument<NonNullGraphType<QuestionInputGraphType>> {Name = "question"}
        ),
        resolve: ResolveAddQuestion
      ).Description = "adds a question";
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
    #endregion Resolvers
  }
}
