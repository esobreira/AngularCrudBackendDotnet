using BEComentarios.Domain;
using BEComentarios.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BEComentarios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComentarioController : ControllerBase
    {
        //private readonly ApplicationDbContext _context;

        //public ComentarioController(ApplicationDbContext dbContext)
        //{
        //    _context = dbContext;
        //}

        private readonly ComentarioRepository _repository;

        public ComentarioController(ComentarioRepository comentarioRepository)
        {
            _repository = comentarioRepository;
        }

        public record ComentarioFormatado()
        {
            public int Id { get; set; }

            public string Titulo { get; set; }

            public string Criador { get; set; }

            public string Texto { get; set; }

            [JsonPropertyName("data_criacao")]
            public DateTime Data_Criacao { get; set; }
        }

        [HttpGet]
        public IList<ComentarioFormatado> GetAll()
        {
            var result = _repository.GetAll()
                .Select(x => new ComentarioFormatado()
                {
                    Id = x.Id,
                    Criador = x.Criador,
                    Titulo = x.Titulo,
                    Texto = x.Texto,
                    Data_Criacao = x.DataCriacao
                }).ToList();

            return result;
        }

        // GET api/<ComentarioController>/5
        [HttpGet("{id}")]
        public ComentarioFormatado? Get(int id)
        {
            var result = _repository.GetById(id);

            if (result is null)
            {
                return null;
            }

            var data = new ComentarioFormatado()
            {
                Id = result.Id,
                Criador = result.Criador,
                Texto = result.Texto,
                Titulo = result.Titulo,
                Data_Criacao = result.DataCriacao
            };

            return data;
        }

        public record ComentarioDto
        {
            public string Titulo { get; set; }
            public string Criador { get; set; }
            public string Texto { get; set; }
        }

        // POST api/<ComentarioController>
        [HttpPost]
        public async void Post([FromBody] ComentarioDto comentario)
        {
            var new_comentario = new Comentario()
            {
                Titulo = comentario.Titulo,
                Criador = comentario.Criador,
                Texto = comentario.Texto,
                DataCriacao = DateTime.Now
            };

            var _ = await _repository.Add(new_comentario);
        }

        // PUT api/<ComentarioController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] ComentarioDto comentario)
        {
            var comment = _repository.GetById(id);

            if (comment is null)
            {
                throw new NullReferenceException("Comentário não encontrado pelo Id.");
            }

            comment.Titulo = comentario.Titulo;
            comment.Criador = comentario.Criador;
            comment.Texto = comentario.Texto;

            _repository.Update(comment);
        }

        // DELETE api/<ComentarioController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var comentario = _repository.GetById(id);

            if (comentario is null)
            {
                throw new NullReferenceException("Comentário não encontrado pelo Id.");
            }

            _repository.Delete(comentario);
        }
    }
}
