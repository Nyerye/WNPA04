/// <file>
/// ErrorViewModel.cs
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
/// View model used for displaying error information on error pages.
/// Provides request tracking details for debugging.
/// </description>
/// <references>
/// Deitel, P., & Deitel, H. (2017). *C# 6 for Programmers Sixth Edition* 
/// (Sixth, Ser. Deitel Development Series). Pearson Education.
/// </references>

namespace InsurancePal.Models.ViewModels
{
    /// <summary>
    /// Represents error details passed to the error view.
    /// Includes the request ID for diagnostic purposes.
    /// </summary>
    public class ErrorViewModel
    {
        /// <summary>
        /// Unique identifier for the current request.
        /// Used to correlate errors with server logs.
        /// </summary>
        public string? RequestId { get; set; }

        /// <summary>
        /// Indicates whether the RequestId should be displayed.
        /// </summary>
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
