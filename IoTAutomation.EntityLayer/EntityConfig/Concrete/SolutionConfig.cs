using IoTAutomation.EntityLayer.Concrete;
using IoTAutomation.EntityLayer.EntityConfig.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IoTAutomation.EntityLayer.EntityConfig.Concrete
{
    public class SolutionConfig : BaseEntityConfig<Solution, int>
    {
        public override void Configure(EntityTypeBuilder<Solution> builder)
        {
            base.Configure(builder);
            builder.Property(p => p.Name).HasMaxLength(50);
            builder.Property(p => p.Price).HasDefaultValue(1);
            builder.Property(p => p.Image).HasMaxLength(50);
            builder.HasCheckConstraint("CK_PriceRange", "[Price] >= 1 AND [Price] <= 99999");
            builder.HasIndex(p => p.Name).IsUnique().HasFilter("[IsDelete] = 0");

        }
    }
}
