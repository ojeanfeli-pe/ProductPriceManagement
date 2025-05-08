// Importa as bibliotecas necessárias
using System;
using FirebirdSql.Data.FirebirdClient;

class Program
{
    static void Main()
    {
        // Define a string de conexão com o banco Firebird (.FDB)
        string connectionString = @"Database=C:\GerenciadorDePrecos\GERENCIADORDEPRECOS\BDPANDORA.FDB; DataSource=localhost; Port=3050; User=SYSDBA; Password=12345678; Charset=NONE;";

        // Cria uma conexão com o banco
        using (FbConnection connection = new FbConnection(connectionString))
        {
            try
            {
                // Abre a conexão
                connection.Open();
                Console.WriteLine("Conectado com sucesso ao banco de dados Firebird!");

                // Consulta SQL para buscar os dados da tabela PRODUTO
                string query = "SELECT CONTADOR, NOME, VALOR_CUSTO, PRECO_VENDA FROM PRODUTO";

                // Executa a consulta
                using (FbCommand command = new FbCommand(query, connection))
                using (FbDataReader reader = command.ExecuteReader())
                {
                    Console.WriteLine("\n--- Lista de Produtos ---");

                    // Percorre cada linha retornada pela consulta
                    while (reader.Read())
                    {
                        // Lê os dados do produto (tratando possíveis nulos)
                        string codigo = reader["CONTADOR"]?.ToString() ?? "";
                        string nome = reader["NOME"]?.ToString() ?? "";
                        decimal precoCusto = reader["VALOR_CUSTO"] != DBNull.Value ? Convert.ToDecimal(reader["VALOR_CUSTO"]) : 0;
                        decimal precoVenda = reader["PRECO_VENDA"] != DBNull.Value ? Convert.ToDecimal(reader["PRECO_VENDA"]) : 0;

                        // Calcula a margem de lucro
                        decimal margem = precoVenda > 0 ? ((precoVenda - precoCusto) / precoVenda) * 100 : 0;

                        // Exibe os dados no console
                        Console.WriteLine($"[{codigo}] {nome}");
                        Console.WriteLine($"  Custo: R$ {precoCusto:F2} | Venda: R$ {precoVenda:F2} | Margem: {margem:F2}%\n");
                    }
                }

                // Pergunta ao usuário qual produto deseja alterar
                Console.WriteLine("\nDigite o CONTADOR (código) do produto que deseja alterar:");
                string id = Console.ReadLine();

                // Pede o novo valor do preço de venda
                Console.WriteLine("Digite o novo preço de venda:");
                string novoPrecoStr = Console.ReadLine();

                // Tenta converter a entrada para decimal
                if (!decimal.TryParse(novoPrecoStr, out decimal novoPreco))
                {
                    Console.WriteLine("Valor inválido.");
                    return;
                }

                // Atualiza o preço de venda no banco para o produto selecionado
                string updateQuery = "UPDATE PRODUTO SET PRECO_VENDA = @preco WHERE CONTADOR = @id";
                using (FbCommand updateCommand = new FbCommand(updateQuery, connection))
                {
                    // Passa os parâmetros com segurança
                    updateCommand.Parameters.AddWithValue("@preco", novoPreco);
                    updateCommand.Parameters.AddWithValue("@id", id);

                    // Executa o UPDATE
                    int linhasAfetadas = updateCommand.ExecuteNonQuery();
                    if (linhasAfetadas > 0)
                    {
                        Console.WriteLine("Preço alterado com sucesso!");
                    }
                    else
                    {
                        Console.WriteLine("Produto não encontrado.");
                    }
                }

                // Fecha a conexão
                connection.Close();
            }
            catch (Exception ex)
            {
                // Mostra qualquer erro que acontecer
                Console.WriteLine("Erro: " + ex.Message);
            }
        }
    }
}
