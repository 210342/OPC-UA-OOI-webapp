using TemplateRepositories.Model;
using System.Threading.Tasks;

namespace TemplateRepositories.Repositories
{
    public interface IImageTemplateRepository
    {
        ImageTemplate GetImageTemplateByAlias(string alias);
        Task<ImageTemplate> GetImageTemplateByAliasAsync(string alias);
    }
}
