﻿using GraphQL.Builders;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphOverflow.WebService.GraphQl.Extensions
{
  public static class GraphQLExtensions
  {
    public static readonly string PERMISSIONS_KEY = "Permissions";

    public static bool RequiresPermissions(this IProvideMetadata type)
    {
      var permissions = type?.GetMetadata<IEnumerable<string>>(PERMISSIONS_KEY, new List<string>());
      return (permissions != null && permissions.Any());
    }

    public static bool CanAccess(this IProvideMetadata type, IEnumerable<string> claims)
    {
      var permissions = type.GetMetadata<IEnumerable<string>>(PERMISSIONS_KEY, new List<string>());
      return permissions.All(x => claims?.Contains(x) ?? false);
    }

    public static bool HasPermission(this IProvideMetadata type, string permission)
    {
      var permissions = type.GetMetadata<IEnumerable<string>>(PERMISSIONS_KEY, new List<string>());
      return permissions.Any(x => string.Equals(x, permission));
    }

    public static void RequirePermission(this IProvideMetadata type, string permission)
    {
      var permissions = type.GetMetadata<List<string>>(PERMISSIONS_KEY);

      if (permissions == null)
      {
        permissions = new List<string>();
        type.Metadata[PERMISSIONS_KEY] = permissions;
      }

      permissions.Add(permission);
    }

    public static FieldBuilder<TSourceType, TReturnType> RequirePermission<TSourceType, TReturnType>(
        this FieldBuilder<TSourceType, TReturnType> builder, string permission)
    {
      builder.FieldType.RequirePermission(permission);
      return builder;
    }
  }
}
