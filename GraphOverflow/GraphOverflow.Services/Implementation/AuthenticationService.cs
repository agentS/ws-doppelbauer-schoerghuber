﻿using GraphOverflow.Domain;
using GraphOverflow.Dtos;
using GraphOverflow.Dtos.Input;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace GraphOverflow.Services.Implementation
{
  public class AuthenticationService : IAuthenticationService
  {
    private const string CLAIM_TYPE_NAME = "unique_name";
    private const string TOKEN_SECRET = 
      "THIS IS USED TO SIGN AND VERIFY JWT TOKENS, REPLACE IT WITH YOUR OWN SECRET, IT CAN BE ANY STRING";

    public AuthenticationService()
    {
    }

    public AuthPayloadDto Authenticate(UserLoginInputDto userLogin)
    {
      try
      {
        // User user = await userDao.GetByEmail(userLogin.Email);
        User user = new User { Id = 1, Name = "alex", PasswordHash = "xyz" };
        if (user == null)
        {
          return null;
        }
        if (user.PasswordHash == GetPasswordHash(userLogin.Password))
        {
          return new AuthPayloadDto
          {
            User = new UserDto { Id = user.Id, Name = user.Name },
            Token = GetJwtToken(user)
          };
        }
        return null;
      }
      catch (Exception)
      {
        return null;
      }
    }

    public UserDto GetAuthenticatedUser(string token)
    {
      if (token != null && ValidateToken(token))
      {
        int id = int.Parse(GetClaim(token, CLAIM_TYPE_NAME));
        return new UserDto { Id = id };
      }
      else
      {
        return null;
      }
    }

    public bool ValidateToken(string token)
    {
      var securityKey = new SymmetricSecurityKey(
        Encoding.ASCII.GetBytes(TOKEN_SECRET));
      var tokenHandler = new JwtSecurityTokenHandler();
      try
      {
        tokenHandler.ValidateToken(token, new TokenValidationParameters
        {
          ValidateIssuer = false,
          ValidateAudience = false,
          ValidateIssuerSigningKey = true,
          IssuerSigningKey = securityKey,
          
        }, out SecurityToken validatedToken);
      }
      catch
      {
        return false;
      }
      return true;
    }

    private string GetJwtToken(User user)
    {
      var key = Encoding.ASCII.GetBytes(TOKEN_SECRET);
      var tokenHandler = new JwtSecurityTokenHandler();
      var tokenDescriptor = new SecurityTokenDescriptor
      {
        Subject = new ClaimsIdentity(new Claim[]
        {
          new Claim(ClaimTypes.Name, user.Id.ToString())
        }),
        Expires = DateTime.UtcNow.AddDays(7),
        SigningCredentials = new SigningCredentials(
          new SymmetricSecurityKey(key), 
          SecurityAlgorithms.HmacSha256Signature)
      };
      var token = tokenHandler.CreateToken(tokenDescriptor);
      return tokenHandler.WriteToken(token);
    }

    private string GetPasswordHash(string password)
    {
      return password;
    }

    public string GetClaim(string token, string claimType)
    {
      var tokenHandler = new JwtSecurityTokenHandler();
      var securityToken = tokenHandler.ReadToken(token) as JwtSecurityToken;
      var stringClaimValue = securityToken.Claims.First(claim => claim.Type == claimType).Value;
      return stringClaimValue;
    }
  }
}