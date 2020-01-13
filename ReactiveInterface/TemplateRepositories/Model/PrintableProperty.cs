using M2MCommunication.Core.Interfaces;

namespace TemplateRepositories.Model
{
    public class PrintableProperty : BaseProperty
    {
        public PrintableProperty(ISubscription subscription, IPropertyTemplate template) : base(subscription, template) { }

        public DrawableProperty MapToDrawable(Point location)
        {
            return new DrawableProperty(Subscription, new PropertyTemplate(Template.Name, location, Template.HexColor));
        }
    }
}
