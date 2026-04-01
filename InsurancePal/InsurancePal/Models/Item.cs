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
/// 
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
    public class Item
    {
        private int itemid;
        private string name;
        private string category;
        private string room;
        private decimal estimatedvalue;
        private DateTime purchasedate;
        private string description;
        private string userid;

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

        public string UserID
        {
            get
            {
                return userid;
            }
            set
            {
                userid = value;

            }
        }

      public Item()
        {

        }
        public Item(int itemid, string name, string category, string room, decimal estimatedvalue, DateTime purchasedate, string description)
        {
            this.itemid = itemid;
            this.name = name;
            this.category = category;
            this.room = room;
            this.estimatedvalue = estimatedvalue;
            this.purchasedate = purchasedate;
            this.description = description;
        }
    }
}
