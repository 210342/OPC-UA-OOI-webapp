using InterfaceModel.Model;
using InterfaceModel.Repositories;
using M2MCommunication.Core;
using M2MCommunication.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MessageParsing
{
    public class ImageMessageParser : MessageParser
    {
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

        protected internal override void OnSubscriptionReceived(ISubscription subscription)
        {
            lock (this)
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
            }
        }
    }
}
