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
    public async Task<IActionResult> Create([FromBody] ClienteCreateDTO clienteDto)
    {   
        if (string.IsNullOrWhiteSpace(clienteDto.Endereco?.Cep))
        return BadRequest("O CEP é obrigatório.");

        //Consulta ViaCEP
        var cep = clienteDto.Endereco.Cep;
        var enderecoAPI = await _viaCepService.BuscarEndereco(cep);
        if (enderecoAPI == null){
            return BadRequest("CEP inválido ou não encontrado.");
        }
       
        //Mapeia DTOs para o Model
        var cliente = _mapper.Map<Cliente>(clienteDto);
        
        var endereco = new Endereco{
            Cep = clienteDto.Endereco.Cep ?? string.Empty,
            Logadouro = enderecoAPI.Logradouro,
            Cidade = enderecoAPI.Cidade,
            Numero = clienteDto.Endereco.Numero ?? string.Empty,
            Complemento = clienteDto.Endereco.Complemento ?? string.Empty,
            ClienteId = cliente.Id
        };

        cliente.Endereco = endereco;

        //Salva no banco
        await _context.Clientes.AddAsync(cliente);
        await _context.SaveChangesAsync();

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
