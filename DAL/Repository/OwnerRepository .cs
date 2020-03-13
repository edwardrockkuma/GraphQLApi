using DAL.Interface;
using DataModel.Data;
using DataModel.Context;
using System.Collections.Generic;
using System.Linq;
namespace DAL.Repository
{
    public class OwnerRepository : IOwnerRepository
    {
        private readonly ApplicationContext _context;
        public OwnerRepository(ApplicationContext context)
        {
            _context = context;
        }
        public IEnumerable<Owner> GetAll() => _context.Owners.ToList();
    }
}