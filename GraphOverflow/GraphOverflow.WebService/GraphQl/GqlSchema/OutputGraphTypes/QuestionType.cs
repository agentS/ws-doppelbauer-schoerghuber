using GraphOverflow.Dtos;
using GraphOverflow.Services;
using GraphOverflow.WebService.GraphQl.GqlSchema.InterfaceGraphTypes;
using GraphQL.Types;

namespace GraphOverflow.WebService.GraphQl.GqlSchema.OutputGraphTypes
{
  public class QuestionType : ObjectGraphType<QuestionDto>
  {
    #region Members
    private readonly ITagService tagService;
    private readonly IAnswerService answerService;
    #endregion Members

    #region Construction
    public QuestionType(ITagService tagService, IAnswerService answerService)
    {
      this.tagService = tagService;
      this.answerService = answerService;
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
      Field<NonNullGraphType<ListGraphType<NonNullGraphType<TagType>>>>(
        name: "tags", resolve: ResolveTags);
      Field<NonNullGraphType<ListGraphType<NonNullGraphType<AnswerType>>>>(
        name: "answers", resolve: ResolveAnswers);

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
    #endregion Resolvers
  }
}
