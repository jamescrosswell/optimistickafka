﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Writ.Messaging.Kafka
{
    public class KafkaConfig : Dictionary<string, object>
    {
        public KafkaConfig(Dictionary<string, object> config)
            : base(config)
        {
            
        }
    }
}