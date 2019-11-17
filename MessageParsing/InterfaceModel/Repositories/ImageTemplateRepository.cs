using InterfaceModel.Model;
using System;
using System.Drawing;

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
            ImageTemplate template = new ImageTemplate(Guid.NewGuid(), @"Template.jpg", 1300, 480);
            template.PropertyTemplates.Add(new PropertyTemplate("BoolToggle", new Point(700, 200), Color.Black, Color.Transparent));
            return template;
        }
    }
}
