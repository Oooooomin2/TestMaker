using DDD.Domain.ViewModels.Home;

namespace DDD.Domain.Model.Interface.Home
{
    public interface IHomeRepository
    {
        public HomeViewModel GetAll();
    }
}
