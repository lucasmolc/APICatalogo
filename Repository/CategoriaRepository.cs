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

    public async Task<PagedList<Categoria>> GetCategorias(CategoriasParameters categoriaParameters)
    {
        return await PagedList<Categoria>.ToPagedList(Get().OrderBy(on => on.CategoriaId), categoriaParameters.PageNumber, categoriaParameters.PageSize);
    }

    public async Task<IEnumerable<Categoria>> GetCategoriasProdutos()
    {
        return await Get().Include(p => p.Produtos).AsNoTracking().ToListAsync();
    }
}
