using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class DataAccessService
    {
        private readonly Func<BankAppDataV2Context> _contextFactory;
        public DataAccessService (Func<BankAppDataV2Context> contextFactory)
        {
            _contextFactory = contextFactory;
        }
        public BankAppDataV2Context GetDbContext()
        {
            return _contextFactory();
        }
    }
}
