namespace Domain.Core.Settings
{
    public record OtlpSettings
    {
        public string Endpoint { get; set; }
        public string ServiceName { get; set; }
        public string ServiceVersion { get; set; }

        public OtlpSettings()
        {

        }
    }
}
