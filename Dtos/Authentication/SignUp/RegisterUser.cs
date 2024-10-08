using System;
using System.ComponentModel.DataAnnotations;

namespace Identity.Management.Api.Models.Authentication.SignUp;

public class RegisterUser
{
  [EmailAddress]
  [Required(ErrorMessage = "Email is required")]
  public required string Email { get; set; }

  [Required(ErrorMessage = "Password is required")]
  public required string Password { get; set; }
}
