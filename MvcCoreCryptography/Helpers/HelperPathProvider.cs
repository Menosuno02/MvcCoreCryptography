﻿using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;

namespace MvcCoreCryptography.Helpers
{
    public class HelperPathProvider
    {
        private IServer server;

        public HelperPathProvider(IServer server)
        {
            this.server = server;
        }

        public string MapUrlPath()
        {
            var addresses =
                server.Features.Get<IServerAddressesFeature>().Addresses;
            return addresses.FirstOrDefault();
        }
    }
}