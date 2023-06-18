using Hotel.DataAccess.Entities;
using System.Linq.Expressions;

using System.Text;
using System.Threading.Tasks;
using Hotel.DataAccess.ObjectValues;

namespace Hotel.DataAccess.Repositories.IRepositories
{
    public interface IRoomRevenueRepository
    {
        Task <IEnumerable<RoomRevenue>> BrowserAsync();
        Task<IEnumerable<RoomRevenue>> FindAsync(int month, int year);
    }
}
