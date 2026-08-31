using ReactiveHMI.ReactiveInterface.TemplateRepositories.Model;
using System.Threading.Tasks;

namespace ReactiveHMI.ReactiveInterface.TemplateRepositories.Repositories
{
    public interface IImageTemplateRepository
    {
        ImageTemplate GetImageTemplateByAlias(string alias);
        Task<ImageTemplate> GetImageTemplateByAliasAsync(string alias);
    }
}
