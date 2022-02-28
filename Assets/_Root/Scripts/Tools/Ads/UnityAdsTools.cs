using System;
using UnityEngine;
using UnityEngine.Advertisements;

namespace RaceMobile.Tools.Ads
{
    internal class UnityAdsTools : MonoBehaviour, IAdsShower, IUnityAdsListener
    {
        private string gameID = "4607623";
        private string rewardPlace = "Rewarded_Android";
        private string interstitialPlace = "Interstitial_Android";

        private Action callbackSuccessShowVideo;

        private void Start()
        {
            Advertisement.Initialize(gameID, true);
        }

        public void OnUnityAdsDidError(string message)
        {
            throw new NotImplementedException();
        }

        public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
        {
            if (showResult == ShowResult.Finished)
                callbackSuccessShowVideo?.Invoke();
        }

        public void OnUnityAdsDidStart(string placementId)
        {
            throw new NotImplementedException();
        }

        public void OnUnityAdsReady(string placementId)
        {
            throw new NotImplementedException();
        }

        public void ShowInterstitial()
        {
            callbackSuccessShowVideo = null;
            Advertisement.Show(interstitialPlace);
        }

        public void ShowVideo(Action sucessShow)
        {
            callbackSuccessShowVideo = sucessShow;
            Advertisement.Show(rewardPlace);
        }
    }
}
