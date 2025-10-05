using System.Threading.Tasks;

namespace MyApp.Services;

public interface IFileAccessService
{
    Task<string?> PickOpenFileAsync(string[] mimeTypes);
    Task SaveTextAsync(string filePath, string contents);
}
