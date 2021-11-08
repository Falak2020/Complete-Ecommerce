using E_Commerce_Api.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Data
{
    public class SqlContext : DbContext
    {
        public SqlContext(DbContextOptions<SqlContext> options) : base(options)
        {
        }
        public virtual DbSet<CategoryEntity> Categories { get; set; }
        public virtual DbSet<SubCategoryEntity> SubCategories { get; set; }
        public virtual DbSet<ProductEntity> Products { get; set; }
        public virtual DbSet<PasswordHashEntity> PasswordHashes { get; set; }


        public virtual DbSet<AddressEntity> Addresses { get; set; }
        public virtual DbSet<UserEntity> Users { get; set; }
        public DbSet<OrderEntity> Orders { get; set; }
        public DbSet<DeliveryTypeEntity> DeliveryTypes { get; set; }
        public DbSet<OrderItemEntity> OrderItems { get; set; }
        public DbSet<UserAddressEntity>UserAddresses { get; set; }

    }
}
