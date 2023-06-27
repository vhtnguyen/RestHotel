using Hotel.BusinessLogic.DTO.RoomDetail;
using Hotel.BusinessLogic.DTO.RoomRegulation;
using Hotel.DataAccess.Entities;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

namespace Hotel.BusinessLogic.DTO.Rooms
{
    public class RoomToReturnQueryPerPageDTO
    {
        public int TotalPage { get; set; }
        public int PageSize { get; set; }
        public int Page { get; set; }
        public List<RoomToReturnListDTO>? Values { get; set; }

        public RoomToReturnQueryPerPageDTO(int totalPage,  int pageSize, int page)
        {
            TotalPage = totalPage;
            Page = page;
            PageSize = pageSize;
            Values = null;

        }
        public void AddList(List<RoomToReturnListDTO> roomlist)
        {
            Values = new List<RoomToReturnListDTO>(roomlist);
        }
    }
}
