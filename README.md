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

## Auto deploy với GitHub Actions

Workflow đã được thêm tại `.github/workflows/deploy-azure-function.yml`.

Để deploy tự động lên Azure Functions, thêm 2 GitHub Secrets trong repo:

- `AZURE_FUNCTIONAPP_NAME`: tên Azure Function App.
- `AZURE_FUNCTIONAPP_PUBLISH_PROFILE`: nội dung publish profile tải từ Azure Portal.

Sau đó push lên nhánh `main` hoặc chạy thủ công từ tab **Actions**.
