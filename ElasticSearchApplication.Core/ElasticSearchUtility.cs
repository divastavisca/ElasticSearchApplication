using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elasticsearch.Net;
using Nest;

namespace ElasticSearchApplication.Core
{
    public class ElasticSearchUtility
    {

        private ElasticClient _queryClient;
        private string _base = "sampleAirlines";
        private string _type = "entry";
        private string _searchField = "IataIdentifier";

        public bool IndexAirlineData(Airline airline)
        {
            return performIndexing(airline) != null;
        }

        private IIndexResponse performIndexing(Airline airline)
        {
            return _queryClient.Index(airline, i => i
                  .Index(_base)
                  .Type(_type)
                  .Id(airline.IataIdentifier)
                  .Refresh(Refresh.True));
        }

        public IEnumerable<Airline> SearchAirline(string iataIdentifier)
        {
            ISearchResponse<Airline> results = getResults(iataIdentifier);
            List<Airline> searchResults = new List<Airline>();
            foreach(var hit in results.Hits)
            { 
                searchResults.Add(new Airline(hit.Source.Name, hit.Source.IataIdentifier, hit.Source.Description));
            }
            return searchResults;
        }

        private ISearchResponse<Airline> getResults(string iataIdentifier)
        {
            return _queryClient.Search<Airline>
                                (s => s
                               .Index(_base)
                               .Type(_type)
                               .Query(q => q.Term(t => t.Field(_searchField).Value(iataIdentifier))));
        }

        public ElasticSearchUtility()
        {
            _queryClient = ElasticSearchClient.GetNewClient();
        }

    }
}
