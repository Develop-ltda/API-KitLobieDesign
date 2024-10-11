# API KIT LOBIE DESIGN 🚀
O objetivo principal da API é facilitar tanto a apresentação de diferentes kits para clientes no front-end quanto o gerenciamento fácil de categorias e itens por meio de um painel de administrativo para a arquiteta e pessoas envolvidas na adm do projeto.

## Objetivos
<ul>
  <li><b>Apresentação do cliente:</b> fornecer uma interface dinâmica e fácil de usar para os clientes visualizarem os kits disponíveis, incluindo suas categorias e itens, com detalhes completos, como descrições, preços e imagens.</li>
  <li><b>Gerenciamento de administração:</b>permitir que arquiteta ou usuários administradores gerenciem kits, categorias e itens de forma eficaz. Isso inclui adicionar, atualizar e excluir categorias e itens para personalizar as ofertas de cada pacote de design.</li>
</ul>

## Entidades
<ul>
  <li><b>Kit:</b> a entidade principal que representa um pacote de design. Cada kit tem atributos como id, nome, descrição, preço, imageUrl e tipo. O tipo pode ser Standard, Premium ou Deluxe, diferenciando os níveis dos pacotes. Os kits também contêm uma lista de categorias.</li>
  <li><b>Categoria:</b> Cada kit contém várias categorias que agrupam itens semelhantes, como Móveis, Eletrônicos ou Decoração. Uma categoria tem atributos como id, nome, kitId (chave estrangeira que o vincula a um kit específico). As categorias também contêm uma lista de itens.</li>
  <li><b>Item:</b> As categorias contêm itens individuais como Sofá, Televisão ou Luminária. Um item é definido por atributos como id, nome, preço e categoryId (chave estrangeira que o vincula a uma categoria específica).</li>
</ul>

## Pontos de extremidades principais

`GET /api/kit`: Recupera todos os kits, incluindo suas categorias e itens, para exibição ao cliente.<br>

`GET /api/kit/{id}`: Recupera um kit específico por seu ID, juntamente com todas as categorias e itens relacionados. A resposta inclui os detalhes do kit e o custo agregado de todas as categorias e itens.<br>

`POST /api/kit`: Cria um novo kit. Útil para adicionar novos pacotes ao sistema.<br>

`PATCH /api/kit/{id}`: Atualiza um kit existente, permitindo alterações no nome, descrição, preço, tipo e imagem.<br>

`DELETE /api/kit/{id}`: Exclui um kit, incluindo todas as categorias e itens associados a ele.<br>

`POST /api/kit/{kitId}/category`: Cria uma nova categoria dentro de um kit existente. Este ponto de extremidade permite que arquitetos adicionem novas categorias como Móveis ou Eletrodomésticos a um kit específico.<br>

`GET /api/category/{id}`: Recupera uma categoria específica por seu ID, incluindo todos os itens dentro dela.<br>

`PATCH /api/category/{id}`: Atualiza os detalhes de uma categoria existente, incluindo seu nome ou detalhes relacionados ao preço.<br>

`DELETE /api/category/{categoryId}`: Exclui uma categoria específica e todos os itens dentro dela.<br>

`POST /api/category/{categoryId}/items`: Adicione um item a uma categoria. Isso é usado para adicionar itens como Sofá, Televisão, etc., a uma categoria específica.<br>

`PATCH /api/category/{categoryId}/items/{itemId}`: Atualize os detalhes de um item existente, como seu nome ou preço.<br>

`DELETE /api/category/{categoryId}/items/{itemId}`: Exclua um item de uma categoria.<br>


## Como a API funciona

<ul>
  <li><b>Funcionalidade do lado do cliente:</b> Os clientes podem visualizar os kits disponíveis por meio do front-end, que consome os endpoints GET para apresentar kits, categorias e itens. Cada kit exibe um preço calculado que reflete a soma de todas as categorias e itens associados.</li>
  <li><b>Funcionalidade do lado do administrador:</b> Usuários administradores ou arquitetos usam a API para adicionar, modificar ou remover kits, categorias e itens por meio de vários endpoints POST, PATCH e DELETE. Isso torna o gerenciamento de ofertas flexível, permitindo atualizações em tempo real dos pacotes.</li>  
</ul>

## Tecnologias Usadas
<ul>
  <li><b>ASP.NET Core:</b> para construir a API RESTful.</li>
  <li><b>Entity Framework Core:</b> para gerenciamento de banco de dados e relacionamentos entre kits, categorias e itens.</li>
  <li><b>Swagger:</b> para fornecer documentação de API e permitir testes fáceis de endpoints.</li>
</ul>

## Instruções de configuração para colaboradores 💻

`Clone o repositório`: clone o repositório do projeto do GitHub para sua máquina local usando:<br><br>
  `git clone https://github.com/Develop-ltda/API-KitLobieDesign`
 <br>
 

 Certifique-se de ter o .NET 6.0 SDK ou posterior instalado.

### 📌 Instale dependências:

Execute o seguinte comando no diretório raiz do projeto para restaurar dependências: <br> <br>
`dotnet restore`

### 📌 Configuração do banco de dados:

O projeto usa o Entity Framework Core para gerenciamento de banco de dados.<br><br>
Certifique-se de ter o MySQL instalado e em execução.<br><br>
Atualize a string de conexão em `Program.cs` com suas credenciais de banco de dados (nome de usuário e senha). O nome do banco de dados deve corresponder ao do projeto.<br>

### 📌  Execute o comando a seguir para aplicar as migrações e criar a estrutura do banco de dados:

`dotnet ef database update`

### 📌 Executando o aplicativo

Para executar o aplicativo localmente, use o comando: <br>

dotnet run<br>
A API estará acessível em `https://localhost:5101` por padrão.

### 📌 Testando a API:

A API usa o Swagger para testes e documentação. <br>
Quando o aplicativo estiver em execução, navegue até `https://localhost:5101/swagger` para visualizar e testar os endpoints disponíveis.<br>

## Resumo


Esta API fornece a funcionalidade principal necessária para exibir pacotes de design personalizáveis ​​no lado do cliente, ao mesmo tempo em que oferece suporte a arquiteta no gerenciamento e atualização de kits disponíveis, tudo com rastreamento preciso de preços e atualizações em tempo real.


![Mr-spockStar-trekGIF](https://github.com/user-attachments/assets/e50ce8ab-a7ef-41da-9177-d6e659400921)




