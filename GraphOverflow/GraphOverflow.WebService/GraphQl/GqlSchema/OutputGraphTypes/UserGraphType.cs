using GraphOverflow.Dtos;
using GraphQL.Types;

namespace GraphOverflow.WebService.GraphQl.GqlSchema.OutputGraphTypes
{
  public class UserGraphType : ObjectGraphType<UserDto>
  {
    #region Construction
    public UserGraphType()
    {
      InitializeName();
      InitializeFields();
    }

    private void InitializeName()
    {
      Name = "User";
    }

    private void InitializeFields()
    {
      Field<NonNullGraphType<IdGraphType>>("id");
      Field<NonNullGraphType<StringGraphType>>("name");
    }
    #endregion Construction
  }
}
