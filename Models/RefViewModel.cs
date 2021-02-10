using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UWA.Models
{
    public class RefViewModel
    {
        public RefViewModel()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        [JsonProperty(PropertyName = "rame")]
        public string Ref { get; set; }
    }
}
