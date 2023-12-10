
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace MusicCatalog.Laba4;
/// <summary>
/// Класс музакального каталога, который взаимодействует с музыкальным каталогом через веб сервис. Конечная точка
/// указывается в контсрукторе. Путь к контроллеру указан в <see cref="API_PATH"/>
/// </summary>
public class MusicCatalogRestClient : IMusicCatalog,IDisposable
{
    /// <summary>
    /// Путь к контроллеру
    /// </summary>
    private const string API_PATH = "api/musiccatalog/";
    /// <summary>
    /// Клиент для взаимодействия с сервисом
    /// </summary>
    private readonly HttpClient httpClient;
    /// <summary>
    /// Конструктор каталога
    /// </summary>
    /// <param name="uri">Конечная точка сервиса</param>
    public MusicCatalogRestClient(string uri)
    {
        httpClient = new HttpClient
        {
            BaseAddress = new Uri(uri),            
        };
        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json", 1.0));
    }
    #region IMusicCatalog interface
    public void AddComposition(Composition composition)
    {
        using var responce = httpClient.PostAsJsonAsync<Data.Composition>(requestUri: API_PATH,
            value: new Data.Composition
            {
                Author=composition.Author,
                SongName=composition.SongName,
            });
        responce.Wait();
        using Task<Data.Composition?>? c = responce.Result.Content.ReadFromJsonAsync<Data.Composition>();
        c?.Wait();
    }
    
    public IEnumerable<Composition> EnumerateAllCompositions()
    {
        using HttpRequestMessage request = new(method: HttpMethod.Get, requestUri: API_PATH);
        
        using HttpResponseMessage responce = httpClient.Send(request);
        using var result =  responce.Content.ReadFromJsonAsync<IEnumerable<Data.Composition>>();
        result?.Wait();

        return  result?.Result is null ? Enumerable.Empty<Composition>() :
            result.Result.Select(c => new Composition
            {
                Author = c.Author ?? "",
                SongName = c.SongName ?? "",
            });
    }

    public int Remove(string query)
    {
        using HttpRequestMessage request = new(method: HttpMethod.Delete, requestUri: $"{API_PATH}{query}");
        using HttpResponseMessage responce = httpClient.Send(request);
        using var result = responce.Content.ReadFromJsonAsync<int>();
        result?.Wait();
        return result?.Result is null ? 0 : result.Result;
    }

    public IEnumerable<Composition> Search(string query)
    {
        using HttpRequestMessage request = new(method: HttpMethod.Get, requestUri: $"{API_PATH}?query={query}");

        using HttpResponseMessage responce = httpClient.Send(request);
        using var result = responce.Content.ReadFromJsonAsync<IEnumerable<Data.Composition>>();
        result?.Wait();

        return result?.Result is null ? Enumerable.Empty<Composition>() :
            result.Result.Select(c => new Composition
            {
                Author = c.Author ?? "",
                SongName = c.SongName ?? "",
            });
    }
    #endregion
    public void Dispose()
    {
        httpClient?.Dispose();
    }

}
