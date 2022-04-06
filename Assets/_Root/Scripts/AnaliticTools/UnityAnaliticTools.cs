using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Analytics;

namespace RaceMobile.AnaliticsTools
{
    internal class UnityAnaliticTools : IAnaliticTools
    {
        public void SendMessage(string alias, Dictionary<string, object> eventData = null)
        {
            if (eventData == null)
                eventData = new Dictionary<string, object>();
            Analytics.CustomEvent(alias, eventData);
            Debug.Log("SendMessage");
        }
    }
}
