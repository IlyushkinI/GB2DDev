using RaceMobile.Base;
using RaceMobile.Tools.ResourceManagment;
using RaceMobile.Tools.Reactive;
using UnityEngine;




namespace RaceMobile.Inputs
{
    internal sealed class InputController : BaseController
    {
        private readonly ResourcePath path = new ResourcePath { PathResource = "Prefabs/Inputs/JoysticMove" };
        private BaseInputView inputView;

        public InputController(SubscriptionProperty<float> leftMove, SubscriptionProperty<float> rightMove)
        {
            inputView = LoadView();
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