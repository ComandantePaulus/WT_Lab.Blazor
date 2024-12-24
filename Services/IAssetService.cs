namespace WT_Lab.Blazor.Services
{
    public interface IAssetService<T> where T : class
    {
        event Action ListChanged;
        // Список объектов
        IEnumerable<T> Assets { get; }
        // Номер текущей страницы
        int CurrentPage { get; }
        // Общее количество страниц
        int TotalPages { get; }
        // Получение списка объектов
        Task GetAssets(int pageNo = 1, int pageSize = 3);
    }
}
