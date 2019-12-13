using M2MCommunication.Core.CommonTypes;
using M2MCommunication.Core.Exceptions;
using M2MCommunication.Core.Interfaces;
using M2MCommunication.Uaooi.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using UAOOI.Configuration.Networking.Serialization;
using UAOOI.Networking.SemanticData;
using UAOOI.Networking.SemanticData.DataRepository;

namespace M2MCommunication.Uaooi.Injections
{
    [Export(typeof(IBindingFactory))]
    public class ConsumerBindingFactory : IBindingFactory, ISubscriptionFactory
    {
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;
        private IConsumerViewModel _consumerViewModel;

        private readonly IDictionary<UaTypeMetadata, ISubscription> _subscriptions = new Dictionary<UaTypeMetadata, ISubscription>();

        [ImportingConstructor]
        public ConsumerBindingFactory(ILogger logger, IConfiguration configuration)
        {
            _configuration = configuration
                ?? throw new ComponentNotInitialisedException($"{nameof(configuration)} injected into {nameof(ConsumerBindingFactory)} is null");
            _logger = logger;
        }

        public void Initialise(IConsumerViewModel consumerViewModel)
        {
            _consumerViewModel = consumerViewModel
                ?? throw new ComponentNotInitialisedException($"{nameof(consumerViewModel)} injected into {nameof(ConsumerBindingFactory)} is null");
        }

        public IConsumerBinding GetConsumerBinding(string repositoryGroup, string processValueName, UATypeInfo fieldTypeInfo)
        {
            if (fieldTypeInfo is null)
            {
                throw new ArgumentNullException(nameof(fieldTypeInfo));
            }
            if (fieldTypeInfo.ContainsMultidimensionalArray())
            {
                throw new ValueRankOutOfRangeException(nameof(fieldTypeInfo.ValueRank));
            }
            UaTypeMetadata typeMetadata = new UaTypeMetadata(repositoryGroup, processValueName);
            switch (fieldTypeInfo.BuiltInType)
            {
                case BuiltInType.Boolean:
                    return fieldTypeInfo.ContainsArray()
                        ? Bind<bool[]>(typeMetadata, fieldTypeInfo)
                        : Bind<bool>(typeMetadata, fieldTypeInfo);
                case BuiltInType.SByte:
                    return fieldTypeInfo.ContainsArray()
                        ? Bind<sbyte[]>(typeMetadata, fieldTypeInfo)
                        : Bind<sbyte>(typeMetadata, fieldTypeInfo);
                case BuiltInType.Byte:
                    return fieldTypeInfo.ContainsArray()
                        ? Bind<byte[]>(typeMetadata, fieldTypeInfo)
                        : Bind<byte>(typeMetadata, fieldTypeInfo);
                case BuiltInType.Int16:
                    return fieldTypeInfo.ContainsArray()
                        ? Bind<short[]>(typeMetadata, fieldTypeInfo)
                        : Bind<short>(typeMetadata, fieldTypeInfo);
                case BuiltInType.UInt16:
                    return fieldTypeInfo.ContainsArray()
                        ? Bind<ushort[]>(typeMetadata, fieldTypeInfo)
                        : Bind<ushort>(typeMetadata, fieldTypeInfo);
                case BuiltInType.Int32:
                    return fieldTypeInfo.ContainsArray()
                        ? Bind<int[]>(typeMetadata, fieldTypeInfo)
                        : Bind<int>(typeMetadata, fieldTypeInfo);
                case BuiltInType.UInt32:
                    return fieldTypeInfo.ContainsArray()
                        ? Bind<uint[]>(typeMetadata, fieldTypeInfo)
                        : Bind<uint>(typeMetadata, fieldTypeInfo);
                case BuiltInType.Int64:
                    return fieldTypeInfo.ContainsArray()
                        ? Bind<long[]>(typeMetadata, fieldTypeInfo)
                        : Bind<long>(typeMetadata, fieldTypeInfo);
                case BuiltInType.UInt64:
                    return fieldTypeInfo.ContainsArray()
                        ? Bind<ulong[]>(typeMetadata, fieldTypeInfo)
                        : Bind<ulong>(typeMetadata, fieldTypeInfo);
                case BuiltInType.Float:
                    return fieldTypeInfo.ContainsArray()
                        ? Bind<float[]>(typeMetadata, fieldTypeInfo)
                        : Bind<float>(typeMetadata, fieldTypeInfo);
                case BuiltInType.Double:
                    return fieldTypeInfo.ContainsArray()
                        ? Bind<double[]>(typeMetadata, fieldTypeInfo)
                        : Bind<double>(typeMetadata, fieldTypeInfo);
                case BuiltInType.String:
                    return fieldTypeInfo.ContainsArray()
                        ? Bind<string[]>(typeMetadata, fieldTypeInfo)
                        : Bind<string>(typeMetadata, fieldTypeInfo);
                case BuiltInType.DateTime:
                    return fieldTypeInfo.ContainsArray()
                        ? Bind<DateTime?[]>(typeMetadata, fieldTypeInfo)
                        : Bind<DateTime?>(typeMetadata, fieldTypeInfo);
                case BuiltInType.Guid:
                    return fieldTypeInfo.ContainsArray()
                        ? Bind<Guid?[]>(typeMetadata, fieldTypeInfo)
                        : Bind<Guid?>(typeMetadata, fieldTypeInfo);
                case BuiltInType.ByteString:
                    return fieldTypeInfo.ContainsArray()
                        ? Bind<byte[][]>(typeMetadata, fieldTypeInfo)
                        : Bind<byte[]>(typeMetadata, fieldTypeInfo);
                default:
                    throw new UnsupportedTypeException(fieldTypeInfo.BuiltInType.ToString());
            }
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

        private IConsumerBinding Bind<type>(UaTypeMetadata typeMetadata, UATypeInfo typeInfo)
        {
            ConsumerBindingMonitoredValue<type> binding = new ConsumerBindingMonitoredValue<type>(typeInfo);
            binding.PropertyChanged += (sender, args) =>
            {
                if (_subscriptions.TryGetValue(typeMetadata, out ISubscription subscription))
                {
                    _logger?.LogInfo($"Value updated to {sender.ToString()} for subscription {subscription.UaTypeMetadata.ToString()}");
                    subscription.InvokeValueUpdated();
                }
            };
            ISubscription subscription = new Subscription(
                typeInfo,
                typeMetadata,
                _configuration.GetAliasForRepositoryGroup(typeMetadata.RepositoryGroupName),
                binding
            );
            _subscriptions[typeMetadata] = subscription;
            _consumerViewModel.AddSubscription(subscription);
            return binding;
        }
    }
}
