using BookAFlight.Context;
using BookAFlight.Entities;


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
        Fleet ReplaceAircraftData(Fleet AircraftData);
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

        public void DeleteAircraftById(int id)
        {
            var aircraftId = new Fleet() { Id = id };
            _context.Fleets.Remove(aircraftId);
            _context.SaveChanges();
        }

        public void DeleteAircraftsByIds(List<int> ids)
        {
            throw new NotImplementedException();
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

        public Fleet ReplaceAircraftData(Fleet AircrftData)
        {
            throw new NotImplementedException();
        }

        public Fleet UpdateAircraftData(Fleet newAircraftData)
        {
            throw new NotImplementedException();
        }
    }
}

