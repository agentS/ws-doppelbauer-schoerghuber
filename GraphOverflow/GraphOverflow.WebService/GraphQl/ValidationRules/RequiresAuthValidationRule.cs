using GraphOverflow.WebService.GraphQl.Extensions;
using GraphQL.Language.AST;
using GraphQL.Validation;
using System.Threading.Tasks;

namespace GraphOverflow.WebService.GraphQl.ValidationRules
{
  public class RequiresAuthValidationRule : IValidationRule
  {
    public INodeVisitor Validate(ValidationContext context)
    {
      var userContext = context.UserContext as GraphQlUserContext;
      bool authenticated = false;
      if (userContext != null)
      {
        authenticated = userContext.IsUserAuthenticated();
      }

      return new EnterLeaveListener(_ =>
      {
        // this could leak info about hidden fields in error messages
        // it would be better to implement a filter on the schema so it
        // acts as if they just don't exist vs. an auth denied error
        // - filtering the schema is not currently supported
        _.Match<Field>(fieldAst =>
        {
          var fieldDef = context.TypeInfo.GetFieldDef();
          if (fieldDef.RequiresPermissions() && (!authenticated || !fieldDef.CanAccess(userContext.User.Claims)))
          {
            context.ReportError(new ValidationError(
                context.OriginalQuery,
                "auth-required",
                $"You are not authorized to run this query.",
                fieldAst));
          }
        });
      });
    }

    public Task<INodeVisitor> ValidateAsync(ValidationContext context)
    {
      return Task.Run(() => Validate(context));
    }
  }
}
