using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WhatsApp_Webhook.Hubs
{
    public interface IDatabaseSubscription
    {
        void Configure(string connectionString);
    }
}
