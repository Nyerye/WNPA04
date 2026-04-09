/// <file>
/// Item.cs 
/// </file>
/// <project>
/// Windows Network Programming Assignment 4 
/// </project>
/// <author>
/// Nicholas Reilly
/// </author>
/// <date>
/// March 28 2026
/// </date>
/// <description>
/// Class file that holds the definition for the Item class.
/// </description>
/// <references>
/// Deitel, P., & Deitel, H. (2017). *C# 6 for Programmers Sixth Edition* 
/// (Sixth, Ser. Deitel Development Series). Pearson Education.
/// </references>
/// 

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InsurancePal.Models
{
    /// <summary>
    /// Item class that defines what am item is and its properties.
    /// </summary>
    public class Item
    {
        private int itemid;
        private string name;
        private string category;
        private string room;
        private decimal estimatedvalue;
        private DateTime purchasedate;
        private string description;
        private string ownerid;

        public int ItemId
        {
            get
            {
                return itemid;
            }
            set
            {
                itemid = value;
            }

        }

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        public string Category
        {
            get
            {
                return category;
            }

            set
            {
                category = value;
            }
        }

        public string Room
        {
            get
            {
                return room;
            }

            set
            {
                room = value;
            }
        }

        [Display(Name = "Estimated Value")]
        [Column(TypeName = "decimal(10,2)")]
        public decimal EstimatedValue
        {
            get
            {
                return estimatedvalue;
            }
            set
            {
                estimatedvalue = value;
            }
        }

        [Display(Name = "Purchased Date")]
        [DataType(DataType.Date)]
        public DateTime purchasedDate
        {
            get
            {
                return purchasedate;

            }

            set
            {
                purchasedate = value;
            }
        }

        public string Description
        {
            get
            {
                return description;
            }

            set
            {
                description = value;
            }
        }

        [Display(Name = "Owner ID")]
        public string OwnerID
        {
            get
            {
                return ownerid;
            }
            set
            {
                ownerid = value;

            }
        }

      /// <summary>
      /// Empty constructor that is used for the DBContext. This allows for easy creation.
      /// </summary>
      public Item()
        {

        }
    }
}
