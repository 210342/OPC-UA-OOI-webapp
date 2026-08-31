using ReactiveHMI.ReactiveInterface.TemplateRepositories.Configuration;
using ReactiveHMI.ReactiveInterface.TemplateRepositories.Model;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace ReactiveHMI.ReactiveInterface.TemplateRepositories.Repositories
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

        public ImageTemplate GetImageTemplateByAlias(string alias)
        {
            return Deserialise(alias);
        }

        public Task<ImageTemplate> GetImageTemplateByAliasAsync(string alias)
        {
            return DeserialiseAsync(alias);
        }

        private ImageTemplate Deserialise(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return new ImageTemplate();
            }
            string json = File.ReadAllText(
                Path.Combine(
                    _configuration.Directory,
                    path,
                    _configuration.PropertiesFileName
                )
            );
            return JsonSerializer.Deserialize<ImageTemplate>(json);
        }

        private async Task<ImageTemplate> DeserialiseAsync(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return new ImageTemplate();
            }
            string json = await File.ReadAllTextAsync(
                Path.Combine(
                    _configuration.Directory,
                    path,
                    _configuration.PropertiesFileName
                )
            );
            return JsonSerializer.Deserialize<ImageTemplate>(json);
        }
    }
}
