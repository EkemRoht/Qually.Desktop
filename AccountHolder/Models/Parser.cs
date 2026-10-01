using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Text;

namespace AccountHolder
{
    public static class Parser
    {
        private static readonly HtmlParser parser = new HtmlParser();

        public static IHtmlDocument ParseDocument(string html)
        {
            IHtmlDocument document = parser.ParseDocument(html);
            return document;
        }

        public static IHtmlDocument ParseDocument(string json, string selector)
        {
            JObject jobj = JObject.Parse(json);
            string html = jobj[selector][0].Values()[0].ToString();
            IHtmlDocument document = parser.ParseDocument(html);
            return document;
        }

        public static IHtmlDocument ParseJsonDocument(string json)
        {
            var root = JToken.Parse(json);
            var tokens = root is JContainer container ? container.Descendants() : new[] { root };
            var html = new StringBuilder();
            foreach (var value in tokens.OfType<JValue>().Where(v => v.Type == JTokenType.String))
            {
                html.Append(value.Value<string>());
            }
            return parser.ParseDocument(html.ToString());
        }
    }
}
