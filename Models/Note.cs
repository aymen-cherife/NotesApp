using System;

namespace NotesApp.Models
{
    public class Note
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public DateTime? ModificationDate { get; set; }
    }
}
