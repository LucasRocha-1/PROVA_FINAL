namespace API.Models;

public class IMC
{
    //Construtor
    public IMC()
    {
        id = Guid.NewGuid().ToString();
        dataCriacao = DateTime.Now;
    }

    //Propriedade/Atributo/Característica
    public string id { get; set; }
    public string nome { get; set; } = string.Empty;
    public string descricao { get; set; } = string.Empty;
    public double altura { get; set; }
    public double peso { get; set; }
    public double imc { get; set; }
    public DateTime dataCriacao { get; set; }
}
