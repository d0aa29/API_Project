using System.ComponentModel.DataAnnotations;

namespace MyAPI.Models.Dto
{
	public class VillaDTOcs
	{
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
