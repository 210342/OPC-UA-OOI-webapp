using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace MessageParsing.Model
{
    public class ImageTemplate
    {
        public Guid MessageTypeGuid { get; }
        public int Width { get; }
        public int Height { get; }
        public Uri Uri { get; }
        public ICollection<IProperty> Properties { get; }
    }
}
