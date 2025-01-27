namespace SMS.Dtos.Stock
{
    public class StockImportDto
    {
        public string Title { get; set; }
        
        public string Description { get; set; }

        public string TransportType { get; set; }

        public double Gauge { get; set; }

        public string  CompanyId { get; set; }
    }
}
