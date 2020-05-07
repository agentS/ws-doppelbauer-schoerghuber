using GraphOverflow.Dtos;
using GraphOverflow.WebService.GraphQl.GqlSchema.InterfaceGraphTypes;
using GraphQL.Types;
using System;

namespace GraphOverflow.WebService.GraphQl.GqlSchema.OutputGraphTypes
{
  public class CommentType : ObjectGraphType<CommentDto>
  {
    #region Members
    #endregion Members

    #region Construction
    public CommentType()
    {
      InitializeName();
      InitializeFields();
    }

    private void InitializeName()
    {
      Name = "Comment";
    }

    private void InitializeFields()
    {
      Field<NonNullGraphType<IdGraphType>>("id");
      Field<NonNullGraphType<StringGraphType>>("content");
      Field<NonNullGraphType<DateTimeGraphType>>("createdAt");
      Field<NonNullGraphType<AnswerType>>(
        name: "answer", resolve: ResolveAnswer);

      Interface<PostInterface>();
    }
    #endregion Construction

    #region Resolvers
    private object ResolveAnswer(IResolveFieldContext<CommentDto> arg)
    {
      throw new NotImplementedException();
    }
    #endregion Resolvers
  }
}
