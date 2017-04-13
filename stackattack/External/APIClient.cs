using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RestSharp;

namespace stackattack.External
{
    public abstract class APIClient
    {
        protected abstract Uri BaseUrl { get; }

        protected virtual void SetClientDefaults(ref IRestClient client)
        {
            // For extension.
        }

        protected IRestClient GetClient()
        {
            IRestClient client = new RestClient(this.BaseUrl);
            SetClientDefaults(ref client);
            return client;
        }
    }
}