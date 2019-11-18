using InterfaceModel.Model;
using InterfaceModel.Repositories;
using System;
using System.Threading.Tasks;

namespace MessageParsingUnitTest.Mocks
{
    class TestImageTemplateRepository : IImageTemplateRepository
    {
        public Task<ImageTemplate> GetImageTemplateByIdAsync(Guid id)
        {
            ImageTemplate imageTemplate = new ImageTemplate(@"", 1920, 1080)
            {
                PropertyTemplates = new[] { new PropertyTemplate("Second type name", new Point(0, 0), "white") }
            };
            return Task.FromResult(imageTemplate);
        }

        public Task<ImageTemplate> GetImageTemplateByNameAsync(string name)
        {
            ImageTemplate imageTemplate = new ImageTemplate(@"", 1920, 1080)
            {
                PropertyTemplates = new[] { new PropertyTemplate("Second type name", new Point(0, 0), "white") }
            };
            return Task.FromResult(imageTemplate);
        }
    }
}
