using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System;

namespace MvcStartApp.Models.Db
{
    public class RequestRepository : IRequestRepository
    {
        // ссылка на контекст
        private readonly BlogContext _context;

        // Метод-конструктор для инициализации
        public RequestRepository(BlogContext context)
        {
            _context = context;
        }
        public async Task AddLog(Request request)
        {
            request.Date = DateTime.Now;
            request.Id = Guid.NewGuid();

            // Добавление пользователя
            var entry = _context.Entry(request);
            if (entry.State == EntityState.Detached)
                await _context.Requests.AddAsync(request);

            // Сохранение изенений
            await _context.SaveChangesAsync();
        }
        public async Task<Request[]> GetLogs()
        {
            // Получим Log
            return await _context.Requests.ToArrayAsync();
        }
    }
}
