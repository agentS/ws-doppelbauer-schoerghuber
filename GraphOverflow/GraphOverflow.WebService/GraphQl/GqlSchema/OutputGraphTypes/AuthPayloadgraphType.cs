using GraphOverflow.Dtos;
using GraphQL.Types;

namespace GraphOverflow.WebService.GraphQl.GqlSchema.OutputGraphTypes
{
  public class AuthPayloadGraphType : ObjectGraphType<AuthPayloadDto>
  {
    #region Construction
    public AuthPayloadGraphType()
    {
      InitializeName();
      InitializeFields();
    }

    private void InitializeName()
    {
      Name = "AuthPayload";
    }

    private void InitializeFields()
    {
      Field<NonNullGraphType<UserGraphType>>("user");
      Field<NonNullGraphType<StringGraphType>>("token");
    }
    #endregion Construction
  }
}
