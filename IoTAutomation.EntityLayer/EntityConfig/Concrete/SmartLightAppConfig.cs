using IoTAutomation.EntityLayer.Concrete;
using IoTAutomation.EntityLayer.EntityConfig.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IoTAutomation.EntityLayer.EntityConfig.Concrete
{
    public class SmartLightAppConfig : BaseEntityConfig<SmartLightApp, int>
    {
        public override void Configure(EntityTypeBuilder<SmartLightApp> builder)
        {
            base.Configure(builder);
            builder.Property(p => p.LightMode).HasDefaultValue(false);
            builder.Property(p => p.Active).HasDefaultValue(false);
            builder.Property(p => p.EspIp).HasMaxLength(50);
            builder.HasIndex(p => p.EspIp).IsUnique().HasFilter("[IsDelete] = 0 AND [EspIp] IS NOT NULL");

        }
    }
}
