namespace HubSpot.NET.Api.EmailSubscriptions
{
    using HubSpot.NET.Api.EmailSubscriptions.Dto;
    using HubSpot.NET.Core.Abstracts;
    using HubSpot.NET.Core.Dictionaries;
    using HubSpot.NET.Core.Interfaces;
    using RestSharp;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public class HubSpotEmailSubscriptionsApi : ApiRoutable, IHubSpotEmailSubscriptionsApi
    {
        private readonly IHubSpotClient _client;
        public override string MidRoute => "/email/public/v1/subscriptions";

        public HubSpotEmailSubscriptionsApi(IHubSpotClient client)
        {
            _client = client;
            AddRoute<SubscriptionTimelineHubSpotModel>("timeline");
        }

        public SubscriptionTypeListHubSpotModel GetSubscriptionTypes() 
            => _client.Execute<SubscriptionTypeListHubSpotModel>(GetRoute());

        public Task<SubscriptionTypeListHubSpotModel> GetSubscriptionTypesAsync(CancellationToken cancellationToken = default) 
            => _client.ExecuteAsync<SubscriptionTypeListHubSpotModel>(GetRoute(), cancellationToken: cancellationToken);

        public SubscriptionTypeHubSpotModel GetSubscription(long id) 
            => GetSubscriptionTypes().Types.FirstOrDefault(x => x.Id == id);

        public async Task<SubscriptionTypeHubSpotModel> GetSubscriptionAsync(long id, CancellationToken cancellationToken = default)
        {
            var data = await GetSubscriptionTypesAsync(cancellationToken).ConfigureAwait(false);
            return data.Types.FirstOrDefault(x => x.Id == id);
        }
        
        public SubscriptionStatusHubSpotModel GetSubscriptionStatusForContact(string email) 
            => _client.Execute<SubscriptionStatusHubSpotModel>(GetRoute(email));

        public Task<SubscriptionStatusHubSpotModel> GetSubscriptionStatusForContactAsync(string email, CancellationToken cancellationToken = default) 
            => _client.ExecuteAsync<SubscriptionStatusHubSpotModel>(GetRoute(email), cancellationToken: cancellationToken);

        public SubscriptionTimelineHubSpotModel GetChangesTimeline()
            => _client.Execute<SubscriptionTimelineHubSpotModel>(GetRoute<SubscriptionTimelineHubSpotModel>());     

        public Task<SubscriptionTimelineHubSpotModel> GetChangesTimelineAsync(CancellationToken cancellationToken = default)
            => _client.ExecuteAsync<SubscriptionTimelineHubSpotModel>(GetRoute<SubscriptionTimelineHubSpotModel>(), cancellationToken: cancellationToken);     

        public void UnsubscribeAll(string email) 
            => SendSubscriptionRequest(GetRoute(email), new { unsubscribeFromAll = true });

        public Task UnsubscribeAllAsync(string email, CancellationToken cancellationToken = default) 
            => SendSubscriptionRequestAsync(GetRoute(email), new { unsubscribeFromAll = true }, cancellationToken: cancellationToken);

        public void UnsubscribeFrom(string email, long id)
        {
            SubscriptionStatusHubSpotModel model = new SubscriptionStatusHubSpotModel();
            model.SubscriptionStatuses.Add(new SubscriptionStatusDetailHubSpotModel(id, false, OptState.OPT_OUT));            

           SendSubscriptionRequest(GetRoute(email), model);
        }

        public Task UnsubscribeFromAsync(string email, long id, CancellationToken cancellationToken = default)
        {
            SubscriptionStatusHubSpotModel model = new SubscriptionStatusHubSpotModel();
            model.SubscriptionStatuses.Add(new SubscriptionStatusDetailHubSpotModel(id, false, OptState.OPT_OUT));            

            return SendSubscriptionRequestAsync(GetRoute(email), model, cancellationToken);
        }

        public void SubscribeAll(string email)
        {
            List<SubscriptionStatusDetailHubSpotModel> subs = new List<SubscriptionStatusDetailHubSpotModel>();
            SubscriptionSubscribeHubSpotModel subRequest = new SubscriptionSubscribeHubSpotModel();

            GetSubscriptionTypes().Types.ForEach(sub =>
            {
                subs.Add(new SubscriptionStatusDetailHubSpotModel(sub.Id, true, OptState.OPT_IN));
            });

            subRequest.SubscriptionStatuses.AddRange(subs);

            SendSubscriptionRequest(GetRoute(email), subRequest);
        }

        public Task SubscribeAllAsync(string email, CancellationToken cancellationToken = default)
        {
            List<SubscriptionStatusDetailHubSpotModel> subs = new List<SubscriptionStatusDetailHubSpotModel>();
            SubscriptionSubscribeHubSpotModel subRequest = new SubscriptionSubscribeHubSpotModel();

            GetSubscriptionTypes().Types.ForEach(sub =>
            {
                subs.Add(new SubscriptionStatusDetailHubSpotModel(sub.Id, true, OptState.OPT_IN));
            });

            subRequest.SubscriptionStatuses.AddRange(subs);

            return SendSubscriptionRequestAsync(GetRoute(email), subRequest, cancellationToken);
        }

        public void SubscribeAll(string email, GDPRLegalBasis legalBasis, string explanation, OptState optState = OptState.OPT_IN)
        {
            List<SubscriptionStatusDetailHubSpotModel> subs = new List<SubscriptionStatusDetailHubSpotModel>();
            SubscriptionSubscribeHubSpotModel subRequest = new SubscriptionSubscribeHubSpotModel();

            GetSubscriptionTypes().Types.ForEach(sub =>
            {
                subs.Add(new SubscriptionStatusDetailHubSpotModel(sub.Id, true, optState, legalBasis, explanation));
            });

            subRequest.SubscriptionStatuses.AddRange(subs);

            SendSubscriptionRequest(GetRoute(email), subRequest);
        }

        public Task SubscribeAllAsync(string email, GDPRLegalBasis legalBasis, string explanation, OptState optState = OptState.OPT_IN, CancellationToken cancellationToken = default)
        {
            List<SubscriptionStatusDetailHubSpotModel> subs = new List<SubscriptionStatusDetailHubSpotModel>();
            SubscriptionSubscribeHubSpotModel subRequest = new SubscriptionSubscribeHubSpotModel();

            GetSubscriptionTypes().Types.ForEach(sub =>
            {
                subs.Add(new SubscriptionStatusDetailHubSpotModel(sub.Id, true, optState, legalBasis, explanation));
            });

            subRequest.SubscriptionStatuses.AddRange(subs);

            return SendSubscriptionRequestAsync(GetRoute(email), subRequest, cancellationToken);
        }

        public void SubscribeTo(string email, long id)
        {
            SubscriptionTypeHubSpotModel singleSub = GetSubscription(id);
            if (singleSub == null)
                throw new KeyNotFoundException("The SubscriptionType ID provided does not exist in the SubscriptionType list");

            SubscriptionSubscribeHubSpotModel subRequest = new SubscriptionSubscribeHubSpotModel();
            subRequest.SubscriptionStatuses.Add(new SubscriptionStatusDetailHubSpotModel(singleSub.Id, true, OptState.OPT_IN));
            
            SendSubscriptionRequest(GetRoute(email), subRequest);
        }

        public Task SubscribeToAsync(string email, long id, CancellationToken cancellationToken = default)
        {
            SubscriptionTypeHubSpotModel singleSub = GetSubscription(id);
            if (singleSub == null)
                throw new KeyNotFoundException("The SubscriptionType ID provided does not exist in the SubscriptionType list");

            SubscriptionSubscribeHubSpotModel subRequest = new SubscriptionSubscribeHubSpotModel();
            subRequest.SubscriptionStatuses.Add(new SubscriptionStatusDetailHubSpotModel(singleSub.Id, true, OptState.OPT_IN));
            
            return SendSubscriptionRequestAsync(GetRoute(email), subRequest, cancellationToken);
        }

        public void SubscribeTo(string email, long id, GDPRLegalBasis legalBasis, string explanation, OptState optState = OptState.OPT_IN)
        {
            SubscriptionTypeHubSpotModel singleSub = GetSubscription(id);
            SubscriptionSubscribeHubSpotModel subRequest = new SubscriptionSubscribeHubSpotModel();

            if (singleSub == null)
                throw new KeyNotFoundException("The SubscriptionType ID provided does not exist in the SubscriptionType list");

            subRequest.SubscriptionStatuses.Add(new SubscriptionStatusDetailHubSpotModel(singleSub.Id, true, optState, legalBasis, explanation));

            SendSubscriptionRequest(GetRoute(email), subRequest);
        }        

        public Task SubscribeToAsync(string email, long id, GDPRLegalBasis legalBasis, string explanation, OptState optState = OptState.OPT_IN, CancellationToken cancellationToken = default)
        {
            SubscriptionTypeHubSpotModel singleSub = GetSubscription(id);
            SubscriptionSubscribeHubSpotModel subRequest = new SubscriptionSubscribeHubSpotModel();

            if (singleSub == null)
                throw new KeyNotFoundException("The SubscriptionType ID provided does not exist in the SubscriptionType list");

            subRequest.SubscriptionStatuses.Add(new SubscriptionStatusDetailHubSpotModel(singleSub.Id, true, optState, legalBasis, explanation));

            return SendSubscriptionRequestAsync(GetRoute(email), subRequest, cancellationToken);
        }        

        private void SendSubscriptionRequest(string path, object payload)
            => _client.ExecuteOnly(path, payload, Method.PUT);

        private Task SendSubscriptionRequestAsync(string path, object payload, CancellationToken cancellationToken = default)
            => _client.ExecuteOnlyAsync(path, payload, Method.PUT, cancellationToken);

        public void UnsubscribeFrom(string email, params long[] ids)
        {
            foreach (var id in ids)
            {
                UnsubscribeFrom(email, id);
            }
        }

        public async Task UnsubscribeFromAsync(string email, CancellationToken cancellationToken = default, params long[] ids)
        {
            foreach (var id in ids)
            {
                await UnsubscribeFromAsync(email, id, cancellationToken).ConfigureAwait(false);
            }
        }

        public void SubscribeTo(string email, params long[] ids)
        {
            foreach (var id in ids)
            {
                SubscribeTo(email, id);
            }
        }

        public async Task SubscribeToAsync(string email, CancellationToken cancellationToken = default, params long[] ids)
        {
            foreach (var id in ids)
            {
                await SubscribeToAsync(email, id, cancellationToken).ConfigureAwait(false);
            }
        }
    }
}
