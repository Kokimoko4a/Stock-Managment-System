namespace SMS.Dtos.Order
{
    public class OrderImportDto
    {
        public string Title { get; set; }


        public string Description { get; set; }


        public List<string> Stock { get; set; }



        public string DriverEmail { get; set; }




        public string ComapanyId { get; set; }



        public string Destination { get; set; }

       

        public string StartPoint { get; set; }

        public string TypeOrder { get; set; }

    }
}
