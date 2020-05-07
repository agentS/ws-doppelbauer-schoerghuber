using Newtonsoft.Json.Linq;

namespace GraphOverflow.GraphQl.Common
{
    public class GraphQlQuery
    {
        /// <summary>
        /// operation name of the graphql query.
        /// </summary>
        public string OperationName { get; set; }

        /// <summary>
        /// variables of the graphql query as json.
        /// </summary>
        public JObject Variables { get; set; }

        /// <summary>
        /// graphql query.
        /// </summary>
        public string Query { get; set; }
    }
}
