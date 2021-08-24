using System.ComponentModel.DataAnnotations;
using static QuestionsOfRuneterra.Data.DataConstants.Room;

namespace QuestionsOfRuneterra.Models.Rooms
{
    public class RoomServiceModel
    {
        public string Id { get; set; }

        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string Name { get; set; }
    }
}
