using GraphOverflow.Dtos;
using GraphQL.Types;

namespace GraphOverflow.WebService.GraphQl.GqlSchema.OutputGraphTypes
{
  public class AuthPayloadgraphType : ObjectGraphType<AuthPayloadDto>
  {
    #region Construction
    public AuthPayloadgraphType()
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
