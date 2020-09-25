using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Models
{
    public enum NotificationType
    {
        danger, info, light, success, warning
    }
    public class Message
    {
        public string Text { get; set; }
        public NotificationType Type { get; set; }
    }

    public static class Notification
    {
       public static void Set(ITempDataDictionary temp, Message message)
       {
           temp["notify"] = Newtonsoft.Json.JsonConvert.SerializeObject(message);
       }

       public static IHtmlContent Get(ITempDataDictionary temp)
       {           
           if(temp["notify"] != null)
           {
               var obj = Newtonsoft.Json.JsonConvert.DeserializeObject<Message>((string)temp["notify"]);
               string html = $"<div class=\"alert alert-{obj.Type}\">{obj.Text}</div>";
               return new HtmlContentBuilder().AppendHtml(html);
           }
           else
           {
               return new HtmlContentBuilder().AppendHtml("");
           }
       }
    }
}
