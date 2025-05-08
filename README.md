# 📄 Sistema de Gerenciamento de Preços de Produtos

Este é um sistema simples desenvolvido em **C# (.NET)** com foco em **edição segura do preço de venda de produtos**, conectando-se a um banco de dados **Firebird (.FDB)**. Ideal para uso interno em comércios que desejam atualizar os preços de venda mantendo controle sobre custo e margem.

---

## 🎓 Tecnologias Utilizadas

* C# (.NET 6)
* Firebird 3.0
* Visual Studio Code (VS Code)
* DBeaver (para gerenciar o banco de dados)

---

## 🎯 Funcionalidades

* Conecta a banco Firebird (.FDB)
* Lista produtos da tabela `PRODUTO`
* Exibe: Código, Nome, Preço de Custo, Preço de Venda e Margem de Lucro
* Permite editar apenas o **preço de venda** via terminal
* Atualiza diretamente no banco com validação

---

## 📊 Cálculo da Margem de Lucro

```csharp
Margem (%) = ((PrecoVenda - PrecoCusto) / PrecoVenda) * 100
```

---

## 🔧 Como Executar

1. Instale o [.NET SDK](https://dotnet.microsoft.com/en-us/download) (versão 6.0 ou superior)
2. Instale o Firebird 3+ no seu sistema
3. Instale o [DBeaver](https://dbeaver.io/download/) para ver e editar o banco .FDB (opcional)
4. Clone o repositório:

```bash
git clone https://github.com/seu-usuario/GerenciadorDePrecos.git
cd GerenciadorDePrecos
```

5. Ajuste o caminho do banco de dados `.FDB` no arquivo `Program.cs`
6. Execute:

```bash
dotnet run
```

---

## 📅 Futuras melhorias (opcionais)

* Filtros de busca por nome
* Edição em lote
* Validação contra margem negativa
* Interface gráfica com Windows Forms
* Controle de usuários e logs de alteração

---

## 🚧 Avisos

* Este projeto é um MVP (Produto Mínimo Viável)
* Não é recomendável rodar em produção sem backup
* Sempre valide com o cliente antes de implantar

---

## 🎮 Desenvolvido por

Jean Felipe Moreira

---

## 💼 Licença

Este projeto é de uso interno e pode ser adaptado livremente conforme a necessidade da empresa/cliente.
