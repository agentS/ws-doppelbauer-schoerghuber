using GraphOverflow.Dtos;
using GraphOverflow.Services;
using GraphOverflow.WebService.GraphQl.GqlSchema.InterfaceGraphTypes;
using GraphQL.Types;
using System;

namespace GraphOverflow.WebService.GraphQl.GqlSchema.OutputGraphTypes
{
  public class QuestionType : ObjectGraphType<QuestionDto>
  {
    #region Members
    private readonly ITagService tagService;
    #endregion Members

    #region Construction
    public QuestionType(ITagService tagService)
    {
      this.tagService = tagService;
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

      Interface<PostInterface>();
    }
    #endregion Construction

    #region Resolvers
    private object ResolveTags(IResolveFieldContext<QuestionDto> context)
    {
      throw new NotImplementedException();
    }
    #endregion Resolvers
  }
}
