using ApiCatalogo.DTOs;
using ApiCatalogo.Models;
using ApiCatalogo.Pagination;
using ApiCatalogo.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace ApiCatalogo.Controllers;

[Route("[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class CategoriasController : ControllerBase
{
    private readonly IUnitOfWork _uof;
    private readonly IMapper _mapper;

    public CategoriasController(IUnitOfWork context, IMapper mapper)
    {
        _uof = context;
        _mapper = mapper;
    }

    [HttpGet]
    public ActionResult<IEnumerable<CategoriaDTO>> Get([FromQuery] CategoriasParameters categoriaParameters)
    {
        try
        {
            var categorias = _uof.CategoriaRepository.GetCategorias(categoriaParameters);

            var metadata = new
            {
                categorias.TotalCount,
                categorias.PageSize,
                categorias.CurrentPage,
                categorias.TotalPages,
                categorias.HasNext,
                categorias.HasPrevious
            };

            if (categorias is null)
                return NotFound("Categorias não encontradas...");

            Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(metadata));

            var categoriasDTO = _mapper.Map<List<CategoriaDTO>>(categorias);

            return categoriasDTO;
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Ocorreu um erro ao executar sua solicitação.");
        }
    }

    [HttpGet("{id:int:min(1)}", Name = "ObterCategoria")]
    public ActionResult<CategoriaDTO> Get(int id)
    {
        try
        {
            var categoria = _uof.CategoriaRepository.GetById(c => c.CategoriaId == id);

            if (categoria is null)
                return NotFound($"Categoria com Id= {id} não encontrada...");

            var categoriaDTO = _mapper.Map<CategoriaDTO>(categoria);

            return categoriaDTO;
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Ocorreu um erro ao executar sua solicitação.");
        }
    }

    [HttpGet("produtos")]
    public ActionResult<IEnumerable<CategoriaDTO>> GetCategoriasProdutos()
    {
        try
        {
            var produtosInCategoria = _uof.CategoriaRepository.GetCategoriasProdutos().ToList();

            if (produtosInCategoria is null)
                return NotFound("Não existem produtos na categoria selecionada!");

            var produtosInCategoriaDTO = _mapper.Map<List<CategoriaDTO>>(produtosInCategoria);

            return produtosInCategoriaDTO;
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Ocorreu um erro ao executar sua solicitação.");
        }
    }

    [HttpPost]
    public ActionResult Post(CategoriaDTO categoriaDTO)
    {
        try
        {
            if (categoriaDTO is null)
                return BadRequest("Categoria Vazia ou Incorreta!");

            var categoria = _mapper.Map<Categoria>(categoriaDTO);

            _uof.CategoriaRepository.Add(categoria);
            _uof.Commit();

            var categoriaDto = _mapper.Map<CategoriaDTO>(categoriaDTO);

            return new CreatedAtRouteResult("ObterCategoria",
                new { id = categoria.CategoriaId }, categoriaDto);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Ocorreu um erro ao executar sua solicitação.");
        }
    }

    [HttpPut("{id:int:min(1)}")]
    public ActionResult Put(int id, CategoriaDTO categoriaDTO)
    {
        try
        {
            if (id != categoriaDTO.CategoriaId)
                return BadRequest("Id da categoria não confere com a categoria informada!");

            var categoria = _mapper.Map<Categoria>(categoriaDTO);

            _uof.CategoriaRepository.Update(categoria);
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
    public ActionResult<CategoriaDTO> Delete(int id)
    {
        try
        {
            var categoria = _uof.CategoriaRepository.GetById(c => c.CategoriaId == id);

            if (categoria is null)
                return NotFound($"Categoria com Id= {id} inexistente.");

            _uof.CategoriaRepository.Delete(categoria);
            _uof.Commit();

            var categoriaDTO = _mapper.Map<CategoriaDTO>(categoria);

            return categoriaDTO;
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Ocorreu um erro ao executar sua solicitação.");
        }
    }
}
