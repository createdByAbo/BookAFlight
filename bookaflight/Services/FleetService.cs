using BookAFlight.Context;
using BookAFlight.Entities;
using BookAFlight.Exceptions;
using BookAFlight.Models;
using BookAFlight.Models.DTOs;
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
        void DeleteAircraftById(Fleet aircraft);
        void DeleteAircraftsByIds(List<int> ids);

        //HTTP patch
        Fleet UpdateAircraftData(Fleet newAircraftData);

        //HTTP put
        Fleet ReplaceAircraftData(AircraftUpdateDTO aircraftData);
    }

    public class FleetService : IFleetService
    {
        private readonly devEnvDbContext _context;

        public FleetService(devEnvDbContext context)
        {
            _context = context;
        }

        public void AddAircraft(Fleet aircraft)
        {
            _context.Fleets.Add(aircraft);
            _context.SaveChanges();
        }

        public void DeleteAircraftById(Fleet aircraft)
        {
            try
            {
                _context.Remove(aircraft);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new NotFoundException($"airctaft with id: {aircraft.Id} not found");
            }
            catch (DbUpdateException)
            {
                throw new DbConflictException($"aircraft with id: {aircraft.Id} has already flights");
            }
            catch (Exception)
            {
                throw new ApplicationException();
            }
        }

        public void DeleteAircraftsByIds(List<int> ids)
        {
            for (int i = 0; i <= ids.Count; i=+1)
            {
                try
                {
                    _context.Remove(ids[i]);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw new NotFoundException($"airctaft with id: {ids[i]} not found");
                }
                catch (DbUpdateException)
                {
                    throw new DbConflictException($"aircraft with id: {ids[i]} has already flights");
                }
                catch (Exception)
                {
                    throw new ApplicationException();
                }
            }
        }

        public Fleet GetAircraftById(int id)
        {
            var returnedAircraft = from aircraft in _context.Fleets
                                   where aircraft.Id == id
                                   select aircraft;
            if (returnedAircraft.FirstOrDefault() == null) { throw new ArgumentException(); }
            return returnedAircraft.FirstOrDefault();
        }

        public Fleet GetAircraftByRegistration(string registration)
        {
            var returnedAircraft = from aircraft in _context.Fleets
                                   where aircraft.Registry == registration
                                   select aircraft;
            if (returnedAircraft.FirstOrDefault() == null) { throw new ArgumentException(); }
            return returnedAircraft.FirstOrDefault();
        }

        public List<Fleet> GetAircraftsByIds(List<int> ids)
        {
            var returnedAircrafts = from aircraft in _context.Fleets
                                   where ids.Contains(aircraft.Id)
                                   select aircraft;
            if (returnedAircrafts.ToList() == null) { throw new ArgumentException(); }
            return returnedAircrafts.ToList();
        }

        public List<Fleet> GetAircraftsByRegistrations(List<string> registrations)
        {
            var returnedAircrafts = from aircraft in _context.Fleets
                                   where registrations.Contains(aircraft.Registry)
                                   select aircraft;
            if (returnedAircrafts.ToList() == null) { throw new ArgumentException(); }
            return returnedAircrafts.ToList();
        }

        public List<Fleet> GetAllAircrafts()
        {
            var returnedAircrafts = from aircraft in _context.Fleets
                                   select aircraft;
            if (returnedAircrafts.ToList() == null) { throw new ArgumentException(); }
            return returnedAircrafts.ToList();
        }

        public Fleet ReplaceAircraftData(AircraftUpdateDTO aircraftData)
        {
            //var Aircraft = _context.Fleets
              //  .Where(aircraft => aircraft.Id == aircraftData.Id)
                //.ToList();
            //if (Aircraft == null)
            //{
              //  throw new NotFoundException($"Not Found Aircraft with id: {aircraftData.Id}");
            //}

            _context.Fleets.Update(AircraftUpdateDtoToFleetEntityConverter.Convert(aircraftData));

            //Aircraft[0] = AircraftUpdateDtoToFleetEntityConverter.Convert(aircraftData);
            _context.SaveChangesAsync();
            return AircraftUpdateDtoToFleetEntityConverter.Convert(aircraftData);
        }

        public Fleet UpdateAircraftData(Fleet newAircraftData)
        {
            throw new NotImplementedException();
        }
    }
}

