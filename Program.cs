using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddEntityFrameworkNpgsql()
    .AddDbContext<Contexto>(options => 
        options.UseNpgsql("Host=localhost;Port=15432;Pooling=true;Database=public;User Id=postgres;Password=123456;")
    );

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddControllers();

builder.Services.AddSwaggerGen(c =>
    c.SwaggerDoc(
        "v1", new OpenApiInfo
        {
            Version = "v1",
            Title = "ToDo API",
            Description = "ASP.NET Web API 6 para gerenciar o aplicativo XXXXX",
            TermsOfService = new Uri("https://example.com/terms"),
            Contact = new OpenApiContact
            {
                Name = "José Iramar",
                Url = new Uri("https://example.com/contact")
            },
            License = new OpenApiLicense
            {
                Name = "Licença de Uso",
                Url = new Uri("https://example.com/license")
            }
        }
    )
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "APP API v1");
});

/*Definindo os endpoints*/

/*
    Estas APIs definem um CRUD para o objeto Mensagem no Postgresql
*/

app.MapGet("/Mensagem", (string nome) => new Mensagem()
{
    Saudacao = $"Olá {nome ?? "Anônimo(a)"}!"
});

app.MapPost("AdicionaMensagem", async (Mensagem mensage, Contexto contexto) =>
{
    contexto.Mensagem.Add(mensage);
    await contexto.SaveChangesAsync();
});

app.MapPost("ExcluirMensagem/{id}", async (int id, Contexto contexto) =>
{
    var mensageExcluir = await contexto.Mensagem.FirstOrDefaultAsync(p => p.Id == id);
    if (mensageExcluir != null)
    {
        contexto.Mensagem.Remove(mensageExcluir);
        await contexto.SaveChangesAsync();
    }
});

app.MapPost("ListarMensagem", async (Contexto contexto) =>
{
    return await contexto.Mensagem.ToListAsync();
});

app.MapPost("ObterMensagem/{id}", async (int id, Contexto contexto) =>
{
    return await contexto.Mensagem.FirstOrDefaultAsync(p => p.Id == id);
});


/**************************************************/

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
