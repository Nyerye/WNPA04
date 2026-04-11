/// <file>
/// UserCreateViewModel.cs
/// </file>
/// <project>
/// Windows Network Programming Assignment 4
/// </project>
/// <author>
/// Nicholas Reilly
/// </author>
/// <date>
/// April 10 2026
/// </date>
/// <description>
/// View model used for creating new user accounts within the InsurancePal
/// application. Enforces username, password, and password confirmation rules.
/// </description>
/// <references>
/// Deitel, P., & Deitel, H. (2017). *C# 6 for Programmers Sixth Edition* 
/// (Sixth, Ser. Deitel Development Series). Pearson Education.
/// </references>

using System.ComponentModel.DataAnnotations;

public class UserCreateViewModel
{
    /// <summary>
    /// Username for the new user account.
    /// </summary>
    [Required]
    public string Username { get; set; } = string.Empty;

    /// <summary>
    /// Password for the new user account. Masked in UI.
    /// </summary>
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;

    /// <summary>
    /// Confirmation field ensuring the password was typed correctly.
    /// Must match the Password field.
    /// </summary>
    [Required]
    [DataType(DataType.Password)]
    [Compare("Password")]
    public string ConfirmPassword { get; set; } = string.Empty;
}
