using System;

namespace APIdemo.Models
{
    public class Lesson
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid CourseId { get; set; }
        public string Title { get; set; }
        public string ContentUrl { get; set; }
        public int DurationSeconds { get; set; }
        public int Order { get; set; }

        public Course Course { get; set; }
    }
}
