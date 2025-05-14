using System.ComponentModel.DataAnnotations;

namespace SPL_Diplom_Winki_Trippi_Sabi.Models
{
    public class Service
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        // Bild-URL hinzufügen:
        public string ImageUrl { get; set; }
    }

}
