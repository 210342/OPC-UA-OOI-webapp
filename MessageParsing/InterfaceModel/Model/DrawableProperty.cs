﻿using M2MCommunication.Core;

namespace InterfaceModel.Model
{
    public class DrawableProperty : BaseProperty
    {
        public DrawableProperty(ISubscription subscription, IPropertyTemplate template) : base(subscription, template) { }

        public PrintableProperty MapToPrintable()
        {
            return new PrintableProperty(Subscription, new PropertyTemplate(Template.Name, null, Template.FontColor, null));
        }
    }
}