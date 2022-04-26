using Microsoft.EntityFrameworkCore;
public class Contexto: DbContext {

    public Contexto(DbContextOptions<Contexto> options)
            : base(options) => Database.EnsureCreated(); // Cria o banco, usar só em dev!


    // Mapeia o domínio mensagem ao banco de dados
    public DbSet<Mensagem> Mensagem { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Mensagem>(
            entityBuilder => {
                entityBuilder.HasKey( m => m.Id);
                entityBuilder.Property(m => m.Id)
                    .HasColumnName("id")
                    .ValueGeneratedOnAdd();
                entityBuilder.Property(m => m.Horario).HasColumnName("time");
                entityBuilder.Property(m => m.Saudacao).HasColumnName("salutation");
                entityBuilder.ToTable("mensagens");
            }
        );
            
    }

}