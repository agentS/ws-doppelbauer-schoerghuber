using System;
using System.Collections.Generic;
using System.Text;

namespace GraphOverflow.Dtos
{
  public class AuthPayloadDto
  {
    public UserDto User { get; set; }
    public string Token { get; set; }
  }
}
