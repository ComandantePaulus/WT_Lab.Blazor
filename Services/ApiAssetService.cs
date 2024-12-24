using WT_Lab.Domain;
using WT_Lab.Models;
using static System.Net.WebRequestMethods;

namespace WT_Lab.Blazor.Services
{
    public class ApiAssetService(HttpClient http):IAssetService<Asset>
    {
        List<Asset> _assets;
        int _currentPage = 1;
        int _totalPages = 1;
        public IEnumerable<Asset> Products => _assets;
        public int CurrentPage => _currentPage;
        public int TotalPages => _totalPages;

        public IEnumerable<Asset> Assets => _assets;

        public event Action ListChanged;
        public async Task GetAssets(int pageNo, int pageSize)
        {
            // Url сервиса API
            var uri = http.BaseAddress.AbsoluteUri;
            // данные для Query запроса
            var queryData = new Dictionary<string, string>
{
{ "pageNo", pageNo.ToString() },
{"pageSize", pageSize.ToString() }
};
            var query = QueryString.Create(queryData);
            // Отправить запрос http
            var result = await http.GetAsync(uri + query.Value);
            // В случае успешного ответа
            if (result.IsSuccessStatusCode)
            {
                // получить данные из ответа
                var responseData = await result.Content
                .ReadFromJsonAsync<ResponseData<AssetListModel<Asset>>>();
                // обновить параметры
                _currentPage = responseData.Data.CurrentPage;
                _totalPages = responseData.Data.TotalPages;
                _assets = responseData.Data.Items;
                ListChanged?.Invoke();
            }
            // В случае ошибки
            else
            {
                _assets = null;
                _currentPage = 1;
                _totalPages = 1;
            }
        }
    }
}
