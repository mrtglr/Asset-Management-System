using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthLoginDemo_bnd.Models
{
    public class Asset
    {
        [Key]
        public int asset_id { get; set; }

        [ForeignKey("ApplicationUser")]
        public string Id {get; set;}

        [ForeignKey("Neighbourhood")]
        public int neighbourhood_id { get; set; }
        public int worth { get; set; }
        public string attribute { get; set; }
        public string adress { get; set; }
        public bool isActive { get; set; }   
        public virtual Neighbourhood Neighbourhood { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}