using GraphOverflow.Services.Implementation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace GraphOverflow.WebService.GraphQl
{
  public class GraphQlUserContext : Dictionary<string, object>
  {
    private const string AUTORICATION_HEADER = "Authorization";

    public const string AUTHORIZATION_TOKEN_KEY = "auth_token";
    public const string USER_ID_KEY = "UserID";

    public static Func<HttpContext, GraphQlUserContext> UserContextCreator = Create;

    private static GraphQlUserContext Create(HttpContext httpcontext)
    {
      var userContext = new GraphQlUserContext();
      if (httpcontext.Request.Headers.ContainsKey(AUTORICATION_HEADER))
      {
        var token = httpcontext.Request.Headers[AUTORICATION_HEADER];
        userContext.Add(AUTHORIZATION_TOKEN_KEY, token);

        AuthenticationService authenticationService = new AuthenticationService();
        var user = authenticationService.GetAuthenticatedUser(token);
        if (user != null)
        {
          userContext.Add(USER_ID_KEY, user.Id);
        }
      }
      return userContext;
    }

    public bool IsUserAuthenticated()
    {
      return ContainsKey(USER_ID_KEY);
    }

    public int GetIdOfAuthenticatedUser()
    {
      return (int)this[USER_ID_KEY];
    }
  }
}
