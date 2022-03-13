using UnityEngine;
using DG.Tweening;

namespace RaceMobile.Car
{
    internal class CarView : MonoBehaviour
    {
        [SerializeField] public GameObject BackWheel;
        [SerializeField] public GameObject ForwardWheel;

        private void Start()
        {
            var sequence = DOTween.Sequence();
            sequence.Append(BackWheel.transform.DORotate(new Vector3(0, 0, -90), 5f, RotateMode.Fast));
            sequence.Join(ForwardWheel.transform.DOLocalRotate(new Vector3(0, 0, -90), 5f));

            sequence.Play().SetLoops(10);

            

           
        }
    }
}
