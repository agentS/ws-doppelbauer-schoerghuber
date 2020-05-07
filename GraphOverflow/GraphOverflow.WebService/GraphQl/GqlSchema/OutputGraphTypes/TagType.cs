using GraphOverflow.Dtos;
using GraphOverflow.Services;
using GraphQL.Types;
using System;

namespace GraphOverflow.WebService.GraphQl.GqlSchema.OutputGraphTypes
{
  public class TagType : ObjectGraphType<TagDto>
  {
    #region Members
    private readonly IQuestionService questionService;
    #endregion Members

    #region Construction
    public TagType(IQuestionService questionService)
    {
      this.questionService = questionService;
      InitializeName();
      InitializeFields();
    }

    private void InitializeName()
    {
      Name = "Tag";
    }

    private void InitializeFields()
    {
      Field<NonNullGraphType<IdGraphType>>("id");
      Field<NonNullGraphType<StringGraphType>>("name");
      Field<NonNullGraphType<ListGraphType<NonNullGraphType<QuestionType>>>>(
        name: "questions", resolve: ResolveQuestions);
    }
    #endregion Construction

    #region Resolvers
    private object ResolveQuestions(IResolveFieldContext<TagDto> context)
    {
      var tagId = context.Source.Id;
      return questionService.FindQuestionsByTagId(tagId);
    }
    #endregion Resolvers
  }
}
