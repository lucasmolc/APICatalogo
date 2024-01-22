using ApiCatalogo.DTOs;
using ApiCatalogo.Models;
using ApiCatalogo.Pagination;
using ApiCatalogo.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ApiCatalogo.Controllers;

[Route("[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class ProdutosController : ControllerBase
{
    private readonly IUnitOfWork _uof;
    private readonly IMapper _mapper;

    public ProdutosController(IUnitOfWork context, IMapper mapper)
    {
        _uof = context;
        _mapper = mapper;
    }

    [HttpGet]
    public ActionResult<IEnumerable<ProdutoDTO>> Get([FromQuery] ProdutosParameters produtosParameters)
    {
        try
        {
            var produtos = _uof.ProdutoRepository.GetProdutos(produtosParameters);

            var metadata = new
            {
                produtos.TotalCount,
                produtos.PageSize,
                produtos.CurrentPage,
                produtos.TotalPages,
                produtos.HasNext,
                produtos.HasPrevious
            };

            if (produtos is null)
                return NotFound("Produtos não encontrados...");

            Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(metadata));

            var produtosDTO = _mapper.Map<List<ProdutoDTO>>(produtos);

            return produtosDTO;
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Ocorreu um erro ao executar sua solicitação.");
        }
    }

    [HttpGet("{id:int:min(1)}", Name = "ObterProduto")]
    public ActionResult<ProdutoDTO> Get(int id)
    {
        try
        {
            var produto = _uof.ProdutoRepository.GetById(p=> p.ProdutoId == id);

            if (produto is null)
                return NotFound($"Produto com Id= {id} não encontrado...");

            var produtoDTO = _mapper.Map<ProdutoDTO>(produto);

            return produtoDTO;
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Ocorreu um erro ao executar sua solicitação.");
        }
    }

    [HttpGet("menorpreco")]
    public ActionResult<IEnumerable<ProdutoDTO>> GetProdutosPrecos()
    {
        var produtos = _uof.ProdutoRepository.GetprodutosPorPreco().ToList();
        var produtosDTO = _mapper.Map<List<ProdutoDTO>>(produtos);

        return produtosDTO;
    }

    [HttpPost]
    public ActionResult Post(ProdutoDTO produtoDTO)
    {
        try
        {
            if (produtoDTO is null)
                return BadRequest("Produto Vazio ou Incorreto!");

            var produto = _mapper.Map<Produto>(produtoDTO);

            _uof.ProdutoRepository.Add(produto);
            _uof.Commit();

            var produtoDto = _mapper.Map<ProdutoDTO>(produto);

            return new CreatedAtRouteResult("ObterProduto",
                new { id = produto.ProdutoId }, produtoDto);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Ocorreu um erro ao executar sua solicitação.");
        }
    }

    [HttpPut("{id:int:min(1)}")]
    public ActionResult Put(int id, Produto produtoDTO)
    {
        try
        {
            if (id != produtoDTO.ProdutoId)
                return BadRequest("Id do produto não confere com o produto informado!");

            var produto = _mapper.Map<Produto>(produtoDTO);

            _uof.ProdutoRepository.Update(produto);
            _uof.Commit();

            return Ok();
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Ocorreu um erro ao executar sua solicitação.");
        }
    }

    [HttpDelete("{id:int:min(1)}")]
    public ActionResult<ProdutoDTO> Delete(int id)
    {
        try
        {
            var produto = _uof.ProdutoRepository.GetById(p => p.ProdutoId == id);

            if (produto is null)
                return NotFound($"Produto com o Id= {id} inexistente.");

            _uof.ProdutoRepository.Delete(produto);
            _uof.Commit();

            var produtoDTO = _mapper.Map<ProdutoDTO>(produto);

            return produtoDTO;
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Ocorreu um erro ao executar sua solicitação.");
        }
    }
}
