using GraphOverflow.GraphQl.RootGraphTypes;
using GraphQL;
using GraphQL.Types;
using System;

namespace GraphOverflow.GraphQl
{
  public class GraphQlSchema : Schema
  {
    public GraphQlSchema(IDependencyResolver resolver) : base(resolver)
    {
      // Query = new QueryType();
      Query = resolver.Resolve<QueryType>();
      Mutation = resolver.Resolve<MutationType>();
    }
  }
}
