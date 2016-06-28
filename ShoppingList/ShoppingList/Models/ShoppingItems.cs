using Newtonsoft.Json;

namespace ShoppingList
{
    public class ShoppingItems
    {
        public string Id { get; set; }

        [JsonProperty(PropertyName = "item")]
        public string Item { get; set; }

        [JsonProperty(PropertyName = "bought")]
        public bool Bought { get; set; }
    }
}