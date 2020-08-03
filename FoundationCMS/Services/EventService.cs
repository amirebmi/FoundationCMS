using FoundationCMS.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoundationCMS.Services
{
    public interface IEventService
    {
        List<Event> GetEvents();
        Event GetEvent(int id);
        void AddEvent(Event e);
        void InviteDonor(EventDonor ed);
        void DeleteEvent(Event e);
        void SaveChanges();

        List<Donor> GetInvitedDonors(int id);
        Donor GetInvitedDonor(int id);
        List<Donor> GetPresentDonors(int eventId);
        List<Donor> GetAbsentDonors(int eventId);

    }
    public class EventService : IEventService
    {
        private readonly AppDbContext _db;

        public EventService(AppDbContext db)
        {
            _db = db;
        }

        public List<Event> GetEvents()
        {
            return _db.Events.ToList();
        }

        public Event GetEvent(int id)
        {
            return _db.Events.Find(id);
        }

        public void AddEvent(Event e)
        {
            var newEvent = new Event
            {
                EventName = e.EventName,
                EndAt = e.EndAt,
                StartAt = e.StartAt,
                Location = e.Location
            };

            _db.Events.Add(newEvent);
            _db.SaveChanges();

            
        }

        // Invite guests (donors)
        public void InviteDonor(EventDonor ed)
        {
            _db.EventDonors.Add(ed);
            _db.SaveChanges();
        }
        public void DeleteEvent(Event e)
        {
            _db.Events.Remove(e);
            _db.SaveChanges();
        }

        public void SaveChanges()
        {
            _db.SaveChanges();
        }



        public List<Donor> GetInvitedDonors(int eventId)
        {
            return _db.EventDonors.Where(e => e.EventId == eventId).Include(e => e.Donor).Select(e => e.Donor).ToList();
        }

        public Donor GetInvitedDonor(int donorId)
        {
            return _db.EventDonors.Where(e => e.DonorId == donorId).Select(e => e.Donor).SingleOrDefault();
        }

        public List<Donor> GetPresentDonors(int eventId)
        {
            return _db.EventDonors.Where(e => e.EventId == eventId).Where(e => e.IsPresent == true).Include(e => e.Donor).Select(e => e.Donor).ToList();
        }

        public List<Donor> GetAbsentDonors(int eventId)
        {
            return _db.EventDonors.Where(e => e.EventId == eventId).Where(e => e.IsPresent == false).Include(e => e.Donor).Select(e => e.Donor).ToList();
        }
    }
}
