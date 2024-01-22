using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiCatalogo.Migrations
{
    /// <inheritdoc />
    public partial class PopulaProdutos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder mb)
        {
            //Lanches - 1
            mb.Sql("Insert into Produtos(Nome, Descricao, Preco, ImagemUrl, Estoque, DataCadastro, CategoriaId) " +
                "Values('Simples', 'Hamburguer de 200g, bacon em tiras e queijo cheddar', 28.00, 'lanchesimples.jpg', 50, now(), 1)");
            mb.Sql("Insert into Produtos(Nome, Descricao, Preco, ImagemUrl, Estoque, DataCadastro, CategoriaId) " +
                "Values('Duplo', '2x Hamburguers de 200g, bacon em tiras e queijo cheddar', 38.00, 'lancheduplo.jpg', 50, now(), 1)");
            mb.Sql("Insert into Produtos(Nome, Descricao, Preco, ImagemUrl, Estoque, DataCadastro, CategoriaId) " +
                "Values('Triplo', '3x Hamburguers de 200g, bacon em tiras e queijo cheddar', 48.00, 'lanchetriplo.jpg', 50, now(), 1)");

            //Bebidas - 2
            mb.Sql("Insert into Produtos(Nome, Descricao, Preco, ImagemUrl, Estoque, DataCadastro, CategoriaId) " +
                "Values('Coca-Cola Zero', 'Lata - 350ml', 6.00, 'cocacolazero.jpg', 50, now(), 2)");
            mb.Sql("Insert into Produtos(Nome, Descricao, Preco, ImagemUrl, Estoque, DataCadastro, CategoriaId) " +
                "Values('Coca-Cola', 'Lata - 350ml', 6.00, 'cocacola.jpg', 50, now(), 2)");
            mb.Sql("Insert into Produtos(Nome, Descricao, Preco, ImagemUrl, Estoque, DataCadastro, CategoriaId) " +
                "Values('Fanta Uva', 'Lata - 350ml', 6.00, 'fantauva.jpg', 50, now(), 2)");
            mb.Sql("Insert into Produtos(Nome, Descricao, Preco, ImagemUrl, Estoque, DataCadastro, CategoriaId) " +
                "Values('RedBull', 'Lata - 475ml', 18.00, 'redbull.jpg', 50, now(), 2)");

            //Sobremesas - 3
            mb.Sql("Insert into Produtos(Nome, Descricao, Preco, ImagemUrl, Estoque, DataCadastro, CategoriaId) " +
                "Values('Brownie', 'Brownie de Chocolate Artesanal com 1g de ervas frescas Paraguaias', 20.00, '.jpg', 50, now(), 3)");
            mb.Sql("Insert into Produtos(Nome, Descricao, Preco, ImagemUrl, Estoque, DataCadastro, CategoriaId) " +
                "Values('Petit Gâteau', 'Petit Gâteau Artesanal importado dos Alpes Africanos', 30.00, '.jpg', 50, now(), 3)");
            mb.Sql("Insert into Produtos(Nome, Descricao, Preco, ImagemUrl, Estoque, DataCadastro, CategoriaId) " +
                "Values('Milk-Shake', 'Milk-Shake Artesanal feito com leite de carneiro Indígena', 35.00, '.jpg', 50, now(), 3)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder mb)
        {
            mb.Sql("Delete from Produtos");
        }
    }
}
