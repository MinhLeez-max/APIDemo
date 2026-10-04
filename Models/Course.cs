using System;
using System.Collections.Generic;

namespace APIdemo.Models
{
    public class Course
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public Guid? CategoryId { get; set; }
        public Guid? InstructorId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public bool IsPublished { get; set; } = false;

        public Category Category { get; set; }
        public User Instructor { get; set; }
        public ICollection<Lesson> Lessons { get; set; }
    }
}
