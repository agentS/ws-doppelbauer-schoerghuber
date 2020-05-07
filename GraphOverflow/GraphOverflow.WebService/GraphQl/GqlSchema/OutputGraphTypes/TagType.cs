using GraphOverflow.Dtos;
using GraphQL.Types;
using System;

namespace GraphOverflow.WebService.GraphQl.GqlSchema.OutputGraphTypes
{
  public class TagType : ObjectGraphType<TagDto>
  {
    #region Construction
    public TagType()
    {
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
      throw new NotImplementedException();
    }
    #endregion Resolvers
  }
}
