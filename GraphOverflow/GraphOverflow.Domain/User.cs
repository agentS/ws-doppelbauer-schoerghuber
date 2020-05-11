using System;
using System.Collections.Generic;
using System.Text;

namespace GraphOverflow.Domain
{
  public class User
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string PasswordHash { get; set; }
  }
}
