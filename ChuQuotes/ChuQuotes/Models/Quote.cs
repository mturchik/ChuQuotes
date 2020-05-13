using System;
using Newtonsoft.Json;
using SQLite;

namespace ChuQuotes.Models
{
    public class Quote
    {
        [PrimaryKey]            public string Id         { get; set; }
        [JsonProperty("value")] public string Content    { get; set; }
        [JsonIgnore]    public bool   IsSelected { get; set; }
    }
}