using InterfaceModel.Model;
using System;

namespace InterfaceModel.Repositories
{
    /// <summary>
    /// Dummy implementation
    /// </summary>
    public class ImageTemplateRepository : IImageTemplateRepository
    {
        public ImageTemplate GetImageTemplateById(Guid id)
        {
            return new ImageTemplate(id, @"Template.jpg", 1300, 480);
        }

        public ImageTemplate GetImageTemplateByName(string name)
        {
            return new ImageTemplate(Guid.NewGuid(), @"Template.jpg", 1300, 480);
        }
    }
}
