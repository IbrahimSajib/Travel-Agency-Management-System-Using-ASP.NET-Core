using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Travel_Agency_Management_System.Models.ViewModels
{
    public class ClientVM
    {
        public ClientVM()
        {
            this.SpotList = new List<int>();
        }
        public int ClientId { get; set; }
        [Required, StringLength(50), Display(Name = "Client Name")]
        public string ClientName { get; set; } = default!;
        [Required, Column(TypeName = "date"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true), Display(Name = "Date Of Birth")]
        public DateTime DateOfBirth { get; set; }
        public string Phone { get; set; }
        public string? Image { get; set; } = default!;
        [Display(Name = "Image")]
        public IFormFile? ImagePath { get; set; }
        [DisplayName("IsMarried?")]
        public bool IsMarried { get; set; }
        public List<int> SpotList { get; set; }
    }
}
