using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace QuestionsOfRuneterra.Models.Uploads
{
    public class UploadServiceModel
    {
        [Required]
        public string ToRoomId { get; set; }
        [Required]
        public IFormFile File { get; set; }
    }
}
