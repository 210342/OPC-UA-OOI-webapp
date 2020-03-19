using ReactiveHMI.TemplateRepositories.Model;
using ReactiveHMI.TemplateRepositories.Repositories;
using ReactiveHMI.M2MCommunication.Core.Interfaces;
using ReactiveHMI.M2MCommunication.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReactiveHMI.ReferenceWebApplication.ReactiveInterface
{
    public class ImageMessageParser : MessageParser
    {
        private readonly object _imageTemplateDictionaryLock = new object();
        protected internal IImageTemplateRepository ImageTemplateRepository { get; }
        public override IEnumerable<PrintableProperty> PrintableProperties => ImageTemplates.Values.SelectMany(template => template.PrintableProperties);
        public IDictionary<string, ImageTemplate> ImageTemplates { get; } = new Dictionary<string, ImageTemplate>();

        public ImageMessageParser(MessageBusService messageBus, IImageTemplateRepository imageTemplateRepository)
            : base(messageBus)
        {
            ImageTemplateRepository = imageTemplateRepository;
        }

        public override void RefreshConfiguration()
        {
            ImageTemplates.Clear();
            base.RefreshConfiguration();
        }

        public override void AddSubscription(ISubscription subscription)
        {
            if (subscription is null)
            {
                throw new ArgumentNullException(nameof(subscription));
            }

            lock (_imageTemplateDictionaryLock)
            {
                if (ImageTemplates.TryGetValue(subscription.UaTypeMetadata.RepositoryGroupName, out ImageTemplate template))
                {
                    template.Subscribe(subscription);
                }
                else
                {
                    ImageTemplates.Add(
                        subscription.UaTypeMetadata.RepositoryGroupName,
                        ImageTemplateRepository.GetImageTemplateByAlias(subscription?.TypeAlias).Subscribe(subscription)
                    );
                }
                subscription.Enable(async (sender, args) => await InvokeSubscriptionUpdated().ConfigureAwait(false));
            }
        }
    }
}
