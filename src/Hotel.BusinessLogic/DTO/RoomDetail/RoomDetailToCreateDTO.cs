using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Hotel.BusinessLogic.DTO.RoomDetail
{
    public class RoomDetailToCreateDTO
    {
        public double Price { get; set; }
        public string? RoomType { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        [JsonConstructor]
        public RoomDetailToCreateDTO(int id, double price, string? roomType, string? description, string? image)
        {

            Price = price;
            RoomType = roomType;
            Description = description;
            Image = image;
        }
    }
}
