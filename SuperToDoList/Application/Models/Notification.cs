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
    public class Notification
    {
        public string Message { get; set; }
        public NotificationType Type { get; set; }
    }

    //public static class Notification
    //{
    //    public void Set(ITempDataDictionary temp, )
    //}
}
