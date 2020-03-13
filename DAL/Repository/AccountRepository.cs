using DAL.Interface;
using DataModel.Data;
using DataModel.Context;
using System.Collections.Generic;
using System.Linq;
namespace DAL.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly ApplicationContext _context;
        public AccountRepository(ApplicationContext context)
        {
            _context = context;
        }
    }
}