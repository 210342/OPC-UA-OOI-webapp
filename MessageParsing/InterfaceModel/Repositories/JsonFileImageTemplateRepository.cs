using InterfaceModel.Configuration;
using InterfaceModel.Model;
using System;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace InterfaceModel.Repositories
{
    /// <summary>
    /// A repository that uses json serialisation to persist and retrieve templates
    /// </summary>
    public class JsonFileImageTemplateRepository : IImageTemplateRepository
    {
        private readonly RepositoryConfiguration _configuration;

        public JsonFileImageTemplateRepository(RepositoryConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Task<ImageTemplate> GetImageTemplateByIdAsync(Guid id)
        {
            return Deserialise(_configuration.Mappings
                            .FirstOrDefault(mapping => mapping.Id.Equals(id))
                            ?.Directory);
        }

        public Task<ImageTemplate> GetImageTemplateByNameAsync(string name)
        {
            return Deserialise(_configuration.Mappings
                            .FirstOrDefault(mapping => mapping.Name.Equals(name))
                            ?.Directory);
        }

        private async Task<ImageTemplate> Deserialise(string path)
        {
            if (!string.IsNullOrEmpty(path))
            {
                string json = await File.ReadAllTextAsync(
                    Path.Combine(
                        _configuration.Directory,
                        path,
                        _configuration.PropertiesFileName
                    )
                );
                return JsonSerializer.Deserialize<ImageTemplate>(json);
            }
            else
            {
                return new ImageTemplate();
            }
        }
    }
}
