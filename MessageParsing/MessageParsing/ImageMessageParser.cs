using InterfaceModel.Model;
using InterfaceModel.Repositories;
using M2MCommunication.Core;
using M2MCommunication.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace MessageParsing
{
    public class ImageMessageParser : MessageParser
    {
        protected internal IImageTemplateRepository ImageTemplateRepository { get; }
        public override IEnumerable<PrintableProperty> PrintableProperties => ImageTemplates.SelectMany(template => template.PrintableProperties);
        public ICollection<ImageTemplate> ImageTemplates { get; } = new List<ImageTemplate>();

        public ImageMessageParser(ConfigurationService configuration, SubscriptionFactoryService subscriptionFactory, IImageTemplateRepository imageTemplateRepository)
            : base(configuration, subscriptionFactory)
        {
            ImageTemplateRepository = imageTemplateRepository;
        }

        public override void Initialise(Func<Task> handler)
        {
            ImageTemplates.Clear();

            foreach (dynamic repository in Subscribe(handler).GroupBy(sub => sub.UaTypeMetadata.RepositoryGroupName, 
                (key, group) => new { RepositoryGroupName = key, Subscriptions = group }))
            {
                ImageTemplates.Add(
                    ImageTemplateRepository
                        .GetImageTemplateByName(repository.RepositoryGroupName)
                        .Initialise(repository.Subscriptions));
            }
        }
    }
}
