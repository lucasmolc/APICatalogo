using ApiCatalogo.Models;
using ApiCatalogo.Pagination;

namespace ApiCatalogo.Repository;

public interface IProdutoRepository : IRepository<Produto>
{
    Task<IEnumerable<Produto>> GetprodutosPorPreco();

    Task<PagedList<Produto>> GetProdutos(ProdutosParameters produtosParameters);
}
