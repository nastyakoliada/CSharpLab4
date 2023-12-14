using MusicCatalog.Laba4;

namespace MusicCatalog.Test.XUnit;

/// <summary>
/// Фейковый музыкальный каталог. Используется для проверки работы коммандера, который
/// взаимедойствует с пользователм
/// </summary>
internal class MockMusicCatalog : IMusicCatalog
{
    /// <summary>
    /// Список композиций для проверки прохождения команды add
    /// </summary>
    public List<Composition> Compositions { get; set; } = new List<Composition>();
    /// <summary>
    /// Полученный запрос на поиск  - для проверки команды search
    /// </summary>
    public string SearhQuery { get; set; } = null!;
    /// <summary>
    /// Полученный запрос на удаление композиции - для проверки прохождения команды Remove
    /// </summary>
    public string RemoveQuery { get; set; } = null!;
    /// <summary>
    /// Счетчик запросов полного списка - для проверки прохожденния команды list
    /// </summary>
    public int ListCallsNumber { get; set; }
    
    /// <summary>
    /// Метод интерфейса <see cref="IMusicCatalog"/>
    /// </summary>
    /// <param name="composition"></param>
    public  Task AddComposition(Composition composition)
    {
        Compositions.Add(composition);
        return Task.CompletedTask;
    }
    /// <summary>
    /// Метод интерфейса <see cref="IMusicCatalog"/>
    /// </summary>

    public Task<IEnumerable<Composition>> EnumerateAllCompositions()
    {
        ListCallsNumber++;
        return Task.FromResult<IEnumerable<Composition>>(Compositions);
    }
    /// <summary>
    /// Метод интерфейса <see cref="IMusicCatalog"/>
    /// </summary>

    public Task<int> Remove(string query)
    {
        RemoveQuery = query;
        return Task.FromResult<int>(0);
    }
    /// <summary>
    /// Метод интерфейса <see cref="IMusicCatalog"/>
    /// </summary>

    public Task<IEnumerable<Composition>> Search(string query)
    {
        SearhQuery = query;
        return Task.FromResult<IEnumerable<Composition>>(Compositions);
    }
}
