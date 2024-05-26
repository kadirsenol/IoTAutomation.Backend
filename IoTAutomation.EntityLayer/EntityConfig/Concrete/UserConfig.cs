using IoTAutomation.EntityLayer.Concrete;
using IoTAutomation.EntityLayer.EntityConfig.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IoTAutomation.EntityLayer.EntityConfig.Concrete
{
    public class UserConfig : BaseEntityConfig<User, int>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            base.Configure(builder);
            builder.Property(p => p.Ad).HasMaxLength(50);
            builder.Property(p => p.Soyad).HasMaxLength(50);
            builder.Property(p => p.Email).HasMaxLength(50);
            builder.Property(p => p.Password).HasMaxLength(50);
            builder.Property(p => p.TcNo).HasMaxLength(11);
            builder.Property(p => p.Rol).HasMaxLength(15);
            builder.Property(p => p.isConfirmEmail).HasDefaultValue(false);
            builder.HasIndex(p => p.Email).IsUnique().HasFilter("[IsDelete] = 0"); // Soft delete ile silinen hesap tekrar kayıt olmak isterse diye IsDelete ile beraber uniq tanimladim.
        }
    }
}
