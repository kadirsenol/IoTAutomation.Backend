using AutoMapper;
using IoTAutomation.EntityLayer.Concrete;
using IoTAutomation.EntityLayer.Concrete.VMs.AdminVms.SmartLightVms;
using IoTAutomation.EntityLayer.Concrete.VMs.AdminVms.SolutionVms;
using IoTAutomation.EntityLayer.Concrete.VMs.AdminVms.UserVms;
using IoTAutomation.EntityLayer.Concrete.VMs.ProfileVm;
using IoTAutomation.EntityLayer.Concrete.VMs.UserVM;

namespace IoTAutomation.WebApiLayer.MyExtensions.AutoMapper
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<UserLoginVm, User>().ReverseMap();
            CreateMap<UserRegisterVm, User>().ReverseMap();
            CreateMap<SaveProfileImageVm, ProfileImage>().ReverseMap();
            CreateMap<UserUpdateVm, User>().ReverseMap();
            CreateMap<UserInsertVm, User>().ReverseMap();
            CreateMap<UpdateSolutionVm, Solution>().ReverseMap();
            CreateMap<InsertSolutionVm, Solution>().ReverseMap();
            CreateMap<UpdateSmartLightAppVm, SmartLightApp>().ReverseMap();
            CreateMap<InsertSmartLightAppVm, SmartLightApp>().ReverseMap();

        }
    }
}
