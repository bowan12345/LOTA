using LOTA.Model.DTO;

namespace LOTA.Service.Service.IService
{
    public interface IPortalContextService
    {
        string GetCurrentArea();
        string GetCurrentPortalName();
        bool IsAuthenticated();
        string GetCurrentRole();
    }
}
