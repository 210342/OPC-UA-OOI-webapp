using M2MCommunication;
using MessageParsing.Model;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;

namespace MessageParsing
{
    public class TextMessageParser : MessageParser
    {
        private readonly IStringLocalizer<TextMessageParser> _localizer;
        
        public TextMessageParser(IStringLocalizer<TextMessageParser> localizer)
        {
            _localizer = localizer;
        }

        public override void Parse(IMessage message)
        {
            if (message is null)
            {
                throw new ArgumentNullException(nameof(message));
            }
            Properties.Clear();
            Properties.Add(new PrintableProperty(
                "first value", 
                new PropertyTemplate("first", null, Color.Black, null))
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
