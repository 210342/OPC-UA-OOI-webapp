using M2MCommunication;
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

        public ImageMessageParser(IStringLocalizer<ImageMessageParser> localizer)
        {
            _localizer = localizer;
        }

        public override void Parse(IMessage message)
        {
            // deserialise content or something
            // get properties from the message
            if (message is null)
            {
                throw new ArgumentNullException(nameof(message));
            }
            Properties.Clear();
            Properties.Add(new DrawableProperty("First property", "Drawable property value", 30, 0, Color.White, Color.Black));
            Properties.Add(new PrintableProperty(nameof(message.TimeSent), message.TimeSent, Color.Black));
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
