using System;
using System.Linq;
using System.Windows.Forms;
using NotesApp.Data;
using NotesApp.Models;

namespace NotesApp.Forms
{
    public partial class MainForm : Form
    {
        private NotesContext _context;

        public MainForm()
        {
            InitializeComponent();
            _context = new NotesContext();
            _context.Database.EnsureCreated();
            LoadNotes();
        }

        private void LoadNotes()
        {
            listBoxNotes.DataSource = _context.Notes.ToList();
            listBoxNotes.DisplayMember = "Title";
        }

        private void ListBoxNotes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxNotes.SelectedItem != null)
            {
                Note selectedNote = (Note)listBoxNotes.SelectedItem;
                textBoxTitle.Text = selectedNote.Title;
                textBoxContent.Text = selectedNote.Content;
                labelCreationDate.Text = $"Date de creation: {selectedNote.CreationDate}";
                labelModificationDate.Text = $"Date de modification: {(selectedNote.ModificationDate.HasValue ? selectedNote.ModificationDate.Value.ToString() : "N/A")}";
            }
        }

        private void ButtonAdd_Click(object sender, EventArgs e)
        {
            var note = new Note
            {
                Title = textBoxTitle.Text,
                Content = textBoxContent.Text,
                CreationDate = DateTime.Now
            };
            _context.Notes.Add(note);
            _context.SaveChanges();
            LoadNotes();
        }

        private void ButtonUpdate_Click(object sender, EventArgs e)
        {
            if (listBoxNotes.SelectedItem != null)
            {
                Note selectedNote = (Note)listBoxNotes.SelectedItem;
                selectedNote.Title = textBoxTitle.Text;
                selectedNote.Content = textBoxContent.Text;
                selectedNote.ModificationDate = DateTime.Now;
                _context.SaveChanges();
                LoadNotes();
            }
        }

        private void ButtonDelete_Click(object sender, EventArgs e)
        {
            if (listBoxNotes.SelectedItem != null)
            {
                Note selectedNote = (Note)listBoxNotes.SelectedItem;
                _context.Notes.Remove(selectedNote);
                _context.SaveChanges();
                LoadNotes();
            }
        }
    }
}
