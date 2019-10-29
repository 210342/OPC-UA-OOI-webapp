using M2MCommunication;
using MessageParsing.Model;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using M2MCommunication.Services;

namespace MessageParsing
{
    public class ImageMessageParser : MessageParser
    {
        private readonly IStringLocalizer<ImageMessageParser> _localizer;
        private readonly SubscriptionFactoryService _bindingFactory;

        public ImageTemplate ImageTemplate { get; private set; }

        public ImageMessageParser(IStringLocalizer<ImageMessageParser> localizer, SubscriptionFactoryService bindingFactory)
        {
            _localizer = localizer;
            _bindingFactory = bindingFactory;
        }

        public override void Parse()
        {
            // get image template from db, something like
            // ImageTemplate = DbContext.ImageTemplates.Where(it => message.TypeGuid.Equals(it.MessageTypeGuid));
            // deserialise content or something
            // get properties from the message
            Properties.Clear();
            ImageTemplate = new ImageTemplate(Guid.NewGuid(), "Template.jpg", 1300, 480);

            foreach (IProperty property in Properties)
            {
                _bindingFactory.SubscriptionFactory.GetSubscription();
            }
        }

        public override async Task ParseAsync()
        {
            await Task.Run(() =>
            {
                Parse();
            })
            .ConfigureAwait(true);
        }
    }
}
