﻿using BEComentarios.Domain;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace BEComentarios.Infrastructure
{
    public class ComentarioRepository : BaseRepository<Comentario>
    {
        public ComentarioRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
