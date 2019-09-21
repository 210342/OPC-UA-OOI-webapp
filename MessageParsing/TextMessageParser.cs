using M2M_Communication;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageParsing
{
    public class TextMessageParser : IMessageParser
    {
        private readonly IStringLocalizer<TextMessageParser> _localizer;

        public event ObjectUpdatedEventHandler ObjectUpdated;
        
        public TextMessageParser(IStringLocalizer<TextMessageParser> localizer)
        {
            _localizer = localizer;
        }

        public void Parse(IMessage message)
        {
            ObjectUpdated?.Invoke($"{_localizer["Sent"]}: {message.TimeSent.ToShortDateString()} {message.TimeSent.ToLongTimeString()}{Environment.NewLine}{message.Content}", message);
        }
    }
}
