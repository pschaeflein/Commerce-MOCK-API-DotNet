namespace MSCorp.CrestMockWebApi.Models.Errors
{
    public class AdditionalError
    {
        public string error_code { get; set; }
        public string message { get; set; }
        public Parameter parameters { get; set; }
    }
}