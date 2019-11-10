using InterfaceModel.Model;
using InterfaceModel.Repositories;
using M2MCommunication.Core;
using M2MCommunication.Services;
using System;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace MessageParsing
{
    public class ImageMessageParser : MessageParser
    {
        protected internal IImageTemplateRepository ImageTemplateRepository { get; }
        public ImageTemplate ImageTemplate { get; private set; }

        public ImageMessageParser(ConfigurationService configuration, SubscriptionFactoryService subscriptionFactory, IImageTemplateRepository imageTemplateRepository)
            : base(configuration, subscriptionFactory)
        {
            ImageTemplateRepository = imageTemplateRepository;
        }

        public override void Initialise(Func<Task> handler)
        {
            ImageTemplate = ImageTemplateRepository.GetImageTemplateById(Guid.NewGuid());

            foreach (ISubscription subscription in Subscribe(handler))
            {
                if (ImageTemplate
                    .PropertyTemplates
                    .Where(p => p.Name.Equals(subscription.TypeName))
                    .FirstOrDefault() is IPropertyTemplate propertyTemplate)
                {
                    Properties.Add(new DrawableProperty(subscription, propertyTemplate));
                }
                else
                {
                    Properties.Add(new PrintableProperty(subscription, new PropertyTemplate(subscription.TypeName, null, Color.Black, null)));
                }
            }
        }
    }
}
