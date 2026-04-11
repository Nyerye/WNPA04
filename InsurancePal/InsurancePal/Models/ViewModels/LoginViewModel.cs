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
/// View model for the login page. Enforces username and password input
/// and supports redirecting users back to their originally requested page
/// after successful authentication.
/// </description>
/// <references>
/// Deitel, P., & Deitel, H. (2017). *C# 6 for Programmers Sixth Edition* 
/// (Sixth, Ser. Deitel Development Series). Pearson Education.
/// </references>

using System.ComponentModel.DataAnnotations;

namespace InsurancePal.Models.ViewModels
{
    /// <summary>
    /// Represents the data required for user login, including username,
    /// password, and an optional return URL for post-authentication redirection.
    /// </summary>
    public class LoginViewModel
    {
        /// <summary>
        /// Username entered by the user. Required for authentication.
        /// </summary>
        [Required]
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// Password entered by the user. Required and masked in UI.
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        /// <summary>
        /// Optional URL to redirect the user to after successful login.
        /// </summary>
        public string? ReturnUrl { get; set; }
    }
}
