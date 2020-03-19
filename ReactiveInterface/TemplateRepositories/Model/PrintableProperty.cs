using ReactiveHMI.M2MCommunication.Core.Interfaces;

namespace ReactiveHMI.TemplateRepositories.Model
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
