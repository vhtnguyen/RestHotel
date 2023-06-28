using Hotel.BusinessLogic.DTO.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.BusinessLogic.DTO.Users
{
    public class ListUserToReturnDTO
    {
        public int TotalPage { get; set; }
        public int PageSize { get; set; }
        public int Page { get; set; }
        public List<UserToReturnDTO>? Values { get; set; }

        public ListUserToReturnDTO(int totalPage, int pageSize, int page)
        {
            TotalPage = totalPage;
            Page = page;
            PageSize = pageSize;
            Values = null;

        }
        public void AddList(List<UserToReturnDTO> roomlist)
        {
            Values = new List<UserToReturnDTO>(roomlist);
        }
    }
}