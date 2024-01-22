using ApiCatalogo.Context;
using ApiCatalogo.Models;
using ApiCatalogo.Pagination;

namespace ApiCatalogo.Repository;

public class ProdutoRepository : Repository<Produto>, IProdutoRepository
{
    public ProdutoRepository(AppDbContext contexto) : base(contexto)
    {
    }

    public PagedList<Produto> GetProdutos(ProdutosParameters produtosParameters)
    {
        return PagedList<Produto>.ToPagedList(Get().OrderBy(on => on.ProdutoId), produtosParameters.PageNumber, produtosParameters.PageSize);
    }

    public IEnumerable<Produto> GetprodutosPorPreco()
    {
        return Get().OrderBy(c => c.Preco).ToList();
    }
}
