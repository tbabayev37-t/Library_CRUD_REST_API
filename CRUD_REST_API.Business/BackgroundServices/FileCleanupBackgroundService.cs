using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRUD_REST_API.Business.BackgroundServices
{
    public class FileCleanupBackgroundService : BackgroundService
    {
        private ILogger<FileCleanupBackgroundService> _logger;
        private readonly IHostEnvironment _env;
        public FileCleanupBackgroundService(ILogger<FileCleanupBackgroundService> logger, IHostEnvironment env)
        {
            _logger = logger;
            _env = env;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Fayl temizleme Background Service ise dusdu.");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    _logger.LogInformation("Planlasdirilmis fayl temizlenmesi basladi: {time}", DateTimeOffset.Now);
                    CleanTempFiles();

                    _logger.LogInformation("Planlasdirilmis fayl temizlenmesi ugurla tamamlandi.");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Fayllar temizlenerken xeta bas verdi.");
                }
                await Task.Delay(TimeSpan.FromHours(24), stoppingToken);
            }

            _logger.LogInformation("Fayl temizleme Background Service dayandirildi.");
        }
        private void CleanTempFiles()
        {
            var tempFolderPath = Path.Combine(_env.ContentRootPath,"wwwroot","uploads","temp");
            if (Directory.Exists(tempFolderPath))
            {
                var files = Directory.GetFiles(tempFolderPath);
                foreach (var filePath in files) {
                    {
                        var fileInfo = new FileInfo(filePath);
                        if (fileInfo.CreationTime < DateTime.Now.AddMinutes(-1))
                        {
                            fileInfo.Delete();
                            _logger.LogInformation("Kohne muveqqeti fayl silindi: {fileName}", fileInfo.Name);
                        }
                    }
                }
            }
        }
    }
}
