using CRUD_REST_API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRUD_REST_API.Configurations
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.Property(x => x.Title).IsRequired().HasMaxLength(150);
            builder.Property(x => x.Genre).IsRequired().HasMaxLength(50);
            builder.Property(x => x.PublishedYear).IsRequired();
            builder.HasOne(b => b.Author).WithMany(x => x.Books).HasForeignKey(x => x.AuthorId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
