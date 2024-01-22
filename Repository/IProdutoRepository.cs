using ApiCatalogo.Models;
using ApiCatalogo.Pagination;

namespace ApiCatalogo.Repository;

public interface IProdutoRepository : IRepository<Produto>
{
    public IEnumerable<Produto> GetprodutosPorPreco();

    public PagedList<Produto> GetProdutos(ProdutosParameters produtosParameters);
}
