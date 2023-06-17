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
        public required int  RoomRegulationId { get; set; }
        [JsonConstructor]
        public RoomDetailToCreateDTO(double price, string? roomType, string? description, string? image, int roomRegulationId)
        {
            Price = price;
            RoomType = roomType;
            Description = description;
            Image = image;
            RoomRegulationId = roomRegulationId;
        }
    }
}
