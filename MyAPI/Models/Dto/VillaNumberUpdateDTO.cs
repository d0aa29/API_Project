using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyAPI.Models.Dto
{
    public class VillaNumberUpdateDTO
    {
        public int Id { get; set; }
        [Required]
        public int VillaNo { get; set; }

        public string SpecialDetails { get; set; }
    }
}
