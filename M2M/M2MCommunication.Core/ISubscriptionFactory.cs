﻿using System;
using System.Collections.Generic;
using System.Text;

namespace M2MCommunication.Core
{
    public interface ISubscriptionFactory
    {
        public void Initialise(IConsumerViewModel consumerViewModel);
    }
}
