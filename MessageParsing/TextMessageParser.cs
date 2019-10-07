﻿using M2MCommunication;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Drawing;

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
            Properties.Add(new PrintableProperty("first property", "first value", Color.Black));
        }
    }
}
