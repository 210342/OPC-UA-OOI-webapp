using MessageParsing.Model;
using MessageParsing.UaooiExtensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using System.Text;
using UAOOI.Configuration.Networking.Serialization;
using UAOOI.Networking.SemanticData;
using UAOOI.Networking.SemanticData.DataRepository;

namespace MessageParsing
{
    public class ConsumerBindingFactory : IBindingFactory
    {
        public IDictionary<string, IProperty> BoundProperties { get; } = new Dictionary<string, IProperty>();

        public IConsumerBinding GetConsumerBinding(string repositoryGroup, string processValueName, UATypeInfo fieldTypeInfo)
        {
            if (fieldTypeInfo is null)
            {
                throw new ArgumentNullException(nameof(fieldTypeInfo));
            }
            return GetConsumerBinding(fieldTypeInfo);
        }

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
            MethodInfo handler = this.GetType().GetMethod("BoundPropertyChanged", BindingFlags.Instance | BindingFlags.NonPublic);
            consumerBindingType
                .GetEvent("PropertyChanged")
                .AddEventHandler(binding, Delegate.CreateDelegate(typeof(PropertyChangedEventHandler), this, handler));
            return binding;
        }

        private void BoundPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (BoundProperties.TryGetValue(args.PropertyName, out IProperty boundProperty))
            {
                boundProperty.Value = sender;
            }
        }
    }
}
