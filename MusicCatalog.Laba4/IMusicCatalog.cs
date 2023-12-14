﻿
namespace MusicCatalog.Laba4;
/// <summary>
/// Интерфейс для работы с музыкальным каталогом
/// </summary>
public interface IMusicCatalog
{
    /// <summary>
    /// Метод добавляет композицию к перечню
    /// </summary>
    /// <param name="composition">Композиция, которую следует добавить</param>
    Task AddComposition(Composition composition);
    /// <summary>
    /// Метод возвращает enumerator для перебора всех композици каталога.
    /// Композиции отсортированы сначала по автору, потом по названию
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<Composition>> EnumerateAllCompositions();
    /// <summary>
    /// Метод возвращает enumerator для перебора композиций, удовлетворяющих
    /// критерию поиска
    /// </summary>
    /// <param name="query">Критерий поиска композиций</param>
    /// <returns>Enumerator для перебора</returns>
    Task<IEnumerable<Composition>> Search(string query);
    /// <summary>
    /// Метод удаляет из каталога композиции, удовлетворяющие критерию поиска
    /// </summary>
    /// <param name="query">Критерий поиска</param>
    /// <returns>Количество удаленных композиций</returns>
    Task<int> Remove(string query);
}
