/// <file>
/// User.cs
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
/// Model representing a user within the InsurancePal application.
/// Stores authentication and authorization information including
/// username, password hash, and administrative privileges.
/// </description>
/// <references>
/// Deitel, P., & Deitel, H. (2017). *C# 6 for Programmers Sixth Edition*
/// (Sixth, Ser. Deitel Development Series). Pearson Education.
/// </references>
using System.Diagnostics.Eventing.Reader;

namespace InsurancePal.Models
{
    /// <summary>
    /// User class with private and public data members
    /// </summary>
    public class User
    {
        private int id;
        private string username;
        private string passwordHash;
        private bool isadmin;

        public int Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }

        public string Username
        {
            get
            {
                return username;
            }

            set
            {
                username = value;
            }

        }

        public string PasswordHash
        {
            get
            {
                return passwordHash;
            }

            set
            {
                passwordHash = value;
            }
        }

        public bool IsAdmin
        {
            get
            {
                return isadmin;
            }

            set
            {
                isadmin = value;
            }
        }
    }
}
