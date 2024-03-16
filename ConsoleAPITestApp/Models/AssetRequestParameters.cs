namespace ConsoleAPITestApp.Models
{
    public class AssetRequestParameters
    {
        public string Search { get; set; }
        public string[] Ids { get; set; }
        public int? Limit { get; set; }
        public int? Offset { get; set; }
    }

}
