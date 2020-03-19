using ReactiveHMI.TemplateRepositories.Model;
using System.Threading.Tasks;

namespace ReactiveHMI.TemplateRepositories.Repositories
{
    public interface IImageTemplateRepository
    {
        ImageTemplate GetImageTemplateByAlias(string alias);
        Task<ImageTemplate> GetImageTemplateByAliasAsync(string alias);
    }
}
