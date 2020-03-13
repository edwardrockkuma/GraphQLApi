
using System.Collections.Generic;
using DataModel.Data;

namespace DAL.Interface
{
    public interface IOwnerRepository
    {
        IEnumerable<Owner> GetAll();
    }
}