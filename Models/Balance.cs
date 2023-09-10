using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace unvest_transactions_ms.Models;

[Table("balance")]
public class Balance
{
    [Column("id")]
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [Column("valor")]
    [JsonPropertyName("valor")]
    public decimal Valor { get; set; }

    [Column("id_usuario")]
    [JsonPropertyName("id_usuario")]
    public int IdUsuario { get; set; }

    
}