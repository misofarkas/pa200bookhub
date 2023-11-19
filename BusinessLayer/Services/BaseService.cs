using DataAccessLayer.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class BaseService : IBaseService
    {
        private readonly BookHubDBContext _dBContext;

        public BaseService(BookHubDBContext dBContext)
        {
            _dBContext = dBContext;
        }

        public async Task SaveAsync(bool save)
        {
            if (save)
            {
                await _dBContext.SaveChangesAsync();
            }
        }
    }
}
