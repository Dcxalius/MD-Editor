using System.IO;
using System.Threading.Tasks;
using MyApp.Services;
using Windows.Storage;
using Windows.Storage.Pickers;

namespace MyApp.Windows.Services;

public class WinUiFileAccessService : IFileAccessService
{
    public async Task<string?> PickOpenFileAsync(string[] mimeTypes)
    {
        var picker = CreateOpenPicker(mimeTypes);
        var file = await picker.PickSingleFileAsync();
        return file?.Path;
    }

    public async Task SaveTextAsync(string filePath, string contents)
    {
        if (File.Exists(filePath))
        {
            var existing = await StorageFile.GetFileFromPathAsync(filePath);
            await FileIO.WriteTextAsync(existing, contents);
            return;
        }

        var picker = CreateSavePicker(Path.GetExtension(filePath));
        var file = await picker.PickSaveFileAsync();
        if (file is null)
        {
            return;
        }

        await FileIO.WriteTextAsync(file, contents);
    }

    private static FileOpenPicker CreateOpenPicker(string[] mimeTypes)
    {
        var picker = new FileOpenPicker();
        picker.FileTypeFilter.Clear();
        if (mimeTypes.Length == 0)
        {
            picker.FileTypeFilter.Add("*");
        }
        else
        {
            foreach (var mime in mimeTypes)
            {
                picker.FileTypeFilter.Add(mime);
            }
        }

        InitializeWithWindow(picker);
        return picker;
    }

    private static FileSavePicker CreateSavePicker(string? extension)
    {
        var picker = new FileSavePicker();
        picker.FileTypeChoices.Add("Document", string.IsNullOrEmpty(extension) ? new[] { ".txt" } : new[] { extension });
        InitializeWithWindow(picker);
        return picker;
    }

    private static void InitializeWithWindow(object picker)
    {
        var handle = WindowHandleProvider.GetHandle();
        WinRT.Interop.InitializeWithWindow.Initialize(picker, handle);
    }
}
