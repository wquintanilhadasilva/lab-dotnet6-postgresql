public class Mensagem {

    public int Id { get; set; }
    public string? Saudacao { get; set; }
    public string Horario { get; init; } = $"{DateTime.Now:HH:mm:ss}";
}