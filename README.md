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

Workflow deploy đang nhận cấu hình trực tiếp từ 2 secrets sau:

- `AZURE_FUNCTIONAPP_NAME`
- `AZURE_FUNCTIONAPP_PUBLISH_PROFILE`

Để deploy tự động lên Azure Functions, thêm 2 GitHub Secrets trong repo:

- `AZURE_FUNCTIONAPP_NAME`: tên Azure Function App.
- `AZURE_FUNCTIONAPP_PUBLISH_PROFILE`: nội dung publish profile tải từ Azure Portal.

Sau đó push lên nhánh `main` hoặc chạy thủ công từ tab **Actions**.

### Cách lấy `AZURE_FUNCTIONAPP_NAME`

#### Cách 1: Azure Portal
1. Vào [portal.azure.com](https://portal.azure.com).
2. Mở resource **Function App** của bạn.
3. Copy giá trị **Function App name** ở trang Overview.
4. Vào GitHub repo → **Settings** → **Secrets and variables** → **Actions** → **New repository secret**.
5. Tạo secret:
   - Name: `AZURE_FUNCTIONAPP_NAME`
   - Value: `<Function App name>`

#### Cách 2: Azure CLI

```bash
az functionapp list --resource-group <resource-group> --query "[].name" -o tsv
```

Lấy đúng tên app rồi lưu vào secret `AZURE_FUNCTIONAPP_NAME` trên GitHub.

### Cách lấy `AZURE_FUNCTIONAPP_PUBLISH_PROFILE`

#### Cách 1: Azure Portal
1. Mở Function App trong Azure Portal.
2. Vào **Overview** (hoặc **Get publish profile**).
3. Chọn **Get publish profile** để tải file `.PublishSettings`.
4. Mở file đó, copy toàn bộ nội dung XML.
5. Tạo GitHub secret:
   - Name: `AZURE_FUNCTIONAPP_PUBLISH_PROFILE`
   - Value: toàn bộ XML từ file publish profile.

#### Cách 2: Azure CLI

```bash
az functionapp deployment list-publishing-profiles \
  --name <function-app-name> \
  --resource-group <resource-group> \
  --xml
```

Copy output XML và lưu vào secret `AZURE_FUNCTIONAPP_PUBLISH_PROFILE`.

### Lưu ý bảo mật

- Không commit publish profile vào source code.
- Nếu nghi ngờ lộ profile, vào Azure Portal và **reset publishing credentials**.
