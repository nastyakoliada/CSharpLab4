using Microsoft.EntityFrameworkCore;
namespace MusicCatalog.Data;

public class MusicCatalogContext : DbContext
{
    /// <summary>
    /// Имя файла бд. 
    /// </summary>
    private readonly string fileName = "..\\mc.db";

    public DbSet<Composition> Compositions { get; set; }
    public MusicCatalogContext(string fileName)
    {
        this.fileName = fileName;

    }
    public MusicCatalogContext()
    {

    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if(!string.IsNullOrEmpty(fileName)) optionsBuilder.UseSqlite($"Filename={fileName}");
    }
}
