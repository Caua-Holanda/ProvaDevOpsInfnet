using System.Threading.Tasks;

namespace ProvaMed.DomainModel.Interfaces.UoW
{
    public interface IUnitOfWork
    {
        Task<int> CommitAsync();
    }
}
