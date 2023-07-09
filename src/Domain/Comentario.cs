using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BEComentarios.Domain
{
    public class Comentario
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Titulo { get; set; }

        [Required]
        public string Criador { get; set; }

        [MaxLength(4000)]
        public string Texto { get; set; }

        [Required]
        public DateTime DataCriacao { get; set; }
    }
}
