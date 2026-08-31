using ReactiveHMI.ReactiveInterface.TemplateRepositories.Model;
using ReactiveHMI.ReactiveInterface.TemplateRepositories.Repositories;
using System.Threading.Tasks;

namespace ReactiveHMI.ReactiveInterface.ReactiveInterfaceUnitTest.Mocks
{
    class TestImageTemplateRepository : IImageTemplateRepository
    {
        public ImageTemplate GetImageTemplateByAlias(string alias)
        {
            return new ImageTemplate(@"", 1920, 1080)
            {
                PropertyTemplates = [new PropertyTemplate("Second type name", new Point(0, 0), "white")]
            };
        }

        public Task<ImageTemplate> GetImageTemplateByAliasAsync(string name)
        {
            ImageTemplate imageTemplate = new(@"", 1920, 1080)
            {
                PropertyTemplates = [new PropertyTemplate("Second type name", new Point(0, 0), "white")]
            };
            return Task.FromResult(imageTemplate);
        }
    }
}
