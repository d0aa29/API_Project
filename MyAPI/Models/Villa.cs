using System.ComponentModel.DataAnnotations;

namespace MyAPI.Models
{
	public class Villa
	{
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
