using InterfaceModel.Model;
using System;
using System.Threading.Tasks;

namespace InterfaceModel.Repositories
{
    public interface IImageTemplateRepository
    {
        Task<ImageTemplate> GetImageTemplateByNameAsync(string name);
        Task<ImageTemplate> GetImageTemplateByIdAsync(Guid id);
    }
}
