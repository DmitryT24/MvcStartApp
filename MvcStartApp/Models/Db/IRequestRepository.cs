using System.Threading.Tasks;

namespace MvcStartApp.Models.Db
{
    public interface IRequestRepository
    {
        Task AddLog(Request request);
        Task<Request[]> GetLogs();
    }
}
