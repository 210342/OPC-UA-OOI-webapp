using M2MCommunication.Core;
using M2MCommunication.UaooiExtensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Globalization;
using System.Reflection;
using UAOOI.Configuration.Networking.Serialization;
using UAOOI.Networking.SemanticData;
using UAOOI.Networking.SemanticData.DataRepository;

namespace M2MCommunication.UaaoiInjections
{
    [Export(typeof(ISubscriptionFactory))]
    public class ConsumerBindingFactory : IBindingFactory, ISubscriptionFactory
    {
        public IDictionary<string, ISubscription> Subscriptions { get; } = new Dictionary<string, ISubscription>();

        public ISubscription GetSubscription()
        {
            throw new NotImplementedException();
        }

        public IConsumerBinding GetConsumerBinding(string repositoryGroup, string processValueName, UATypeInfo fieldTypeInfo)
        {
            if (fieldTypeInfo is null)
            {
                throw new ArgumentNullException(nameof(fieldTypeInfo));
            }
            return GetConsumerBinding(fieldTypeInfo);
        }

        /// <summary>
        /// Unsupported for consumers
        /// </summary>
        /// <param name="repositoryGroup"></param>
        /// <param name="processValueName"></param>
        /// <param name="fieldTypeInfo"></param>
        /// <returns></returns>
        public IProducerBinding GetProducerBinding(string repositoryGroup, string processValueName, UATypeInfo fieldTypeInfo)
        {
            throw new NotSupportedException();
        }

        private IConsumerBinding GetConsumerBinding(UATypeInfo typeInfo)
        {
            if (typeInfo.ContainsArray())
            {
                throw new ValueRankOutOfRangeException(typeInfo.ValueRank.ToString(CultureInfo.InvariantCulture));
            }
            Type consumerBindingType = typeof(ConsumerBindingMonitoredValue<>).MakeGenericType(typeInfo.GetUAType());
            IConsumerBinding binding = Activator.CreateInstance(consumerBindingType, new object[] { typeInfo }) as IConsumerBinding;
            MethodInfo handler = GetType().GetMethod("BoundPropertyChanged", BindingFlags.Instance | BindingFlags.NonPublic);
            consumerBindingType
                .GetEvent("PropertyChanged")
                .AddEventHandler(binding, Delegate.CreateDelegate(typeof(PropertyChangedEventHandler), this, handler));
            return binding;
        }

        private void BoundPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (Subscriptions.TryGetValue(args.PropertyName, out ISubscription subscription))
            {
                subscription.Value = sender;
            }
        }
    }
}
