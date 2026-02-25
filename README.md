# Azure Function HelloWorld (.NET 8)

Azure Function HTTP trigger đơn giản bằng **.NET 8 (isolated worker)**.

## Endpoint

- `GET /api/hello`
- `GET /api/hello?name=Codex`
- `POST /api/hello`

Response mẫu:

```json
{
  "message": "Hello, World!",
  "framework": ".NET 8 Azure Function"
}
```

## Chạy local

Yêu cầu:
- .NET SDK 8+
- Azure Functions Core Tools v4

Chạy:

```bash
func start
```

## Build nhanh

```bash
dotnet build
```
