using RaceMobile.Base;
using RaceMobile.Tools.ResourceManagment;
using RaceMobile.Tools.Reactive;
using UnityEngine;
using DG.Tweening;

namespace RaceMobile.Car
{
    internal class CarController : BaseController
    {
        private readonly ResourcePath viewPath = new ResourcePath() { PathResource = "Prefabs/Transport/Car" };
        private CarView carView;

        private SubscriptionProperty<float> leftMove;
        private SubscriptionProperty<float> rightMove;


        public CarController(SubscriptionProperty<float> leftMove, SubscriptionProperty<float> rightMove, ProfilePlayer profile)
        {
            carView = LoadView();
            RotationWheel();

            //temp:
            //this.leftMove = leftMove;
            //this.rightMove = rightMove;


            //this.leftMove.SubscribeOnChange(RotationWheel);
            //this.rightMove.SubscribeOnChange(RotationWheel);
        }

        //protected override void OnDispose()
        //{
        //    leftMove.UnsubscribeOnChange(RotationWheel);
        //    rightMove.UnsubscribeOnChange(RotationWheel);
        //}

        private CarView LoadView()
        {
            var prefabCar = ResourceLoader.LoadPrefab(viewPath);
            var go = Object.Instantiate(prefabCar);
            AddGameObject(go);

            return go.GetComponent<CarView>();
        }


        private void RotationWheel()
        {
            
        }


        public GameObject GetViewObject()
        {
            return carView.gameObject;
        }


    }
}
