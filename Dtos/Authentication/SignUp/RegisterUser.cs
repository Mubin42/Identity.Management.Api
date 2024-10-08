using System;
using System.ComponentModel.DataAnnotations;

namespace Identity.Management.Api.Models.Authentication.SignUp;

public class RegisterUser
{
  [Required(ErrorMessage = "Email is required")]
  public string? Username { get; set; }

  [EmailAddress]
  [Required(ErrorMessage = "Email is required")]
  public string? Email { get; set; }

  [Required(ErrorMessage = "Password is required")]
  public string? Password { get; set; }
}
