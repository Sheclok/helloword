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

Workflow: `.github/workflows/deploy-azure-function.yml`

Workflow hỗ trợ 2 chế độ deploy:
1. **Khuyên dùng**: `AZURE_CREDENTIALS` (Azure Login / RBAC)
2. Fallback: `AZURE_FUNCTIONAPP_PUBLISH_PROFILE`

> Nếu cùng lúc có cả 2 secrets, workflow ưu tiên `AZURE_CREDENTIALS` để tránh lỗi Kudu 401.

Secrets cần có:
- `AZURE_FUNCTIONAPP_NAME`: tên Function App.
- `AZURE_CREDENTIALS` (khuyên dùng) **hoặc** `AZURE_FUNCTIONAPP_PUBLISH_PROFILE`.

## Cách lấy secrets

### 1) `AZURE_FUNCTIONAPP_NAME`

**Azure Portal**
1. Vào [portal.azure.com](https://portal.azure.com).
2. Mở resource Function App.
3. Copy `Function App name` ở trang Overview.

**Azure CLI**
```bash
az functionapp list --resource-group <resource-group> --query "[].name" -o tsv
```

### 2) `AZURE_CREDENTIALS` (khuyên dùng)

Tạo Service Principal chỉ scope vào đúng Function App:

```bash
az ad sp create-for-rbac \
  --name "github-actions-functionapp" \
  --role contributor \
  --scopes /subscriptions/<subscription-id>/resourceGroups/<resource-group>/providers/Microsoft.Web/sites/<function-app-name> \
  --sdk-auth
```

Copy toàn bộ JSON output vào GitHub secret `AZURE_CREDENTIALS`.

### 3) `AZURE_FUNCTIONAPP_PUBLISH_PROFILE` (fallback)

**Azure Portal**
1. Mở Function App.
2. Chọn **Get publish profile**.
3. Tải file `.PublishSettings`, mở file và copy toàn bộ XML.
4. Lưu XML vào secret `AZURE_FUNCTIONAPP_PUBLISH_PROFILE`.

**Azure CLI**
```bash
az functionapp deployment list-publishing-profiles \
  --name <function-app-name> \
  --resource-group <resource-group> \
  --xml
```

## Troubleshooting lỗi `Unauthorized (CODE: 401)`

Nếu workflow báo:

- `Failed to fetch Kudu App Settings. Unauthorized (CODE: 401)`

Thường do publish profile:
- đã cũ/không còn hiệu lực,
- copy XML không đầy đủ,
- hoặc SCM publishing/basic auth bị tắt.

Cách xử lý:
1. Chuyển sang `AZURE_CREDENTIALS` (khuyên dùng).
2. Nếu vẫn dùng publish profile, tải lại profile mới và cập nhật secret.
3. Kiểm tra SCM publishing/basic auth trong cấu hình Function App.

## Lưu ý bảo mật

- Không commit publish profile hoặc credentials vào source code.
- Nếu nghi ngờ lộ thông tin, rotate credentials ngay trong Azure Portal.
