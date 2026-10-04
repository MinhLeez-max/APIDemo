using System;
using System.Collections.Generic;

namespace APIdemo.DTOs
{
    public class CourseDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool IsPublished { get; set; }
    }
}
