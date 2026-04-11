namespace InsurancePal.Models
{
    public class HomeDashboardViewModel
    {
        private int totalItems;
        private decimal totalValue;
        private int uniqueRooms;
        private int uniqueCategories;

        public int TotalItems
        {
            get 
            { 
                return totalItems; 
            }
            set 
            { 
                totalItems = value; 
            }
        }

        public decimal TotalValue
        {
            get 
            { 
                return totalValue; 
            }
            set 
            { 
                totalValue = value; 
            }
        }

        public int UniqueRooms
        {
            get 
            { 
                return uniqueRooms; 
            }
            set 
            { 
                uniqueRooms = value; 
            }
        }

        public int UniqueCategories
        {
            get 
            { 
                return uniqueCategories; 
            }
            set 
            { 
                uniqueCategories = value; 
            }
        }

        public HomeDashboardViewModel()
        {

        }
    }
}
