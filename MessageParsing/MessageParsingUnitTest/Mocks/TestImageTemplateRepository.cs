using InterfaceModel.Model;
using InterfaceModel.Repositories;
using System;
using System.Drawing;

namespace MessageParsingUnitTest.Mocks
{
    class TestImageTemplateRepository : IImageTemplateRepository
    {
        public ImageTemplate GetImageTemplateById(Guid id)
        {
            ImageTemplate imageTemplate = new ImageTemplate(Guid.NewGuid(), @"", 1920, 1080);
            imageTemplate.PropertyTemplates.Add(new PropertyTemplate("Second type name", new Point(0, 0), Color.White, Color.Black));
            return imageTemplate;
        }

        public ImageTemplate GetImageTemplateByName(string name)
        {
            throw new NotImplementedException();
        }
    }
}
