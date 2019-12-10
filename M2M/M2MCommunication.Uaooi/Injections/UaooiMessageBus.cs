﻿using M2MCommunication.Core;
using M2MCommunication.Core.Exceptions;
using System;
using System.ComponentModel.Composition;
using System.IO;
using System.Threading.Tasks;
using UAOOI.Configuration.Networking;
using UAOOI.Networking.Core;
using UAOOI.Networking.SemanticData;

namespace M2MCommunication.Uaooi.Injections
{
    [Export(typeof(IMessageBus))]
    public class UaooiMessageBus : DataManagementSetup, IMessageBus
    {
        private readonly ILogger _logger;

        [ImportingConstructor]
        public UaooiMessageBus(
            IConfigurationFactory configurationFactory,
            IEncodingFactory encodingFactory,
            IBindingFactory subscriptionFactory,
            IMessageHandlerFactory messageHandlerFactory,
            ILogger logger)
        {
            ConfigurationFactory = configurationFactory
                ?? throw new ComponentNotInitialisedException(nameof(configurationFactory));
            EncodingFactory = encodingFactory
                ?? throw new ComponentNotInitialisedException(nameof(encodingFactory));
            BindingFactory = subscriptionFactory
                ?? throw new ComponentNotInitialisedException(nameof(subscriptionFactory));
            MessageHandlerFactory = messageHandlerFactory
                ?? throw new ComponentNotInitialisedException(nameof(messageHandlerFactory));
            _logger = logger;
        }

        /// <summary>
        /// Starts this instance - Initializes the data set infrastructure, enables all associations and starts pumping the data;
        /// </summary>
        /// <param name="onSubsctiptionAdded">Callback invoked each time a subscription is created</param>
        /// <exception cref="ComponentNotInitialisedException"></exception>
        /// <exception cref="ConfigurationFileNotFoundException"></exception>
        /// <exception cref="UnsupportedTypeException"></exception>
        /// <exception cref="ValueRankOutOfRangeException"></exception>
        public void Initialise(Action<object, ISubscription> onSubsctiptionAdded)
        {
            _logger?.LogInfo("Initialising subscription logic");
            (BindingFactory as ISubscriptionFactory).SubscriptionAdded += new EventHandler<ISubscription>((sender, subscription) =>
            {
                _logger?.LogInfo($"Adding a subscription for: {subscription.UaTypeMetadata.ToString()}");
                onSubsctiptionAdded(sender, subscription);
            });

            _logger?.LogInfo("Starting communication");
            Start();
        }

        /// <summary>
        /// Starts this instance - Initializes the data set infrastructure, enables all associations and starts pumping the data;
        /// </summary>
        /// <param name="onSubsctiptionAdded">Callback invoked each time a subscription is created</param>
        /// <exception cref="ComponentNotInitialisedException"></exception>
        /// <exception cref="ConfigurationFileNotFoundException"></exception>
        /// <exception cref="UnsupportedTypeException"></exception>
        /// <exception cref="ValueRankOutOfRangeException"></exception>
        public async Task InitialiseAsync(Action<object, ISubscription> onSubsctiptionAdded)
        {
            await Task.Run(() => Initialise(onSubsctiptionAdded));
        }

        public void RefreshConfiguration()
        {
            _logger?.LogInfo("Reloading consumer configuration and restarting communication");
            Start();
        }
    }
}
