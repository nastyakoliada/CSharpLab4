using MusicCatalog.Data;
using Microsoft.EntityFrameworkCore;
namespace MusicCatalog.WebService.Repositories;
/// <summary>
/// Реализация хранилища музыкального каталога
/// </summary>
public class MusicCatalogRepository : IMusicCatalogRepository
{
    /// <summary>
    /// Контекст
    /// </summary>
    private MusicCatalogContext db = null!;
    /// <summary>
    /// Конструктор, который получает контекст из хранилища сервисов
    /// </summary>
    /// <param name="injectedContext">Контекст к бд</param>
    public MusicCatalogRepository(MusicCatalogContext injectedContext)
    {
        db = injectedContext;
    }
    /// <summary>
    /// Добавить композицию в хранилище
    /// </summary>
    /// <param name="composition">Композиция</param>
    /// <returns>Возвращаемая композиция содержит ID</returns>
    public async Task<Composition?> AddCompositionAsync(Composition composition)
    {
        await db.Compositions.AddAsync(composition);
        return await db.SaveChangesAsync() == 1? composition : null;
        
    }
    /// <summary>
    /// Все композиции в хранилище
    /// </summary>
    /// <returns></returns>
    public async Task<IEnumerable<Composition>> EnumerateAllCompositionsAsync()
    {
        return await db.Compositions
            .OrderBy(c => c.Author)
            .ThenBy(c => c.SongName)
            .ToListAsync();
    }
    /// <summary>
    /// Удаляет копозиции, удовлетворяющие запросу, из хранилища
    /// </summary>
    /// <param name="query">Запрос, например, имя автора или название песни</param>
    /// <returns>Количество удаленных композиций</returns>
    public async Task<int> RemoveAsync(string query)
    {
        var listToRemove = await db.Compositions
                .Where(c => (c.Author!.Contains(query))
                || (c.SongName!.Contains(query))).ToListAsync();
       
        db.Compositions.RemoveRange(listToRemove);
        await db.SaveChangesAsync();
        return listToRemove.Count;
    }
    /// <summary>
    /// Все композиции в хранилище, удовлетворящие запросу
    /// </summary>
    /// <param name="query">Запрос, например, имя автора или название песни</param>
    /// <returns></returns>
    public async Task<IEnumerable<Composition>> SearchAsync(string query)
    {
        return await db.Compositions
                .Where(c => (c.Author!.Contains(query))
                    || (c.SongName!.Contains(query)))
                .OrderBy(c => c.Author)
                .ThenBy(c => c.SongName)
                .ToListAsync();
    }
    /// <summary>
    /// Возвращает композицию по ID
    /// </summary>
    /// <param name="id">Уникальный ключ к композиции</param>
    /// <returns>Композиция <see cref="Composition"/></returns>
    public async Task<Composition?> GetCompositionAsync(int? id)
    {
        if (!id.HasValue) return null;
        return await db.Compositions.FindAsync(id);
    }
}
