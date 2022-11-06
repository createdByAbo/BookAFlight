using System;

using BookAFlight.Context;
using BookAFlight.JWT;
using BookAFlight.Models.DTOs;
using BookAFlight.Entities;
using Microsoft.EntityFrameworkCore;


namespace BookAFlight.Services
{
    public interface IFleetService
    {
        //HTTP get
        Fleet GetAircraftById(int id);
        Fleet GetAircraftByRegistration(string registration);
        List<Fleet> GetAircraftsByIds(List<int> ids);
        List<Fleet> GetAircraftsByRegistrations(List<string> registrations);
        List<Fleet> GetAllAircrafts();

        //Http post
        void AddAircraft(Fleet aircraft);

        //HTTP delete
        void DeleteAircraftById(int id);
        void DeleteAircraftsByIds(List<int> ids);

        //HTTP patch
        Fleet UpdateAircraftData(Fleet newAircraftData);

        //HTTP put
        Fleet ReplaceAircraftData(Fleet AircrafrData);
    }

    public class FleetService : IFleetService
    {
        private readonly devEnvDbContext _context;

        public FleetService(devEnvDbContext context)
        {
            _context = context;
        }

        void AddAircraft(Fleet aircraft)
        {
            throw new NotImplementedException();
        }

        void IFleetService.AddAircraft(Fleet aircraft)
        {
            throw new NotImplementedException();
        }

        void DeleteAircraftById(int id)
        {
            throw new NotImplementedException();
        }

        void IFleetService.DeleteAircraftById(int id)
        {
            throw new NotImplementedException();
        }

        void DeleteAircraftsByIds(List<int> ids)
        {
            throw new NotImplementedException();
        }

        void IFleetService.DeleteAircraftsByIds(List<int> ids)
        {
            throw new NotImplementedException();
        }

        Fleet GetAircraftById(int id)
        {
            throw new NotImplementedException();
        }

        Fleet IFleetService.GetAircraftById(int id)
        {
            throw new NotImplementedException();
        }

        Fleet GetAircraftByRegistration(string registration)
        {
            throw new NotImplementedException();
        }

        Fleet IFleetService.GetAircraftByRegistration(string registration)
        {
            throw new NotImplementedException();
        }

        List<Fleet> GetAircraftsByIds(List<int> ids)
        {
            throw new NotImplementedException();
        }

        List<Fleet> IFleetService.GetAircraftsByIds(List<int> ids)
        {
            throw new NotImplementedException();
        }

        List<Fleet> GetAircraftsByRegistrations(List<string> registrations)
        {
            throw new NotImplementedException();
        }

        List<Fleet> IFleetService.GetAircraftsByRegistrations(List<string> registrations)
        {
            throw new NotImplementedException();
        }

        List<Fleet> GetAllAircrafts()
        {
            throw new NotImplementedException();
        }

        List<Fleet> IFleetService.GetAllAircrafts()
        {
            throw new NotImplementedException();
        }

        Fleet ReplaceAircraftData(Fleet AircrafrData)
        {
            throw new NotImplementedException();
        }

        Fleet IFleetService.ReplaceAircraftData(Fleet AircrafrData)
        {
            throw new NotImplementedException();
        }

        Fleet UpdateAircraftData(Fleet newAircraftData)
        {
            throw new NotImplementedException();
        }

        Fleet IFleetService.UpdateAircraftData(Fleet newAircraftData)
        {
            throw new NotImplementedException();
        }
    }
}

