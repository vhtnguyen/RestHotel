using Hotel.BusinessLogic.DTO.RoomRegulation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.BusinessLogic.DTO.RoomDetail
{
    public class RoomDetailToUpdateDTO
    {
        public int Id { get; set; }
        public double Price { get; set; }
        public string? RoomType { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public int roomRegulationID { get; set; }
        public RoomDetailToUpdateDTO(int id, double price, string? roomType, string? description, string? image)
        {
            Id = id;
            Price = price;
            RoomType = roomType;
            Description = description;
            Image = image;
        }
    }
}
