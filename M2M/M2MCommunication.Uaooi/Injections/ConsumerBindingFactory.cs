using M2MCommunication.Core;
using M2MCommunication.Core.Exceptions;
using M2MCommunication.Uaooi.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using UAOOI.Configuration.Networking.Serialization;
using UAOOI.Networking.SemanticData;
using UAOOI.Networking.SemanticData.DataRepository;

namespace M2MCommunication.Uaooi.Injections
{
    [Export(typeof(ISubscriptionFactory))]
    public class ConsumerBindingFactory : IBindingFactory, ISubscriptionFactory
    {
        public IDictionary<string, ISubscription> Subscriptions { get; } = new Dictionary<string, ISubscription>();

        /// <summary>
        /// Returns a subscriptions for the specified type by name
        /// </summary>
        /// <param name="subscriptionName">Name of the type to subscribe to</param>
        /// <param name="handler">Callback method of the subscription</param>
        /// <returns></returns>
        public ISubscription Subscribe(string subscriptionName, PropertyChangedEventHandler handler)
        {
            if (Subscriptions.TryGetValue(subscriptionName, out ISubscription subscription))
            {
                subscription.Enable(handler);
                return subscription;
            }
            else
            {
                throw new UnsupportedTypeException($"Type {subscriptionName} is not supported");
            }
        }

        public IConsumerBinding GetConsumerBinding(string repositoryGroup, string processValueName, UATypeInfo fieldTypeInfo)
        {
            if (fieldTypeInfo is null)
            {
                throw new ArgumentNullException(nameof(fieldTypeInfo));
            }
            return GetConsumerBinding(processValueName, fieldTypeInfo);
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

        private IConsumerBinding GetConsumerBinding(string typeName, UATypeInfo typeInfo)
        {
            if (typeInfo.ContainsMultidimensionalArray())
            {
                throw new ValueRankOutOfRangeException(nameof(typeInfo.ValueRank));
            }

            switch (typeInfo.BuiltInType)
            {
                case BuiltInType.Boolean:
                    return typeInfo.ContainsArray()
                        ? Bind<bool[]>(typeName, typeInfo)
                        : Bind<bool>(typeName, typeInfo);
                case BuiltInType.SByte:
                    return typeInfo.ContainsArray()
                        ? Bind<sbyte[]>(typeName, typeInfo)
                        : Bind<sbyte>(typeName, typeInfo);
                case BuiltInType.Byte:
                    return typeInfo.ContainsArray()
                        ? Bind<byte[]>(typeName, typeInfo)
                        : Bind<byte>(typeName, typeInfo);
                case BuiltInType.Int16:
                    return typeInfo.ContainsArray()
                        ? Bind<short[]>(typeName, typeInfo)
                        : Bind<short>(typeName, typeInfo);
                case BuiltInType.UInt16:
                    return typeInfo.ContainsArray()
                        ? Bind<ushort[]>(typeName, typeInfo)
                        : Bind<ushort>(typeName, typeInfo);
                case BuiltInType.Int32:
                    return typeInfo.ContainsArray()
                        ? Bind<int[]>(typeName, typeInfo)
                        : Bind<int>(typeName, typeInfo);
                case BuiltInType.UInt32:
                    return typeInfo.ContainsArray()
                        ? Bind<uint[]>(typeName, typeInfo)
                        : Bind<uint>(typeName, typeInfo);
                case BuiltInType.Int64:
                    return typeInfo.ContainsArray()
                        ? Bind<long[]>(typeName, typeInfo)
                        : Bind<long>(typeName, typeInfo);
                case BuiltInType.UInt64:
                    return typeInfo.ContainsArray()
                        ? Bind<ulong[]>(typeName, typeInfo)
                        : Bind<ulong>(typeName, typeInfo);
                case BuiltInType.Float:
                    return typeInfo.ContainsArray()
                        ? Bind<float[]>(typeName, typeInfo)
                        : Bind<float>(typeName, typeInfo);
                case BuiltInType.Double:
                    return typeInfo.ContainsArray()
                        ? Bind<double[]>(typeName, typeInfo)
                        : Bind<double>(typeName, typeInfo);
                case BuiltInType.String:
                    return typeInfo.ContainsArray()
                        ? Bind<string[]>(typeName, typeInfo)
                        : Bind<string>(typeName, typeInfo);
                case BuiltInType.DateTime:
                    return typeInfo.ContainsArray()
                        ? Bind<DateTime?[]>(typeName, typeInfo)
                        : Bind<DateTime?>(typeName, typeInfo);
                case BuiltInType.Guid:
                    return typeInfo.ContainsArray()
                        ? Bind<Guid?[]>(typeName, typeInfo)
                        : Bind<Guid?>(typeName, typeInfo);
                case BuiltInType.ByteString:
                    return typeInfo.ContainsArray()
                        ? Bind<byte[][]>(typeName, typeInfo)
                        : Bind<byte[]>(typeName, typeInfo);
                default:
                    throw new UnsupportedTypeException(typeInfo.BuiltInType.ToString());
            }
        }

        private IConsumerBinding Bind<type>(string typeName, UATypeInfo typeInfo)
        {
            ConsumerBindingMonitoredValue<type> binding = new ConsumerBindingMonitoredValue<type>(typeInfo);
            binding.PropertyChanged += (sender, args) =>
            {
                if (Subscriptions.TryGetValue(typeName, out ISubscription subscription))
                {
                    subscription.Value = sender;
                }
            };
            Subscriptions[typeName] = new Subscription(typeInfo, typeName, binding);
            return binding;
        }
    }
}
