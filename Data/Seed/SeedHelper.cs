using System;
namespace Lona.Data.Seed;

public static class SeedHelper
{
    public static List<TEntity> SeedData<TEntity>()
    {
        var typeName = typeof(TEntity).Name;
        string fileName = $"{typeName}.json";
        string currentDirectory = Directory.GetCurrentDirectory();
        string folder = $"Data{Path.DirectorySeparatorChar}Seed{Path.DirectorySeparatorChar}SeedData";
        string fullPath = Path.Combine(currentDirectory, folder, fileName);


        var result = new List<TEntity>();
        using (StreamReader reader = new(fullPath))
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            string json = reader.ReadToEnd();
            result = JsonSerializer.Deserialize<List<TEntity>>(json, options);
        }

        return result;
    }

}

