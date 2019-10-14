using M2MCommunication;
using MessageParsing.Model;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageParsing
{
    public class ImageMessageParser : MessageParser
    {
        private readonly IStringLocalizer<ImageMessageParser> _localizer;

        public ImageTemplate ImageTemplate { get; private set; }

        public ImageMessageParser(IStringLocalizer<ImageMessageParser> localizer)
        {
            _localizer = localizer;
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
                "drawable value",
                new PropertyTemplate("drawable", new Point(0, 0), Color.BlueViolet, Color.White))
            );
            Properties.Add(new PrintableProperty(
                "printable value",
                new PropertyTemplate("printable", null, Color.Black, null))
            );
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
