using BookAFlight.Entities;
using BookAFlight.Models.DTOs;

namespace BookAFlight.Models;

public class AircraftUpdateDtoToFleetEntityConverter
{
    public static Fleet Convert(AircraftUpdateDTO dto)
    {
        Fleet aircraft = new Fleet();
        aircraft.Id = dto.Id;
        aircraft.Brand = dto.Brand;
        aircraft.Model = dto.Model;
        aircraft.NumberOfServiceSeats = dto.NumberOfServiceSeats;
        aircraft.NumberOfBusinessClassSeats = dto.NumberOfBusinessClassSeats;
        aircraft.NumberOfEconomicClassSeats = dto.NumberOfEconomicClassSeats;
        aircraft.NumberOfFirstClassSeats = dto.NumberOfFirstClassSeats;
        aircraft.Registry = dto.Registry;
        return aircraft;
    }
}