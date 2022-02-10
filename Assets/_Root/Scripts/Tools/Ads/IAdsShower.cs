using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaceMobile.Tools.Ads
{
    internal interface IAdsShower
    {
        void ShowInterstitial();
        void ShowVideo(Action sucessShow);
    }
}
