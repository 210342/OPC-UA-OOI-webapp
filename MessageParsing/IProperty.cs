using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace MessageParsing
{
    public interface IProperty
    {
        string Name { get; }
        object Value { get; }
        decimal? XPosition { get; }
        decimal? YPosition { get; }
        Color? BackgroundColor { get; }
        Color? FontColor { get; }
    }
}
