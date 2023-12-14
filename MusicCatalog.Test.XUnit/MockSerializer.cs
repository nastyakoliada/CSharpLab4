using MusicCatalog.Laba4;

namespace MusicCatalog.Test.XUnit;

/// <summary>
/// Фейковый сериализатор для проверки прохождения команд на сериализацию
/// </summary>
internal class MockSerializer : ISerializer<IEnumerable<Composition>>
{
    /// <summary>
    /// Начальный список композиций, как бы десериализированный
    /// </summary>
    public IEnumerable<Composition> Compositions { get; set; } = new List<Composition>()
    {
        new Composition
        {
            Author = "Sia",
            SongName = "Forever",
        },
        new Composition
        {
            Author = "Billie Eilish",
            SongName = "Lovely",
        }
    };
    /// <summary>
    /// Список композиций, который нужно сериализовать
    /// </summary>
    public IEnumerable<Composition> SerializedCompositions { get; set; } = null!;
    /// <summary>
    /// Метод интерфейса <see cref="ISerializer{T}"/>
    /// </summary>
    /// <returns>Возвращает предопреленный список композиций</returns>
    public Task<IEnumerable<Composition>> Deserialize()
    {
        return Task.FromResult<IEnumerable<Composition>>(Compositions);
    }
    /// <summary>
    /// Метод интерфейса <see cref="ISerializer{T}"/> сохранаяет полученный список композиций для сериализации
    /// </summary>
    public Task Serialize(IEnumerable<Composition> compositions)
    {
        SerializedCompositions = compositions;
        return Task.CompletedTask;
    }
}
