using Microsoft.AspNetCore.Http;
using System.Text.Json.Serialization;

namespace SensorApp.DAO.MeteoDAO
{ 
    public class Values
    {

        [JsonPropertyName("avg_data")]
        public List<decimal> AvgData { get; set; } = new List<decimal>();

        [JsonPropertyName("min_data")]
        public List<decimal> MinData { get; set; } = new List<decimal>();

        [JsonPropertyName("max_data")]
        public List<decimal> MaxData { get; set; } = new List<decimal>();
    }

    public class DeviceData
    {
        [JsonPropertyName("codigo_parametro")]
        public string CodigoParametro { get; set; } =  string.Empty;

        [JsonPropertyName("nombre_parametro")]
        public string NombreParametro { get; set; } = string.Empty;

        [JsonPropertyName("unidad_parametro")]
        public string UnidadParametro { get; set; } = string.Empty;

        [JsonPropertyName("abreviacion_parametro")]
        public string AbreviacionParametro { get; set; } = string.Empty;

        [JsonPropertyName("values")]
        public Values? Values { get; set; }
    }



    public class MeteoResponse
    {

        [JsonPropertyName("device_dates")]
        public List<string> DeviceDates { get; set; } = null!;

        [JsonPropertyName("device_data")]
        public List<DeviceData> DeviceData { get; set; } = null!;
    }
}