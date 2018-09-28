using FriendProximityAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FriendProximityAPI.Infrastructure.Mappings
{
    public class FriendMap : IEntityTypeConfiguration<Friend>
    {
        public void Configure(EntityTypeBuilder<Friend> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder.OwnsOne(c => c.Point)
                .HasForeignKey(p => p.Id);

            builder.Ignore(c => c.Notifications);
            builder.Ignore(c => c.Invalid);
            builder.Ignore(c => c.Valid);
        }
    }
}
