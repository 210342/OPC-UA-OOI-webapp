using M2MCommunication.Core.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace TemplateRepositories.Model
{
    public class ImageTemplate
    {
        #region Serializable properties

        public int Width { get; set; }
        public int Height { get; set; }
        public string RelativePath { get; set; }
        public IEnumerable<PropertyTemplate> PropertyTemplates { get; set; } = Enumerable.Empty<PropertyTemplate>();

        #endregion

        #region Logic properties

        public ICollection<IProperty> Properties { get; } = new List<IProperty>();
        public IEnumerable<DrawableProperty> DrawableProperties => Properties?.OfType<DrawableProperty>()
            ?? Enumerable.Empty<DrawableProperty>();
        public IEnumerable<PrintableProperty> PrintableProperties => Properties?.OfType<PrintableProperty>()
            ?? Enumerable.Empty<PrintableProperty>();

        #endregion

        public ImageTemplate() { }
        public ImageTemplate(string relativePath, int width, int height)
        {
            Width = width;
            Height = height;
            RelativePath = relativePath;
        }

        public ImageTemplate Subscribe(ISubscription subscription)
        {
            IProperty existingProperty = Properties.FirstOrDefault(p => p.Subscription.UaTypeMetadata.Equals(subscription.UaTypeMetadata));
            if (existingProperty != null)
            {
                existingProperty.Subscription = subscription;
                return this;
            }
            else if (PropertyTemplates
                        .Where(template => template.Name.Equals(subscription.UaTypeMetadata.TypeName))
                        .FirstOrDefault() is PropertyTemplate propertyTemplate)
            {
                Properties.Add(new DrawableProperty(subscription, propertyTemplate));
            }
            else
            {
                Properties.Add(new PrintableProperty(
                    subscription,
                    new PropertyTemplate(subscription.UaTypeMetadata.TypeName, null, "#ffffff")
                ));
            }
            return this;
        }
    }
}
