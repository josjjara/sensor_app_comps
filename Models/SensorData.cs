using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SensorApp.Models
{
    public class SensorData
    {

        [Key]
        [Column("id")]
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [Column("codigo_parametro")]
        [JsonPropertyName("codigo_parametro")]
        public int CodigoParametro { get; set; }

        [ForeignKey("Parameter")]
        [Column("parametro_sensores_id")]
        [JsonPropertyName("parametro_sensores_id")]
        public int ParametroSensoresId { get; set; }

        [Column("nombre_parametro")]
        [JsonPropertyName("nombre_parametro")]
        public string NombreParametro { get; set; }

        [Column("fecha_dato")]
        [JsonPropertyName("fecha_dato")]
        public DateTime FechaDato { get; set; }


        [Column("valor_numero")]
        [JsonPropertyName("valor_numero")]
        public decimal ValorNumero { get; set; }

        public Parameter Parameter { get; set; }

    }
}
