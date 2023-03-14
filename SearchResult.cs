using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MauiApp1
{
    public class IviSearchResult
        {
        [JsonPropertyName("years")]
        public List<int> Years { get; set; }

        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("object_type")]
        public string ObjectType { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("orig_title")]
        public string OriginalTitle { get; set; }

        [JsonPropertyName("has_5_1")]
        public bool Has_51 { get; set; }

        [JsonPropertyName("year")]
        public int? Year { get; set; }
    }
    public class IviSearch {
        [JsonPropertyName("result")]
        public List<IviSearchResult> SearchResult { get; set; }
    }
}