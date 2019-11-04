namespace HubSpot.NET.Api.EmailSubscriptions
{
    using HubSpot.NET.Api.EmailSubscriptions.Dto;
    using HubSpot.NET.Core.Abstracts;
    using HubSpot.NET.Core.Dictionaries;
    using HubSpot.NET.Core.Interfaces;
    using RestSharp;
    using System.Collections.Generic;
    using System.Linq;

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

        public SubscriptionTypeHubSpotModel GetSubscription(long id) 
            => GetSubscriptionTypes().Types.FirstOrDefault(x => x.Id == id);
        
        public SubscriptionStatusHubSpotModel GetSubscriptionStatusForContact(string email) 
            => _client.Execute<SubscriptionStatusHubSpotModel>(GetRoute(email));

        public SubscriptionTimelineHubSpotModel GetChangesTimeline()
            => _client.Execute<SubscriptionTimelineHubSpotModel>(GetRoute<SubscriptionTimelineHubSpotModel>());     

        public void UnsubscribeAll(string email) 
            => SendSubscriptionRequest(GetRoute(email), new { unsubscribeFromAll = true });

        public void UnsubscribeFrom(string email, long id)
        {
            SubscriptionStatusHubSpotModel model = new SubscriptionStatusHubSpotModel();
            model.SubscriptionStatuses.Add(new SubscriptionStatusDetailHubSpotModel(id, false, OptState.OPT_OUT));            

           SendSubscriptionRequest(GetRoute(email), model);
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

        public void SubscribeTo(string email, long id)
        {
            SubscriptionTypeHubSpotModel singleSub = GetSubscription(id);
            if (singleSub == null)
                throw new KeyNotFoundException("The SubscriptionType ID provided does not exist in the SubscriptionType list");

            SubscriptionSubscribeHubSpotModel subRequest = new SubscriptionSubscribeHubSpotModel();
            subRequest.SubscriptionStatuses.Add(new SubscriptionStatusDetailHubSpotModel(singleSub.Id, true, OptState.OPT_IN));
            
            SendSubscriptionRequest(GetRoute(email), subRequest);
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

        private void SendSubscriptionRequest(string path, object payload)
            => _client.ExecuteOnly(path, payload, Method.PUT);

        public void UnsubscribeFrom(string email, params long[] ids)
        {
            foreach (var id in ids)
            {
                UnsubscribeFrom(email, id);
            }
        }

        public void SubscribeTo(string email, params long[] ids)
        {
            foreach (var id in ids)
            {
                SubscribeTo(email, id);
            }
        }
    }
}
