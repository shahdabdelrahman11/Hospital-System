using Hospital_System.Data;
using Hospital_System.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class AppointmentsController : Controller
{
    private readonly ApplicationDbContext _context = new ApplicationDbContext();

   
    [HttpGet]
    public IActionResult Book(int doctorId)
    {
        var doctor = _context.Doctors.Find(doctorId);
        if (doctor == null) return NotFound();

        ViewBag.Slots = GenerateSlots();
        return View(doctor);
    }

    
    [HttpPost]
    public IActionResult Book(int doctorId, string patientName, DateTime date, TimeSpan time)
    {
        var doctor = _context.Doctors.Find(doctorId);
        if (doctor == null) return NotFound();

       
        if (date.DayOfWeek == DayOfWeek.Friday || date.DayOfWeek == DayOfWeek.Saturday)
            return BackWithError(doctor, patientName, date, time,
                "Appointments are only available from Sunday to Thursday.");

        if (date.Date < DateTime.Today)
            return BackWithError(doctor, patientName, date, time,
                "You cannot book a date in the past.");

        
        if (!GenerateSlots().Contains(time))
            return BackWithError(doctor, patientName, date, time,
                "Please choose a valid time slot (9:00 AM - 4:30 PM).");

       
        bool isBooked = _context.Appointments.Any(a =>
            a.DoctorId == doctorId &&
            a.Date == date.Date &&
            a.Time == time);

        if (isBooked)
            return BackWithError(doctor, patientName, date, time,
                "This time slot is already booked for this doctor. Please choose another time.");

        var appointment = new Appointment
        {
            DoctorId = doctorId,
            PatientName = patientName,
            Date = date.Date,
            Time = time
        };

        _context.Appointments.Add(appointment);
        _context.SaveChanges();

        return RedirectToAction("Index");
    }

  
    public IActionResult Index()
    {
        var appointments = _context.Appointments
            .Include(a => a.Doctor)
            .OrderBy(a => a.Date)
            .ThenBy(a => a.Time)
            .ToList();

        return View(appointments);
    }

  
    private IActionResult BackWithError(Doctor doctor, string patientName, DateTime date, TimeSpan time, string error)
    {
        ViewBag.Error = error;
        ViewBag.PatientName = patientName;
        ViewBag.Date = date.ToString("yyyy-MM-dd");
        ViewBag.Time = time.ToString(@"hh\:mm");
        ViewBag.Slots = GenerateSlots();
        return View("Book", doctor);
    }

  
    private static List<TimeSpan> GenerateSlots()
    {
        var slots = new List<TimeSpan>();
        for (var t = new TimeSpan(9, 0, 0); t <= new TimeSpan(16, 30, 0); t = t.Add(TimeSpan.FromMinutes(30)))
            slots.Add(t);
        return slots;
    }
}