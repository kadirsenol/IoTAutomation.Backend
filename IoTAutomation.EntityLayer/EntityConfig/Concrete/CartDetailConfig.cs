using IoTAutomation.EntityLayer.Concrete;
using IoTAutomation.EntityLayer.EntityConfig.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IoTAutomation.EntityLayer.EntityConfig.Concrete
{
    public class CartDetailConfig : BaseEntityConfig<CartDetail, int>
    {
        public override void Configure(EntityTypeBuilder<CartDetail> builder)
        {
            base.Configure(builder);
            builder.Property(p => p.Quantity).HasDefaultValue(1);
            builder.HasIndex(p => new { p.UserId, p.SolutionId }).IsUnique().HasFilter("[IsDelete] = 0"); //Silinen urunleri tekrar yukleyebilmesi icin IsDelete yi de filtre olarak birlestirdim.
        }
    }
}
