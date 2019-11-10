using System;
using System.Collections.Generic;

namespace InterfaceModel.Model
{
    public class ImageTemplate
    {
        public Guid MessageTypeGuid { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public string RelativePath { get; set; }
        public ICollection<IPropertyTemplate> PropertyTemplates { get; } = new List<IPropertyTemplate>();

        public ImageTemplate() { }
        public ImageTemplate(Guid messageTypeGuid, string relativePath, int width, int height)
        {
            MessageTypeGuid = messageTypeGuid;
            Width = width;
            Height = height;
            RelativePath = relativePath;
        }
    }
}
