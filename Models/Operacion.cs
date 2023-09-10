using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace unvest_transactions_ms.Models;

[Table("operacion")]
public class Operacion
{
    [Column("id")]
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [Column("fecha")]
    [JsonPropertyName("fecha")]
    public DateTime Fecha { get; set; }

    [Column("tipo")]
    [JsonPropertyName("tipo")]
    public int Tipo { get; set; }

    [Column("cantidad")]
    [JsonPropertyName("cantidad")]
    public decimal Cantidad { get; set; }

    [Column("id_usuario")]
    [JsonPropertyName("id_usuario")]
    public int IdUsuario { get; set; }
}