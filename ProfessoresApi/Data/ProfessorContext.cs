using Microsoft.EntityFrameworkCore;
using ProfessoresApi.Models;

namespace ProfessoresApi.Data;

public class ProfessorContext : DbContext
{
    public ProfessorContext(DbContextOptions<ProfessorContext> opts) : base(opts) { }

    public DbSet<Professor> Professores { get; set; }
}
