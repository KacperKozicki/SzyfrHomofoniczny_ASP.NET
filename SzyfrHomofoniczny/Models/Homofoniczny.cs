using System.ComponentModel.DataAnnotations;
using SzyfrHomofoniczny.Controllers;
namespace SzyfrHomofoniczny.Models
{
    public class Homofoniczny
    {
        [Required]
        public string? Tekst { get; set; }
        public string? ZaszyfrowanyTekst { get; set; }

    }
}
