/// <file>
/// LoginViewModel.cs 
/// </file>
/// <project>
/// Windows Network Programming Assignment 4
/// </project>
/// <author>
/// Nicholas Reilly
/// </author>
/// <date>
/// April 9 2026
/// </date>
/// <description>
/// View model for the login page.
/// </description>
/// <references>
/// Deitel, P., & Deitel, H. (2017). *C# 6 for Programmers Sixth Edition* 
/// (Sixth, Ser. Deitel Development Series). Pearson Education.
/// </references>
/// 
using System.ComponentModel.DataAnnotations;

namespace InsurancePal.Models.ViewModels
{
    /// <summary>
    /// LoginViewModel class. Enforces the use of a username and password. ReturnUrl is to redirect back to page user wants after authentication.
    /// </summary>
    public class LoginViewModel
    {
        [Required]
        public string Username { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        public string? ReturnUrl { get; set; }
    }
}