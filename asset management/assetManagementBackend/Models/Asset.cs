using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace assetManagementBackend.Models
{
    public class Asset
    {   
        [Key]
        public int asset_id { get; set; }

        [ForeignKey("User")]
        public int user_id {get; set;}

        [ForeignKey("Neighbourhood")]
        public int neighbourhood_id { get; set; }
        public int worth { get; set; }
        public string attribute { get; set; }
        public string adress { get; set; }
        public bool isActive { get; set; }   
        public virtual Neighbourhood Neighbourhood { get; set; }
        public virtual User User { get; set; }
    }
}