using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using DataModel.Data;

namespace DataModel.Context
{
    public class OwnerContextConfiguration : IEntityTypeConfiguration<Owner>
    {
        private Guid[] _ids;
        public OwnerContextConfiguration(Guid[] ids)
        {
            _ids = ids;
        }
        public void Configure(EntityTypeBuilder<Owner> builder)
        {
            builder
              .HasData(
                new Owner
                {
                    Id = _ids[0],
                    Name = "John ",
                    Address = "John's address"
                },
                new Owner
                {
                    Id = _ids[1],
                    Name = "Jane ",
                    Address = "Jane's address"
                }
            );
        }
    }
}