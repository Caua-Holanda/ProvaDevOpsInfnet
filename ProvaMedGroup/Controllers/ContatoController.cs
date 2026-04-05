using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProvaMedGroup.DomainModel.Entities;
using ProvaMedGroup.DomainModel.Interfaces.Services;
using ProvaMedGroup.DomainService;
using ProvaMedGroup.ViewModel;
using ProvaMedGroup.ViewModels;

namespace ProvaMedGroup.Controllers
{

    [ApiController]
    [Route("api/[Controller]")]
    public class ContatoController : ControllerBase
    {
        private readonly IContatoService _ContatosService;
        private readonly IMapper _mapper;

        public ContatoController(IContatoService ContatosService, IMapper mapper)
        {
            _ContatosService = ContatosService;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<IEnumerable<ContatoViewModel>> ListarContato()
        {
            return _mapper.Map<IEnumerable<ContatoViewModel>>(await _ContatosService.ListarContatos());
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ContatoViewModel>> ListarContatoId(Guid id)
        {
            var dados = _mapper.Map<ContatoViewModel>(await _ContatosService.ListarContatoId(id));
            if (dados == null) return NotFound();

            return Ok(dados);
        }

        [HttpPost]
        public async Task<ActionResult<ContatoViewModel>> AdicionarContato([FromBody] ContatoViewModel contatoViewModel)
        {
            if (!ModelState.IsValid) return BadRequest();

                return Ok(await _ContatosService.AdicionarContato(_mapper.Map<ContatoViewModel, Contato>(contatoViewModel)));

        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<ContatoViewModel>> AtualizarContato(Guid id, [FromBody] ContatoViewModel ContatoViewModel)
        {
            if (!ModelState.IsValid) return BadRequest();
            var contato = _mapper.Map<ContatoViewModel, Contato>(ContatoViewModel);
            contato.Id = id;
            return Ok(await _ContatosService.AtualizarContato(contato));
        }

        [HttpPatch("{id:guid}")]
        public async Task<ActionResult<ContatoViewModel>> AtualizarContatoAtivo(Guid id)
        {
            var contato = await _ContatosService.ListarContatoId(id);
            if (contato == null) return NotFound();
            contato.Id = id;
            var resultado = await _ContatosService.AtualizarContatoAtivo(contato);
            return Ok(resultado);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<bool>> DeletarContato(Guid id)
        {
  
            return Ok(await _ContatosService.DeletarContato(id));
        }

    }
}
