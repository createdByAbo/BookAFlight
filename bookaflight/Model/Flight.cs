using System;
namespace bookaflight.Model
{
    public class Flight
    {
        public int Id { get; set; } = 0;

        public string startCity { get; set; }
        public string startAirport { get; set; }

        public string endCity { get; set; }
        public string endAirport { get; set; }

        public List<string> betweenApproaches { get; set; }

        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }

        public string flightCode { get; set; }

        public int numberOfFirstClassSeats { get; set; }
        public int numberOfBuisnessClassSeats { get; set; }
        public int numberOfEconomicClassSeats { get; set; }


        public Flight(
                string startCity,
                string startAirport,
                string endCity,
                string endAirport,
                List<string> betweenApproaches,
                DateTime startDate,
                DateTime endDate,
                string flightCode
        )
        {
            this.Id = Id += 1;
            this.startCity = startCity;
            this.startAirport = startAirport;
            this.endCity = endCity;
            this.endAirport = endAirport;
            this.betweenApproaches = betweenApproaches;
            this.startDate = startDate;
            this.endDate = endDate;
            this.flightCode = flightCode;
        }
    }
}
