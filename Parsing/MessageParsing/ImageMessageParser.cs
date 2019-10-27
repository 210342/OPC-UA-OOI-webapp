using M2MCommunication;
using MessageParsing.Model;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UAOOI.Configuration.Networking.Serialization;
using UAOOI.Networking.SemanticData;

namespace MessageParsing
{
    public class ImageMessageParser : MessageParser
    {
        private readonly IStringLocalizer<ImageMessageParser> _localizer;
        private readonly IBindingFactory _bindingFactory;

        public ImageTemplate ImageTemplate { get; private set; }

        public ImageMessageParser(IStringLocalizer<ImageMessageParser> localizer, IBindingFactory bindingFactory)
        {
            _localizer = localizer;
            _bindingFactory = bindingFactory;
        }

        public override void Parse(IMessage message)
        {
            if (message is null)
            {
                throw new ArgumentNullException(nameof(message));
            }
            // get image template from db, something like
            // ImageTemplate = DbContext.ImageTemplates.Where(it => message.TypeGuid.Equals(it.MessageTypeGuid));
            // deserialise content or something
            // get properties from the message
            Properties.Clear();
            ImageTemplate = new ImageTemplate(message.TypeGuid, "Template.jpg", 1300, 480);

            Properties.Add(new DrawableProperty(
                message.Id.ToString(),
                new PropertyTemplate("drawable", new Point(0, 0), Color.BlueViolet, Color.Transparent))
            );
            Properties.Add(new PrintableProperty(
                message.TypeGuid.ToString(),
                new PropertyTemplate("printable", null, Color.Black, null))
            );
            foreach (IProperty property in Properties)
            {
                (_bindingFactory as ConsumerBindingFactory)?.BoundProperties.TryAdd(property.Template.Name, property);
                _bindingFactory.GetConsumerBinding(string.Empty, property.Template.Name, new UATypeInfo(BuiltInType.String));
            }
        }

        public override async Task ParseAsync(IMessage message)
        {
            await Task.Run(() =>
            {
                Parse(message);
            })
            .ConfigureAwait(true);
        }
    }
}
