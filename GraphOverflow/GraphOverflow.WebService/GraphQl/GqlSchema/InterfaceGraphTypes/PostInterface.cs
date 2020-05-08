using GraphOverflow.Dtos;
using GraphQL.Types;
using System;

namespace GraphOverflow.WebService.GraphQl.GqlSchema.InterfaceGraphTypes
{
  public class PostInterface : InterfaceGraphType<IPostDto>
  {
    #region Construction
    public PostInterface()
    {
      InitializeTypeName();
      InitializeFields();
    }

    private void InitializeTypeName()
    {
      Name = "PostInterface";
    }

    private void InitializeFields()
    {
      Field<NonNullGraphType<IdGraphType>>("id");
      Field<NonNullGraphType<StringGraphType>>("content");
      Field<NonNullGraphType<DateTimeGraphType>>("createdAt");
      Field<NonNullGraphType<IntGraphType>>("upVotes");
    }
    #endregion Construction
  }
}
