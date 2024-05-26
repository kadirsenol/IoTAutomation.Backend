using IoTAutomation.EntityLayer.Concrete;
using IoTAutomation.EntityLayer.EntityConfig.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IoTAutomation.EntityLayer.EntityConfig.Concrete
{
    public class ProfileImageConfig : BaseEntityConfig<ProfileImage, int>
    {
        public override void Configure(EntityTypeBuilder<ProfileImage> builder)
        {
            base.Configure(builder);
            builder.HasIndex(p => p.UserId).IsUnique().HasFilter("[IsDelete] = 0");

        }
    }
}
