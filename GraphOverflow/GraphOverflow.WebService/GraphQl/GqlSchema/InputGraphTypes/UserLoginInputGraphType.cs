using GraphOverflow.Dtos.Input;
using GraphQL.Language.AST;
using GraphQL.Types;

namespace GraphOverflow.WebService.GraphQl.GqlSchema.InputGraphTypes
{
  public class UserLoginInputGraphType : InputObjectGraphType<UserLoginInputDto>
  {
    public UserLoginInputGraphType()
    {
      InitializeName();
      InitializeFields();
    }

    private void InitializeName()
    {
      Name = "UserLoginInput";
    }

    private void InitializeFields()
    {
      Field<NonNullGraphType<StringGraphType>>(
        name: "userName"
      );
      Field<NonNullGraphType<StringGraphType>>(name: "password");
    }
  }
}
