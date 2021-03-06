﻿using ReactiveHMI.TemplateRepositories.Model;
using ReactiveHMI.TemplateRepositories.Repositories;
using System.Threading.Tasks;

namespace ReactiveHMI.ReactiveInterfaceUnitTest.Mocks
{
    class TestImageTemplateRepository : IImageTemplateRepository
    {
        public ImageTemplate GetImageTemplateByAlias(string alias)
        {
            return new ImageTemplate(@"", 1920, 1080)
            {
                PropertyTemplates = new[] { new PropertyTemplate("Second type name", new Point(0, 0), "white") }
            };
        }

        public Task<ImageTemplate> GetImageTemplateByAliasAsync(string name)
        {
            ImageTemplate imageTemplate = new ImageTemplate(@"", 1920, 1080)
            {
                PropertyTemplates = new[] { new PropertyTemplate("Second type name", new Point(0, 0), "white") }
            };
            return Task.FromResult(imageTemplate);
        }
    }
}
