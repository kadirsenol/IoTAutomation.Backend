using IoTAutomation.EntityLayer.Concrete;
using IoTAutomation.EntityLayer.EntityConfig.Abstract;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IoTAutomation.EntityLayer.EntityConfig.Concrete
{
    public class PreOrderConfig : BaseEntityConfig<PreOrder, int>
    {
        public override void Configure(EntityTypeBuilder<PreOrder> builder)
        {
            base.Configure(builder);
        }
    }
}
