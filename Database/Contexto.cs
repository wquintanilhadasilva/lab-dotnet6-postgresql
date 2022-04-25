using Microsoft.EntityFrameworkCore;
public class Contexto: DbContext {

    public Contexto(DbContextOptions<Contexto> options)
            : base(options) => Database.EnsureCreated(); // Cria o banco, usar só em dev!


    // Mapeia o domínio mensagem ao banco de dados
    public DbSet<Mensagem> Mensagem { get; set; }

}