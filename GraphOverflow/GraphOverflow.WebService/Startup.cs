using GraphOverflow.Dal;
using GraphOverflow.Dal.Implementation;
using GraphOverflow.Services;
using GraphOverflow.Services.Implementation;
using GraphOverflow.WebService.GraphQl;
using GraphOverflow.WebService.GraphQl.GqlSchema;
using GraphQL.Server;
using GraphQL.Server.Ui.GraphiQL;
using GraphQL.Server.Ui.Playground;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace GraphOverflow.WebService
{
  public class Startup
  {
    private const string CORS_POLICY = "CorsPolicy";
    private const string GRAPHIQL_ENDPOINT = "/graphql";
    private const string GRAPHIQL_API_ENDPOINT = "/api/graphql";
    public Startup(IConfiguration configuration, IWebHostEnvironment environment)
    {
      Configuration = configuration;
      Environment = environment;
    }

    public IConfiguration Configuration { get; }
    public IWebHostEnvironment Environment { get; }

    public void ConfigureServices(IServiceCollection services)
    {
      services.AddCors(o => o.AddPolicy(CORS_POLICY, builder =>
      {
        builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
      }));
      //services.AddControllers();
      // due to dotnet core 3.x: https://github.com/dotnet/aspnetcore/issues/13564
      //services.AddControllers()
      //  .AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);


      // define services
      services.AddSingleton<ITagDao, TagDao>();
      services.AddSingleton<ITagService, TagService>();

      //define schema types
      //services.AddSingleton<TagType>();
      //services.AddSingleton<QueryType>();
      //services.AddSingleton<MutationType>();
      
      // Add GraphQL services and configure options
      services
          .AddSingleton<GraphQlSchema>()
          .AddGraphQL(options =>
          {
            options.EnableMetrics = false;
            //options.EnableMetrics = Environment.IsDevelopment();
            options.ExposeExceptions = Environment.IsDevelopment();
          })
          .AddSystemTextJson(deserializerSettings => { }, serializerSettings => { })
          .AddWebSockets() // Add required services for web socket support
          //.AddDataLoader() // Add required services for DataLoader support
          .AddGraphTypes(typeof(GraphQlSchema)); // Add all IGraphType implementors in assembly which GraphQlSchema exists 

      // define services
      //services.AddSingleton<ITagService, TagService>();

      // define graphql dependencies
      ////services.AddSingleton<IDependencyResolver>(s => 
      ////  new FuncDependencyResolver(s.GetRequiredService));
      ////services.AddSingleton<IDocumentWriter, DocumentWriter>();
      ////services.AddSingleton<TagType>();
      ////services.AddSingleton<QueryType>();
      ////services.AddSingleton<MutationType>();
      ////services.AddSingleton<ISchema, GraphQlSchema>();
      ////services.AddSingleton<GraphQlExecutor, GraphQlExecutor>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (Environment.IsDevelopment())
        app.UseDeveloperExceptionPage();

      app.UseWebSockets();
      app.UseGraphQLWebSockets<GraphQlSchema>(GRAPHIQL_API_ENDPOINT);

      app.UseGraphQL<GraphQlSchema, GraphQLHttpMiddlewareWithLogs<GraphQlSchema>>(GRAPHIQL_API_ENDPOINT);

      app.UseGraphiQLServer(new GraphiQLOptions
      {
        GraphiQLPath = GRAPHIQL_ENDPOINT,
        GraphQLEndPoint = GRAPHIQL_API_ENDPOINT,
      });

      app.UseGraphQLPlayground(new GraphQLPlaygroundOptions
      {
        Path = "/ui/playground",
        GraphQLEndPoint = GRAPHIQL_API_ENDPOINT
      });

      // https://github.com/JosephWoodward/graphiql-dotnet
      // app.UseGraphiQl(GRAPHIQL_ENDPOINT, GRAPHIQL_API_ENDPOINT);
    }
  }
}
