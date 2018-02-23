using System;
using System.Collections.Generic;
using OnePiece.App.Services;
using OnePiece.App.Droid.Services;
using System.Threading.Tasks;
using OnePiece.App.Models;
using Xamarin.Forms;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;

[assembly: Dependency(typeof(AzureDocumentDBService))]
namespace OnePiece.App.Droid.Services
{
    public class AzureDocumentDBService : IAzureDocumentDBService
    {
        const string accountURL = @"https://opapp.documents.azure.com:443/";
        const string accountKey = @"20XxxZ9EvmwQ3XzI4LtTW9d6M1ifa3I1FWsIGGEGd4XwsltDUCMcthaLVXzb8lYLHn85ZEI3Q8nz1J2LfzjFAg==";
        const string databaseId = @"OnePieceDB";

        private DocumentClient client;

        public AzureDocumentDBService()
        {
            client = new DocumentClient(new System.Uri(accountURL), accountKey);
        }

        public async Task<List<NewsFeed>> GetNewsFeeds()
        {
            try
            {
                var collectionLink = UriFactory.CreateDocumentCollectionUri(databaseId, "NewsFeeds");
                var newsFeeds = new List<NewsFeed>();
                // The query excludes completed TodoItems
                var query = client.CreateDocumentQuery<NewsFeed>(collectionLink, new FeedOptions { MaxItemCount = -1 })
                              .AsDocumentQuery();
                
                while (query.HasMoreResults)
                {
                    newsFeeds.AddRange(await query.ExecuteNextAsync<NewsFeed>());
                }

                return newsFeeds;
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(@"ERROR {0}", e.Message);
                return null;
            }
        }
    }
}