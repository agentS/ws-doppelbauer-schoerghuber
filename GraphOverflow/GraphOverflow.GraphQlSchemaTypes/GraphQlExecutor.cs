using GraphOverflow.GraphQl.Common;
using GraphQL;
using System.Threading.Tasks;
using GraphQL.Types;

namespace GraphOverflow.GraphQl
{
  public class GraphQlExecutor
  {
    private ISchema schema;
    public GraphQlExecutor(ISchema schema)
    {
      this.schema = schema;
    }
    public async Task<object> ExecuteAsync(GraphQlQuery query)
    {
      ExecutionResult result = await new DocumentExecuter().ExecuteAsync(_ =>
      {
        _.Schema = schema;
        _.Query = query.Query;
        _.Inputs = query.Variables.ToInputs();
        _.ExposeExceptions = false;
      }).ConfigureAwait(false);
      return result;
    }
  }
}
