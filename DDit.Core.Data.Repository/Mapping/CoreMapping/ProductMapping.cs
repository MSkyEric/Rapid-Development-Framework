using DDit.Core.Data.Entity.CoreEntity;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDit.Core.Data.Repository.Mapping.CoreMapping
{
    class ProductMapping : EntityTypeConfiguration<Product>
    {
        public ProductMapping()
        {
            HasKey(A => A.ProductId);
            this.ToTable("Product", "Test");
            this.Property(a => a.ProductId).HasColumnName("ProductId");
            this.Property(a => a.ProductName).HasColumnName("ProductName");
            this.Property(a => a.ProductDesc).HasColumnName("ProductDesc");
            this.Property(a => a.CreateTime).HasColumnName("CreateTime");

        }
    }
}
