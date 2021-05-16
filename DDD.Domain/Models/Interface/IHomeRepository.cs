using DDD.Domain.Models.ViewModels;

namespace DDD.Domain.Model.Interface
{
    public interface IHomeRepository
    {
        public HomeViewModel GetAll();
    }
}
