using InterfaceModel.Model;
using System.Threading.Tasks;

namespace InterfaceModel.Repositories
{
    public interface IImageTemplateRepository
    {
        ImageTemplate GetImageTemplateByAlias(string alias);
        Task<ImageTemplate> GetImageTemplateByAliasAsync(string alias);
    }
}
