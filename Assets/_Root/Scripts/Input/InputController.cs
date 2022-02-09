using RaceMobile.Base;
using RaceMobile.Tools.ResourceManagment;
using RaceMobile.Tools.Reactive;
using UnityEngine;




namespace RaceMobile.Input
{
    internal sealed class InputController : BaseController
    {
        private readonly ResourcePath path = new ResourcePath { PathResource = "Prefabs/Input" };
        private BaseInputView inputView;

        public InputController(SubscriptionProperty<float> leftMove, SubscriptionProperty<float> rightMove)
        {
            inputView.Init(leftMove, rightMove);
        }

        private BaseInputView LoadView()
        {
            var pref = ResourceLoader.LoadPrefab(path);
            var go = Object.Instantiate(pref);

            AddGameObject(go);


            return go.GetComponent<BaseInputView>();
        }

    }
}