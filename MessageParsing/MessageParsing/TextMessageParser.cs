using M2MCommunication;
using MessageParsing.Model;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using M2MCommunication.Core;

namespace MessageParsing
{
    public class TextMessageParser : MessageParser
    {
        private readonly IStringLocalizer<TextMessageParser> _localizer;
        
        public TextMessageParser(IStringLocalizer<TextMessageParser> localizer)
        {
            _localizer = localizer;
        }

        public override void Parse()
        {
            Properties.Clear();
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
