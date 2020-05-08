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
    private readonly ICommentService commentService;
    #endregion Members

    #region Construction
    public QuestionType(ITagService tagService,
      IAnswerService answerService,
      ICommentService commentService)
    {
      this.tagService = tagService;
      this.answerService = answerService;
      this.commentService = commentService;

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
      Field<NonNullGraphType<IntGraphType>>("upVotes");
      Field<NonNullGraphType<ListGraphType<NonNullGraphType<TagType>>>>(
        name: "tags", resolve: ResolveTags);
      Field<NonNullGraphType<ListGraphType<NonNullGraphType<AnswerType>>>>(
        name: "answers", resolve: ResolveAnswers);
      ////Field<NonNullGraphType<ListGraphType<NonNullGraphType<CommentType>>>>(
      ////  name: "comments", resolve: ResolveComments);

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

    private object ResolveComments(IResolveFieldContext<QuestionDto> context)
    {
      var question = context.Source;
      return commentService.FindCommentsForQuestion(question);
    }
    #endregion Resolvers
  }
}
