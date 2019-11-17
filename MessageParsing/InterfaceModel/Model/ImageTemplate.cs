using M2MCommunication.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace InterfaceModel.Model
{
    public class ImageTemplate
    {
        public Guid MessageTypeGuid { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public string RelativePath { get; set; }
        public ICollection<IPropertyTemplate> PropertyTemplates { get; } = new List<IPropertyTemplate>();
        public ICollection<IProperty> Properties { get; } = new List<IProperty>();
        public IEnumerable<DrawableProperty> DrawableProperties => Properties?.OfType<DrawableProperty>() ?? Enumerable.Empty<DrawableProperty>();
        public IEnumerable<PrintableProperty> PrintableProperties => Properties?.OfType<PrintableProperty>() ?? Enumerable.Empty<PrintableProperty>();

        public ImageTemplate() { }
        public ImageTemplate(Guid messageTypeGuid, string relativePath, int width, int height)
        {
            MessageTypeGuid = messageTypeGuid;
            Width = width;
            Height = height;
            RelativePath = relativePath;
        }

        public ImageTemplate Initialise(IEnumerable<ISubscription> subscriptions)
        {
            foreach (ISubscription subscription in subscriptions)
            {
                if (PropertyTemplates
                        .Where(template => template.Name.Equals(subscription.UaTypeMetadata.TypeName))
                        .FirstOrDefault() is PropertyTemplate propertyTemplate)
                {
                    Properties.Add(new DrawableProperty(subscription, propertyTemplate));
                }
                else
                {
                    Properties.Add(new PrintableProperty(
                        subscription, 
                        new PropertyTemplate(subscription.UaTypeMetadata.TypeName, null, Color.Black, null)
                    ));
                }
            }
            return this;
        }
    }
}
