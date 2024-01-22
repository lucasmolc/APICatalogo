using ApiCatalogo.Context;
using ApiCatalogo.Models;
using ApiCatalogo.Pagination;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ApiCatalogo.Repository;

public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
{
    public CategoriaRepository(AppDbContext contexto) : base(contexto)
    {
    }

    public PagedList<Categoria> GetCategorias(CategoriasParameters categoriaParameters)
    {
        return PagedList<Categoria>.ToPagedList(Get().OrderBy(on => on.CategoriaId), categoriaParameters.PageNumber, categoriaParameters.PageSize);
    }

    public IEnumerable<Categoria> GetCategoriasProdutos()
    {
        return Get().Include(p => p.Produtos).AsNoTracking();
    }
}
