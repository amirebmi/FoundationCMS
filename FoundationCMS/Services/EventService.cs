using FoundationCMS.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FoundationCMS.Services
{
    public interface IEventService
    {
        List<Event> GetEvents();    // Used to display list of events
        Event GetEvent(int id);     // Used to get a specific event
        void AddEvent(Event e);     // Used to add donor(s)
        void InviteDonor(EventDonor ed);    // Used to invite donor into EventDonor

        void DeleteEvent(Event e);
        void SaveChanges();     // Used to make a SaveChanges()

        List<Donor> GetInvitedDonors(int eventId);
        Donor GetInvitedDonor(int donorId, int eventId);

        void Uninvite(int eventId, int donorId);
        bool IsInvited(int donorId, int eventId);
        List<Donor> GetPresentDonors(int eventId);
        List<Donor> GetAbsentDonors(int eventId);

        List<EventDonor> GetEventDonors();

        void Present(int eventId, int donorId);

        void Absent(int eventId, int donorId);

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
            

            _db.Events.Add(e);
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
            var invitedDonors = _db.EventDonors.Where(e => e.EventId == eventId).Include(e => e.Donor).Select(e => e.Donor).ToList();

            return invitedDonors;
        }

        public Donor GetInvitedDonor(int donorId, int eventId)
        {
            return _db.EventDonors.Where(e => e.DonorId == donorId).Where(c => c.EventId == eventId).Select(e => e.Donor).SingleOrDefault(); 
        }

        public void Uninvite(int eventId, int donorId)
        {
            var x = _db.EventDonors.Where(e => (e.EventId == eventId))
                                   .Where(d => (d.DonorId == donorId)).SingleOrDefault();

            _db.EventDonors.Remove(x);

            _db.SaveChanges();
        }

        public bool IsInvited(int donorId, int eventId)
        {
            var result = _db.EventDonors.Where(e => e.DonorId == donorId && e.EventId == eventId).Select(e => e.Donor).SingleOrDefault();

            if (result == null)
                return false;
            return true;
        }

        public List<Donor> GetPresentDonors(int eventId)
        {
            return _db.EventDonors.Where(e => e.EventId == eventId).Where(e => e.IsPresent == true).Include(e => e.Donor).Select(e => e.Donor).ToList();
        }

        public List<Donor> GetAbsentDonors(int eventId)
        {
            return _db.EventDonors.Where(e => e.EventId == eventId).Where(e => e.IsPresent == false).Include(e => e.Donor).Select(e => e.Donor).ToList();
        }

        public List<EventDonor> GetEventDonors()
        {
            return _db.EventDonors.ToList();
        }

        public void Present(int eventId, int donorId)
        {
            //var donor = _db.EventDonors.Where(e => (e.EventId == eventId))
            //                           .Where(e => (e.DonorId == donorId)).SingleOrDefault();

            var donor = _db.EventDonors.Where(e => e.EventId == eventId && e.DonorId == donorId).SingleOrDefault();
            donor.IsPresent = true;

            _db.EventDonors.Update(donor);
            _db.SaveChanges(); 
        }

        public void Absent(int eventId, int donorId)
        {
            var donor = _db.EventDonors.Where(e => (e.EventId == eventId))
                                       .Where(e => (e.DonorId == donorId)).SingleOrDefault();
            donor.IsPresent = false;

            _db.EventDonors.Update(donor);
            _db.SaveChanges();
        }
    }
}
