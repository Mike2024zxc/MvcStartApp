using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcStartApp.Data.Models.Db.Repositories
{
    public class RequestRepository : IRequestRepository
    {
        private readonly BlogContext _context;

        public RequestRepository(BlogContext context)
        {
            _context = context;
        }
        public async Task AddRequest(Request request)
        {

            var model = _context.Entry(request);
            if (model.State == EntityState.Detached)
                await _context.Requests.AddAsync(request);
            await _context.SaveChangesAsync();
        }
        /// <summary>
        /// Получаем список Log
        /// </summary>
        /// <returns></returns>
        public Task<Request[]> GetRequest()
        {
            return _context.Requests.ToArrayAsync();
        }
    }
}
