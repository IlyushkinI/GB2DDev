using System.Collections.Generic;

namespace RaceMobile.AnaliticsTools
{
    internal interface IAnaliticTools
    {
        void SendMessage(string alias, Dictionary<string, object> eventData = null);
    }
}