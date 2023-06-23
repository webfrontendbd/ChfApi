﻿using ChfApi.Entities;

namespace ChfApi.Dtos
{
    public class DoctorDto
    {
        public int Id { get; set; }
        public string DoctorName { get; set; }
        public string Gender { get; set; }
        public string Phone { get; set; }
        public string Specialist { get; set; }
        public bool IsActivated { get; set; }
        public ICollection<Booking> Bookings { get; set; }
    }
}
