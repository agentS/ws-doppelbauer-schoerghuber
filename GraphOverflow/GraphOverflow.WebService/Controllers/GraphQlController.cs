using Microsoft.AspNetCore.Mvc;

namespace GraphOverflow.WebService.Controllers
{
  [Route("api/graphql")]
  [ApiController]
  public class GraphQlController : Controller
  {
    ////private GraphQlExecutor executor;
    ////public GraphQlController(GraphQlExecutor executor)
    ////{
    ////  this.executor = executor;
    ////}

    ////[HttpPost]
    ////public async Task<IActionResult> Post([FromBody] GraphQlQuery query)
    ////{
    ////  var result = await executor.ExecuteAsync(query);
    ////  return Ok(result);
    ////}
  }
}