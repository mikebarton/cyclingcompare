using bikecompare.import.commissionfactory.Models;
using bikecompare.import.commissionfactory.Options;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace bikecompare.import.services.commissionfactory
{
    public class CommissionFactoryService
    {
        private readonly HttpClient _httpClient;
        private readonly CommissionFactoryOptions _options;

        public CommissionFactoryService(HttpClient httpClient, IOptions<CommissionFactoryOptions> options)
        {
            _httpClient = httpClient;
            _options = options.Value;
        }

        public async Task<Merchant[]> GetMerchants()
        {
            var path = string.Format(_options.GetMerchantsPath, _options.ApiKey);
            var responseJson = await _httpClient.GetStringAsync(path);
            var merchants = JsonConvert.DeserializeObject<Merchant[]>(responseJson);
            return merchants;
        }

        public async Task<Banner[]> GetAllBanners()
        {
            var path = string.Format(_options.GetAllBannersPath, _options.ApiKey);
            var responseJson = await _httpClient.GetStringAsync(path);
            var banners = JsonConvert.DeserializeObject<Banner[]>(responseJson);
            return banners;
        }

        public async Task<List<DataFeed>> GetDataFeedsWithoutItems(string merchantId)
        {
            var path = string.Format(_options.GetDataFeedsPath, _options.ApiKey, merchantId);
            var responseJson = await _httpClient.GetStringAsync(path);
            var feeds = JsonConvert.DeserializeObject<List<DataFeed>>(responseJson);
            return feeds;
        }

        public async Task<DataFeed> GetDataFeedWithItems(string feedId)
        {
            var path = string.Format(_options.GetDataFeedPath, feedId, _options.ApiKey);
            var responseJson = await _httpClient.GetStringAsync(path);
            var feed = JsonConvert.DeserializeObject<DataFeed>(responseJson);
            return feed;
        }
    }
}
