namespace MusicCatalog.Laba4;
/// <summary>
/// Класс содержит перечень музыкальных композиций и предоставляет методы по работе с ним.
/// Поддерживается сериализация с исполльзование <see cref="ISerializer{T}"/>
/// </summary>
public class MusicCatalog : IMusicCatalog
{
    
    /// <summary>
    /// Сериализатор для использования
    /// </summary>
    private readonly ISerializer<IEnumerable<Composition>> serializer = null!;
    /// <summary>
    /// Конструктор с указанием сериализатора
    /// </summary>
    /// <param name="serializer">Сериализатор для использования в дальнейшем</param>
    public MusicCatalog(ISerializer<IEnumerable<Composition>> serializer)
    {       
        this.serializer = serializer;
        using var res = serializer.Deserialize();
        res.Wait();
        Compositions = res.Result?.ToList() ?? new List<Composition>();
    }


    /// <summary>
    /// Перечень композиций
    /// </summary>
    private List<Composition> Compositions { get; set; } = null!;

    #region IMusicCatalog interface implementation
    /// <summary>
    /// Метод доавляет композицию к перечню
    /// </summary>
    /// <param name="composition">Композиция, которую следует добавить</param>
    public async Task AddComposition(Composition composition)
    {
        Compositions.Add(composition);
        await Serialize();
    }
    /// <summary>
    /// Метод возвращает enumerator для перебора всех композици каталога.
    /// Композиции отсортированы сначала по автору, потом по названию
    /// </summary>
    /// <returns></returns>
    public async Task<IEnumerable<Composition>> EnumerateAllCompositions()
    {
        return await Task.Run(() =>
        {
            return Compositions.OrderBy(c => c.Author).ThenBy(c => c.SongName);
        });
    }

    /// <summary>
    /// Метод возвращает enumerator для перебора композиций, удовлетворяющих
    /// критерию поиска
    /// </summary>
    /// <param name="query">Критерий поиска композиций</param>
    /// <returns>Enumerator для перебора</returns>
    public async Task<IEnumerable<Composition>> Search(string query)
    {
        return await Task.Run(() =>
        {
           return Compositions
            .Where(c => c.Author.Contains(query, StringComparison.OrdinalIgnoreCase)
            || c.SongName.Contains(query, StringComparison.OrdinalIgnoreCase))
            .OrderBy(c => c.Author)
            .ThenBy(c => c.SongName);
        });
    }
    
    /// <summary>
    /// Метод удаляет из каталога композиции, удовлетворяющие критерию поиска
    /// </summary>
    /// <param name="query">Критерий поиска</param>
    /// <returns>Количество удаленных композиций</returns>
    public async Task<int> Remove(string query)
    {
        var removeList = (await Search(query)).ToList();
        
        foreach(var item in removeList)
        {
            Compositions.Remove(item);
        }
        await Serialize();

        return removeList.Count;
       
    }
    #endregion

    /// <summary>
    /// Сериализация каталога.
    /// </summary>
    private async Task Serialize()
    {
        await serializer?.Serialize(Compositions)!;
    }
    
}
