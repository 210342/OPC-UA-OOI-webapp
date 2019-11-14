using InterfaceModel.Model;
using System;

namespace InterfaceModel.Repositories
{
    public interface IImageTemplateRepository
    {
        ImageTemplate GetImageTemplateByName(string name);
        ImageTemplate GetImageTemplateById(Guid id);
    }
}
