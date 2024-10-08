using System;
using System.ComponentModel.DataAnnotations;

namespace Identity.Management.Api.Dtos.Authentication.Login;

public class Login
{
  [Required]
  [EmailAddress]
  public required string Email { get; set; }

  [Required]
  public required string Password { get; set; }
}
