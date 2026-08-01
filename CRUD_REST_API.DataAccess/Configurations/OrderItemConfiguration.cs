using CRUD_REST_API.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRUD_REST_API.DataAccess.Configurations
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.Property(x => x.Quantity).IsRequired();

            builder.Property(x => x.UnitPrice)
                   .HasColumnType("decimal(18,2)")
                   .IsRequired();

            builder.HasOne(oi => oi.Book)
                   .WithMany()
                   .HasForeignKey(oi => oi.BookId);
        }
    }
}
