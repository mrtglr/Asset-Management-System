using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace assetManagementBackend.Models
{
    public class Neighbourhood
    {
        [Key]
        public int neighbourhood_id {get; set;}

        [ForeignKey("District")]
        public int district_id {get; set;}
        public string neighbourhood_name {get; set;}               
        public virtual District District {get; set;}
    }
}