using Moq;
using ProvaMed.DomainModel.Interfaces.UoW;
using ProvaMedGroup.DomainModel.Entities;
using ProvaMedGroup.DomainModel.Exceptions;
using ProvaMedGroup.DomainModel.Interfaces.Repositories;
 using ProvaMedGroup.DomainService;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

public class ContatoServiceTests
{
    private readonly Mock<IContatoRepository> _contatoRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly ContatoService _contatoService;

    public ContatoServiceTests()
    {
        _contatoRepositoryMock = new Mock<IContatoRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _contatoService = new ContatoService(_contatoRepositoryMock.Object, _unitOfWorkMock.Object);
    }

    [Fact(DisplayName = "Adicionar Contato - Sucesso")]
    public async Task AdicionarContato_Sucesso()
    {
        var contato = new Contato
        {
            Nome = "Cau� Holanda",
            DataNascimento = DateTime.Now.AddYears(-20) // 20 anos
        };

        _contatoRepositoryMock.Setup(r => r.Create(contato)).Verifiable();
 
        var resultado = await _contatoService.AdicionarContato(contato);

        Assert.NotNull(resultado);
        Assert.True(resultado.Ativo);
        _contatoRepositoryMock.Verify(r => r.Create(contato));
        _unitOfWorkMock.Verify(u => u.CommitAsync());
    }

    [Fact(DisplayName = "Adicionar Contato - Falha por Idade")]
    public async Task AdicionarContato_FalhaPorIdade()
    {
        var contato = new Contato
        {
            Nome = "Cau� Holanda",
            DataNascimento = DateTime.Now.AddYears(-10) // 10 anos
        };

        await Assert.ThrowsAsync<TratedExceptions>(() => _contatoService.AdicionarContato(contato));
    }

    [Fact(DisplayName = "Adicionar Contato - Falha por Data Futura")]
    public async Task AdicionarContato_FalhaPorDataFutura()
    {
        var contato = new Contato
        {
            Nome = "Cau� Holanda",
            DataNascimento = DateTime.Now.AddDays(1) // Data futura
        };

        await Assert.ThrowsAsync<TratedExceptions>(() => _contatoService.AdicionarContato(contato));
    }

    [Fact(DisplayName = "Adicionar Contato - Falha por Menos de 1 Ano")]
    public async Task AdicionarContato_FalhaPorMenosDeUmAno()
    {
        var contato = new Contato
        {
            Nome = "Cau� Holanda",
            DataNascimento = DateTime.Now.AddMonths(-6) // Menos de 1 ano
        };

        await Assert.ThrowsAsync<TratedExceptions>(() => _contatoService.AdicionarContato(contato));
    }

    [Fact(DisplayName = "Atualizar Contato - Sucesso")]
    public async Task AtualizarContato_Sucesso()
    {
        var contato = new Contato
        {
            Id = Guid.NewGuid(),
            Nome = "Cau� Holanda",
            DataNascimento = DateTime.Now.AddYears(-20), // 20 anos
            Ativo = true
        };

        _contatoRepositoryMock.Setup(r => r.Update(contato)).Verifiable();

        var resultado = await _contatoService.AtualizarContato(contato);

        Assert.NotNull(resultado);
        _contatoRepositoryMock.Verify(r => r.Update(contato));
        _unitOfWorkMock.Verify(u => u.CommitAsync());
    }

    [Fact(DisplayName = "Atualizar Contato Ativo - Sucesso")]
    public async Task AtualizarContatoAtivo_Sucesso()
    {
        var contato = new Contato
        {
            Ativo = false
        };

        _contatoRepositoryMock.Setup(r => r.Update(contato)).Verifiable();

        var resultado = await _contatoService.AtualizarContatoAtivo(contato);

        Assert.NotNull(resultado);
        Assert.True(resultado.Ativo);
        _contatoRepositoryMock.Verify(r => r.Update(contato), Times.Once);
        _unitOfWorkMock.Verify(u => u.CommitAsync(), Times.Once);
    }

    [Fact(DisplayName = "Atualizar Contato - Falha por Idade")]
    public async Task AtualizarContato_FalhaPorIdade()
    {
        var contato = new Contato
        {
            Id = Guid.NewGuid(),
            Nome = "Cau� Holanda",
            DataNascimento = DateTime.Now.AddYears(-10), // 10 anos
     
        };

        await Assert.ThrowsAsync<TratedExceptions>(() => _contatoService.AtualizarContato(contato));
    }

    [Fact(DisplayName = "Atualizar Contato - Falha por Inativo")]
    public async Task AtualizarContato_FalhaInatividade()
    {
        var contato = new Contato
        {
            Id = Guid.NewGuid(),
            Nome = "Cau� Holanda",
            DataNascimento = DateTime.Now.AddYears(-20),
            Ativo = false
        };

        await Assert.ThrowsAsync<TratedExceptions>(() => _contatoService.AtualizarContato(contato));
    }

    [Fact(DisplayName = "Listar Contato por ID - Sucesso")]
    public async Task ListarContatoPorId_Sucesso()
    {
        var contatoId = Guid.NewGuid();
        var contato = new Contato { Id = contatoId, Nome = "Cau� Holanda", Ativo = true };

        _contatoRepositoryMock.Setup(r => r.Read(contatoId)).ReturnsAsync(contato);

        var resultado = await _contatoService.ListarContatoId(contatoId);

        Assert.NotNull(resultado);
        Assert.Equal(contatoId, resultado.Id);
    }

    [Fact(DisplayName = "Deletar Contato - Sucesso")]
    public async Task DeletarContato_Sucesso()
    {
        var contatoId = Guid.NewGuid();

        _contatoRepositoryMock.Setup(r => r.Delete(contatoId)).Verifiable();
 
        var resultado = await _contatoService.DeletarContato(contatoId);

        Assert.True(resultado);
        _contatoRepositoryMock.Verify(r => r.Delete(contatoId));
        _unitOfWorkMock.Verify(u => u.CommitAsync());
    }
}
