using ProvaMedGroup.DomainModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProvaMedGroup.DomainModel.Interfaces.Services
{
    public interface IContatoService
    {
        Task<Contato> AdicionarContato(Contato contato);
        Task<Contato> AtualizarContato(Contato contatos);
        Task<Contato> AtualizarContatoAtivo(Contato contato);
        Task<bool> DeletarContato(Guid id);
        Task<Contato> ListarContatoId(Guid id);
        Task<IEnumerable<Contato>> ListarContatos();

    }
}
