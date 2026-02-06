using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using Microsoft.Extensions.Logging;

namespace UsersApp.Services
{
    public class FileService
    {
        private readonly string _filePath;
        private readonly ILogger<FileService> _logger;

        public FileService(ILogger<FileService> logger)
        {
            _logger = logger;
            _filePath = Path.Combine(AppContext.BaseDirectory, "public", "data.json");

            var directory = Path.GetDirectoryName(_filePath);
            if (directory == null) return;

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
                _logger.LogInformation("Directory created at {Directory}", directory);
            }
            else
            {
                _logger.LogInformation("Directory already exists at {Directory}", directory);
            }

            if (!File.Exists(this._filePath))
            {
                using (File.Create(_filePath)) { }
                _logger.LogInformation("File created at {FilePath}", _filePath);
            }
            else
            {
                _logger.LogInformation("File already exists at {FilePath}", _filePath);
            }
        }

        public void SaveToFile<T>(T data)
        {
            if (!File.Exists(this._filePath)) return;

            string json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(this._filePath, json);
            _logger.LogInformation("Data saved to file {FilePath}", _filePath);
        }

        public T? LoadFromFile<T>()
        {
            if (!File.Exists(this._filePath)) return default;

            var json = File.ReadAllText(this._filePath);

            if (string.IsNullOrWhiteSpace(json))
            {
                _logger.LogWarning("File {FilePath} is empty, returning default", _filePath);
                return default;
            }

            _logger.LogInformation("Data loaded from file {FilePath}", _filePath);
            return JsonSerializer.Deserialize<T>(json);
        }
    }
}