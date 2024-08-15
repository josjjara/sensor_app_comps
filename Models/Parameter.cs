using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SensorApp.Models
{
    public class Parameter
    {

        [Key]
        [Column("id")]
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [Column("id_codigo")]
        [JsonPropertyName("id_codigo")]
        public int CodigoParametro { get; set; }

        [Column("descripcion_larga")]
        [JsonPropertyName("descripcion_larga")]
        public string DescripcionLarga { get; set; }

        [Column("descripcion_med")]
        [JsonPropertyName("descripcion_med")]
        public string DescripcionMed { get; set; }

        [Column("descripcion_corta")]
        [JsonPropertyName("descripcion_corta")]
        public string DescripcionCorta { get; set; }

        [Column("abreviacion")]
        [JsonPropertyName("abreviacion")]
        public string Abreviacion { get; set; }

        [Column("observacion")]
        [JsonPropertyName("observacion")]
        public string Observacion { get; set; }

        [Column("fecha_creacion")]
        [JsonPropertyName("fecha_creacion")]
        public DateTime FechaCreacion { get; set; }

        [Column("fecha_modificacion")]
        [JsonPropertyName("fecha_modificacion")]
        public DateTime FechaModificacion { get; set; }

        [Column("estado")]
        [JsonPropertyName("estado")]
        public char Estado { get; set; }

        [Column("unidad")]
        [JsonPropertyName("unidad")]
        public string? Unidad { get; set; }


        public ICollection<SensorData> SensorDatas { get; set; }

    }
}
