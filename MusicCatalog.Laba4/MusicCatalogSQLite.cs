using Microsoft.EntityFrameworkCore;
using MusicCatalog.Data;
namespace MusicCatalog.Laba4;
/// <summary>
/// Класс музыкального каталога, котрый хранится в бд sqlite. База данных каталога создается на основе файла- шаблона
/// базы данных. Файл шаблона sqlite_template.db должен находиться в текущей папке приложения.
/// Пользовыатель указывает путь к файлу музыкального каталога, если такой файл не существует,
/// приложение копирует файл шаблона бд в файл с указанным именем. 
/// 
/// </summary>
public class MusicCatalogSQLite:IMusicCatalog
{
    /// <summary>
    /// Имя файла-шаблона базы данных
    /// </summary>
    private const string TEMPLATE_DB_FILE = "sqlite_template.db";

    /// <summary>
    /// Имя файла бд
    /// </summary>
    private string fileName = string.Empty;
    /// <summary>
    /// Конструктор для создания каталога
    /// </summary>
    /// <param name="path">Имя файла каталога</param>
    public MusicCatalogSQLite(string path)
    {
        if (!File.Exists(path))
        {
            File.Copy(
                Path.Combine(Environment.CurrentDirectory??"", TEMPLATE_DB_FILE),
                path, false);
        }
        fileName = path;
        
    }

    #region IMusicCatalog interface implementation
    /// <summary>
    /// Метод доавляет композицию к перечню
    /// </summary>
    /// <param name="composition">Композиция, которую следует добавить</param>
    public async Task AddComposition(Composition composition)
    {
        using(MusicCatalogContext context = new MusicCatalogContext(fileName))
        {
            await context.Compositions.AddAsync(
               new Data.Composition
               {
                   Author = composition.Author,
                   SongName = composition.SongName,
               }
               );
            await context.SaveChangesAsync();
        }
    }
    /// <summary>
    /// Метод возвращает enumerator для перебора всех композици каталога.
    /// Композиции отсортированы сначала по автору, потом по названию
    /// </summary>
    /// <returns></returns>
    public async Task<IEnumerable<Composition>> EnumerateAllCompositions()
    {
        using (MusicCatalogContext context = new MusicCatalogContext(fileName))
        {
            return await context.Compositions
                .OrderBy(c => c.Author)
                .ThenBy(c => c.SongName)
                .Select(c => new Composition
                {
                    Author = c.Author ?? "",
                    SongName = c.SongName ?? "",
                }).ToListAsync();
        }
    }
    /// <summary>
    /// Метод возвращает enumerator для перебора композиций, удовлетворяющих
    /// критерию поиска
    /// </summary>
    /// <param name="query">Критерий поиска композиций</param>
    /// <returns>Enumerator для перебора</returns>
    public async Task<int> Remove(string query)
    {
        using (MusicCatalogContext context = new MusicCatalogContext(fileName))
        {
            var listToRemove =await  context.Compositions
                .Where(c => (c.Author!.Contains(query))
                || (c.SongName!.Contains(query))).ToListAsync();

            context.Compositions.RemoveRange(listToRemove);
            await context.SaveChangesAsync();
            return listToRemove.Count;
        }
    }
    /// <summary>
    /// Метод удаляет из каталога композиции, удовлетворяющие критерию поиска
    /// </summary>
    /// <param name="query">Критерий поиска</param>
    /// <returns>Количество удаленных композиций</returns>
    public async Task<IEnumerable<Composition>> Search(string query)
    {
        using (MusicCatalogContext context = new MusicCatalogContext(fileName))
        {
            return await context.Compositions
                .Where(c => (c.Author!.Contains(query))
                || (c.SongName!.Contains(query)))
                .OrderBy(c => c.Author)
                .ThenBy(c => c.SongName)
                .Select(c => new Composition
                {
                    Author = c.Author ?? "",
                    SongName = c.SongName ?? "",
                }).ToListAsync();
        }
    }
    #endregion
}
