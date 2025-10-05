using System.IO;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Media;
using Android.Provider;
using MyApp.Services;

namespace MyApp.Android.Services;

public class AndroidFileAccessService : IFileAccessService
{
    private readonly Activity _activity;

    public AndroidFileAccessService(Activity activity)
    {
        _activity = activity;
    }

    public Task<string?> PickOpenFileAsync(string[] mimeTypes)
    {
        var intent = new Intent(Intent.ActionOpenDocument);
        intent.AddCategory(Intent.CategoryOpenable);
        intent.SetType("*");
        intent.PutExtra(Intent.ExtraMimeTypes, mimeTypes);

        // In a real application you would use ActivityResult APIs.
        return Task.FromResult<string?>(null);
    }

    public async Task SaveTextAsync(string filePath, string contents)
    {
        await File.WriteAllTextAsync(filePath, contents);
        MediaScannerConnection.ScanFile(_activity, new[] { filePath }, null, null);
    }
}
