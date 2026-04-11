/// <file>
/// HomeDashboardViewModel.cs
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
/// View model used to store and transport dashboard statistics for the
/// landing page of the InsurancePal application.
/// </description>
/// <references>
/// Deitel, P., & Deitel, H. (2017). *C# 6 for Programmers Sixth Edition*
/// (Sixth, Ser. Deitel Development Series). Pearson Education.
/// </references>

namespace InsurancePal.Models
{
    /// <summary>
    /// Holds summary statistics for the user's items, displayed on the home dashboard.
    /// </summary>
    public class HomeDashboardViewModel
    {
        //Total number of items owned by the logged-in user
        private int totalItems;

        //Combined estimated value of all items
        private decimal totalValue;

        //Number of unique rooms represented in the user's inventory
        private int uniqueRooms;

        //Number of unique categories represented in the user's inventory
        private int uniqueCategories;

        /// <summary>
        /// Gets or sets the total number of items.
        /// </summary>
        public int TotalItems
        {
            get { return totalItems; }
            set { totalItems = value; }
        }

        /// <summary>
        /// Gets or sets the total estimated value of all items.
        /// </summary>
        public decimal TotalValue
        {
            get { return totalValue; }
            set { totalValue = value; }
        }

        /// <summary>
        /// Gets or sets the number of unique rooms.
        /// </summary>
        public int UniqueRooms
        {
            get { return uniqueRooms; }
            set { uniqueRooms = value; }
        }

        /// <summary>
        /// Gets or sets the number of unique categories.
        /// </summary>
        public int UniqueCategories
        {
            get { return uniqueCategories; }
            set { uniqueCategories = value; }
        }

        /// <summary>
        /// Default constructor for the dashboard ViewModel.
        /// </summary>
        public HomeDashboardViewModel()
        {
        }
    }
}
