using ProvaMedGroup.DomainModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProvaMedGroup.DomainModel.Interfaces.Repositories
{
    public interface IContatoRepository : IRepository<Contato>
    {
        Task<IEnumerable<Contato>> GetContatos();

        Task<Contato> GetContato(Guid id);

    }
}
