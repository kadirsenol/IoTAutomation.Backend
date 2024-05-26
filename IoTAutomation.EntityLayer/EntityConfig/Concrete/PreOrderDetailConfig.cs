using IoTAutomation.EntityLayer.Concrete;
using IoTAutomation.EntityLayer.EntityConfig.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IoTAutomation.EntityLayer.EntityConfig.Concrete
{
    public class PreOrderDetailConfig : BaseEntityConfig<PreOrderDetail, int>
    {
        public override void Configure(EntityTypeBuilder<PreOrderDetail> builder)
        {
            base.Configure(builder);
            builder.HasIndex(p => new { p.PreOrderId, p.SolutionId }).IsUnique().HasFilter("[IsDelete] = 0"); //Silinen urunleri tekrar yukleyebilmesi icin IsDelete yi de filtre olarak birlestirdim.

        }
    }
}
