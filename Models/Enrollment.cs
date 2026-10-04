using System;

namespace APIdemo.Models
{
    public class Enrollment
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid CourseId { get; set; }
        public Guid UserId { get; set; }
        public DateTime EnrolledAt { get; set; } = DateTime.UtcNow;
        public double Progress { get; set; }
        public string Status { get; set; }

        public Course Course { get; set; }
        public User User { get; set; }
    }
}
