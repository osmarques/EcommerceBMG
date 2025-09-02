# BMG.ECommerce

Back-end de um E-Commerce em .NET 8 com EF Core e SQLite.
Contém autenticação JWT, gestão de produtos, carrinho/pedidos e histórico de pedidos.

---

## Requisitos

* .NET 8 SDK
* SQLite (não precisa instalar, o arquivo será criado automaticamente)
* Navegador ou Postman para testar a API

---

## Executando a aplicação com `dotnet run`

1. Abra o terminal na raiz do projeto:

```bash
cd src/BMG.ECommerce.Api
```

2. Configure a connection string e chave JWT (opcional, valores default serão usados se não definir):

Linux/macOS:

```bash
export ConnectionStrings__Default="Data Source=./bmg-ecommerce.db"
export Jwt__Key="uma-chave-secreta-muito-grande-para-jwt"
```

Windows (PowerShell):

```powershell
$env:ConnectionStrings__Default="Data Source=./bmg-ecommerce.db"
$env:Jwt__Key="uma-chave-secreta-muito-grande-para-jwt"
```

3. Execute a aplicação:

```bash
dotnet run --configuration Release
```

4. Acesse a API via Swagger:

```
http://localhost:5089/swagger/index.html
```

---

## Endpoints principais

### Autenticação

* **POST** `/auth/login`
  Request:

  ```json
  {
    "email": "admin@shop.local",
    "password": "admin123"
  }
  ```

  Response:

  ```json
  {
    "token": "<JWT_TOKEN>"
  }
  ```

---

### Produtos

* **GET** `/products` — lista todos os produtos
* **GET** `/products/{id}` — obtém produto por id
* **POST** `/products` — cria produto (Admin)

  ```json
  {
    "name": "Notebook",
    "description": "Notebook gamer",
    "price": 4500.50,
    "stock": 10
  }
  ```
* **PUT** `/products/{id}` — atualiza produto (Admin)
* **DELETE** `/products/{id}` — remove produto (Admin)

---

### Carrinho

* **POST** `/cart/items` — adiciona item ao carrinho (Customer/Admin)
  Request:

  ```json
  {
    "productId": "ID_DO_PRODUTO",
    "quantity": 2
  }
  ```

  Response:

  ```json
  {
    "id": "ID_DO_PEDIDO",
    "total": 9001.0,
    "status": 0,
    "items": 2
  }
  ```

---

### Pedidos

* **POST** `/orders/checkout` — finaliza pedido (Customer/Admin)

  ```json
  {
    "paymentMethod": "Pix"
  }
  ```

  Response:

  ```json
  {
    "id": "ID_DO_PEDIDO",
    "status": "Confirmed",
    "total": 9001.0
  }
  ```

* **GET** `/orders/history` — histórico de pedidos do usuário

---

## Usuários de Seed (para teste)

| Email                                             | Senha       | Role     |
| ------------------------------------------------- | ----------- | -------- |
| [admin@shop.local](mailto:admin@shop.local)       | admin123    | Admin    |
| [customer@shop.local](mailto:customer@shop.local) | customer123 | Customer |

---

## Observações

* Banco SQLite será criado automaticamente em `src/BMG.ECommerce.Api/bmg-ecommerce.db`
* JWT é necessário para endpoints protegidos. Use `/auth/login` para gerar.
* Teste via Swagger ou Postman usando o token retornado no login.
* Todas as tabelas e dados iniciais são populados automaticamente com `SeedData`.
