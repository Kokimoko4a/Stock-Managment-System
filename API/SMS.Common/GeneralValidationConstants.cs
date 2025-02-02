namespace SMS.Common
{
    public static class GeneralValidationConstants
    {
        public static class Vehicle
        {

            public const int RegistrationNumberMaxLength = 8;

            public const int RegistrationNumberMinLength = 4;

            public const double LongtitudeMaxValue = 180;
                         
            public const double LongtitudeMinValue = -180;
                         
            public const double LatitudeMaxValue = 90;
                         
            public const double LatitudeMinValue = -90;
                         
            public const double LoadCapacityMaxValue = 100000;
                         
            public const double LoadCapacityMinValue = 200;
                         
            public const double TravelledKmMinValue = 0;
                         
            public const double TraveledKmMaxValue = 99999999;
                         
            public const double ReservoirCapacityMaxValue = 1500000;
                         
            public const double ReservoirCapacityMinValue = 100;

            public const int BrandMaxLength = 20;

            public const int BrandMinLength = 2;

            public const int ModelMaxLength = 20;

            public const int ModelMinLength = 2;
        }

        public static class Stock
        {

            public const int DescriptionMaxLength = 30;

            public const int DescriptionMinLength = 5;

            public const int TitleMaxLenth = 15;

            public const int TitleMinLength = 3;

            public const double GaugeMaxValue = 160000;

            public const double GaugeMinValue = 100;
        }

        public static class Order
        {

            public const int StartPointMaxLength = 30;

            public const int StartPointMinLength = 3;

            public const int DestinationMaxLength = 30;

            public const int DestinationMinLength = 3;

            public const int TitleMaxLength = 30;

            public const int TitleMinLength = 5;

            public const int DescriptionMaxLength = 200;

            public const int DescriptionMinLength = 20;
        }

        public static class Company
        {
            public const int DescriptionMaxLength = 30;

            public const int DescriptionMinLength = 5;

            public const int TitleMaxLenth = 15;

            public const int TitleMinLength = 3;
        }

        public static class ApplicationUser
        { 
            public const int FirstNameMaxLength = 30;

            public const int FirstNameMinLength = 3;

            public const int LastNameMaxLength = 30;

            public const int LastNameMinLength = 3;

            public const int AgeMaxValue = 64;

            public const int AgeMinValue = 25;
        }

    }
}
