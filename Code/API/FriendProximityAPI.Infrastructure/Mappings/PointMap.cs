using FriendProximityAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FriendProximityAPI.Infrastructure.Mappings
{
    public class PointMap : IEntityTypeConfiguration<Point>
    {
        public void Configure(EntityTypeBuilder<Point> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Latitude)
                .IsRequired();

            builder.Property(c => c.Longitude)
                .IsRequired();

            builder.Ignore(c => c.Notifications);
            builder.Ignore(c => c.Invalid);
            builder.Ignore(c => c.Valid);
        }
    }
}
