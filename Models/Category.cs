using System;

namespace APIdemo.Models
{
    public class Category
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string Slug { get; set; }
    }
}
