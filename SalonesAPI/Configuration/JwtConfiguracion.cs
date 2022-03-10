namespace SalonesAPI.Configuration
{
    public class JwtConfiguracion
    {
        public string service { get; set; }
        public string secret { get; set; }
        public string connectionString { get; set; }
        public string[] audience { get; set; }
    }
}
