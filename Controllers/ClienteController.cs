using Microsoft.AspNetCore.Mvc;
using SistemaCliente.Models;
using SistemaCliente.DTOs;
using AutoMapper;
using SistemaCliente.Services;
using SistemaCliente.Data;
using SistemaCliente.Profiles;
using Microsoft.EntityFrameworkCore;

namespace SistemaCliente.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClienteController : ControllerBase
{

    private readonly AppDbContext _context;
    private readonly IMapper _mapper;
    private readonly InterfaceViaCepService _viaCepService;

    public ClienteController(AppDbContext context, IMapper mapper, InterfaceViaCepService viaCepService)
    {
        _context = context;
        _mapper = mapper;
        _viaCepService = viaCepService;
    }

    // Metodo POST para um novo cliente
    [HttpPost]
public async Task<IActionResult> CriarCliente([FromBody] ClienteCreateDTO clienteDto)
{
    if (!ModelState.IsValid)
        return BadRequest(ModelState);

    if (clienteDto.Endereco == null)
        return BadRequest("Endereço não fornecido.");

    // Consulta o CEP via serviço
    var viaCepResponse = await _viaCepService.BuscarEndereco(clienteDto.Endereco.Cep);

    if (viaCepResponse == null)
        return BadRequest("CEP inválido ou não encontrado.");

    // Monta o endereço com dados do ViaCEP + número e complemento do DTO
    var endereco = new Endereco
    {
        Cep = viaCepResponse.Cep,
        Logradouro  = viaCepResponse.Logradouro ,
        Cidade = viaCepResponse.Localidade,
        Numero = clienteDto.Endereco.Numero,
        Complemento = clienteDto.Endereco.Complemento
    };

    // Cria o cliente completo
    var cliente = new Cliente
    {
        Nome = clienteDto.Nome,
        DataCadastro = clienteDto.DataCadastro,
        Endereco = endereco,
        Contatos = clienteDto.Contatos.Select(c => new Contato
        {
            Tipo = c.Tipo,
            Texto = c.Texto
        }).ToList()
    };

    _context.Clientes.Add(cliente);
    await _context.SaveChangesAsync();

    // Retorna DTO mapeado para evitar ciclos de referência na serialização
    var clienteRead = _mapper.Map<ClienteReadDTO>(cliente);
    return CreatedAtAction(nameof(GetById), new { id = cliente.Id }, clienteRead);
}


    // Metodo GET para listar todos os clientes
    [HttpGet]
    public IActionResult GetAll()
    {
        var clientes = _context.Clientes
            .Include(c => c.Endereco)
            .Include(c => c.Contatos)
            .ToList();

        var clientesRead = _mapper.Map<List<ClienteReadDTO>>(clientes);
        return Ok(clientesRead);
    }

    // Metodo GET para listar cliente por id
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var cliente =  _context.Clientes
            .Include(c => c.Endereco)
            .Include(c => c.Contatos)
            .FirstOrDefault(c => c.Id == id);

        if (cliente == null)
            return NotFound($"Cliente com o id {id} não encontrado.");

        var clienteRead = _mapper.Map<ClienteReadDTO>(cliente);
        return Ok(clienteRead);
    }

    // Atualiza Cliente pelo ID
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] ClienteCreateDTO clienteDto)
    {
        if (clienteDto == null)
            return BadRequest("Dados do cliente não fornecidos.");

        if (clienteDto.Endereco == null)
            return BadRequest("Endereço não fornecido.");

        var cliente = await _context.Clientes
            .Include(c => c.Endereco)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (cliente == null)
            return NotFound($"Cliente com id {id} não encontrado.");

        // Atualiza o nome
        cliente.Nome = clienteDto.Nome;
        
        // Verifica se endereco é null
        var enderecoAtual = cliente.Endereco;
        var cepAtual = enderecoAtual?.Cep ?? string.Empty;

        // Atualiza o endereço se o CEP mudou
        if (!string.Equals(cepAtual, clienteDto.Endereco.Cep, StringComparison.OrdinalIgnoreCase))
           {
                var EnderecoCreateDTO = await _viaCepService.BuscarEndereco(clienteDto.Endereco.Cep);
                if (EnderecoCreateDTO != null)
                {
                    var novoEndereco = _mapper.Map<Endereco>(EnderecoCreateDTO);
                // mantém clienteId como chave estrangeira
                    novoEndereco.ClienteId = cliente.Id;
                    cliente.Endereco = novoEndereco;
                }
            else
                {
                    return BadRequest("CEP inválido ao tentar atualizar.");
                }
            }

        await _context.SaveChangesAsync();
        var clienteRead = _mapper.Map<ClienteReadDTO>(cliente);
        return Ok(clienteRead); 
    }

    // Remover o CLiente
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var cliente = _context.Clientes
            .Include(c => c.Endereco)
            .Include(c => c.Contatos)
            .FirstOrDefault(c => c.Id == id);

        if (cliente == null)
            return NotFound($"Cliente com id {id} não encontrado.");

        _context.Clientes.Remove(cliente);
        _context.SaveChanges();

        return NoContent();
    }


}
