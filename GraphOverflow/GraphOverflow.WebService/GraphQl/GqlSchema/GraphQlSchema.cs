using GraphOverflow.Services;
using GraphOverflow.WebService.GraphQl.GqlSchema.RootGraphTypes;
using GraphQL.Types;
using System;

namespace GraphOverflow.WebService.GraphQl.GqlSchema
{
  public class GraphQlSchema : Schema
  {
    public GraphQlSchema(ITagService tagService, IQuestionService questionService, IServiceProvider provider) 
      : base(provider)
    {
      Query = new QueryType(tagService, questionService);
      Mutation = new MutationType(tagService, questionService);
      Subscription = new SubscriptionType(tagService);
    }
  }
}
