﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Confluent.Kafka;
using Confluent.Kafka.Serialization;
using Microsoft.Extensions.Logging;

namespace Messaging.Kafka
{
    /// <summary>
    /// Base class for reading objects from kafka. 
    /// </summary>
    public class ObjectMessageConsumer : IObjectMessageConsumer
    {
        private readonly Consumer<string, object> _wrappedConsumer;

        /// <summary>
        ///  <inheritdoc cref="IObjectMessageConsumer"/>
        /// </summary>
        public string Name => _wrappedConsumer.Name;

        /// <summary>
        /// Creates and object message consumer
        /// </summary>
        /// <param name="consumer">The kafka consumer that we wrap</param>
        public ObjectMessageConsumer(Consumer<string, object> consumer)
        {
            _wrappedConsumer = consumer ?? throw new ArgumentNullException(nameof(consumer));
        }

        /// <summary>
        ///  <inheritdoc cref="IObjectMessageConsumer"/>
        /// </summary>
        bool IObjectMessageConsumer.Consume<TMessage>(out Message<string, TMessage> message, TimeSpan timeout)
        {
            var result = _wrappedConsumer.Consume(out Message<string, object> fetched, Convert.ToInt32(timeout.TotalMilliseconds));
            message = fetched?.Repackage(fetched.Value as TMessage);
            return result;
        }

        /// <summary>
        ///  <inheritdoc cref="IDisposable"/>
        /// </summary>
        public void Dispose()
        {
            (_wrappedConsumer as IDisposable)?.Dispose();
        }
    }
}