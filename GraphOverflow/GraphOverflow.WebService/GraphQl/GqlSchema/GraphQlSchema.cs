using GraphOverflow.Services;
using GraphOverflow.WebService.GraphQl.GqlSchema.RootGraphTypes;
using GraphQL.Types;
using System;

namespace GraphOverflow.WebService.GraphQl.GqlSchema
{
  public class GraphQlSchema : Schema
  {
    public GraphQlSchema(ITagService tagService, 
      IQuestionService questionService, 
      IAuthenticationService authenticationService,
      IAnswerService answerService,
      IServiceProvider provider,
      IUserService userService) 
      : base(provider)
    {
      Query = new QueryType(tagService, userService);
      Mutation = new MutationType(tagService, questionService, answerService, authenticationService);
      Subscription = new SubscriptionType(tagService);
    }
  }
}
