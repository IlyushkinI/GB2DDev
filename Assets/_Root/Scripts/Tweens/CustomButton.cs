using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;

namespace RaceMobile.Tweens
{
    [RequireComponent(typeof(RectTransform))]
    public class CustomButton : Button
    {
        public const string EasingFieldName = nameof(easeing);
        public const string TransitionFieldName = nameof(transitionType);
        public const string DurationFieldName = nameof(duration);
        public const string PowerFieldName = nameof(power);


        [SerializeField] private Ease easeing = Ease.Linear;
        [SerializeField] private TransitionType transitionType;
        [SerializeField] private float duration;
        [SerializeField] private float power;

        private Tween activeTween;

        public override void OnPointerClick(PointerEventData eventData)
        {
            base.OnPointerClick(eventData);
            ShowAnimation();
        }

        private void ShowAnimation()
        {
            switch (transitionType)
            {
                case TransitionType.None:
                    break;
                case TransitionType.Rotation:
                    activeTween?.Complete(true);
                    activeTween = (transform as RectTransform).DOShakeRotation(duration, power * Vector3.forward)
                        .SetEase(easeing)
                        .OnComplete(() => transform.rotation = Quaternion.identity)
                        .OnComplete(() => activeTween = null);
                    break;
                case TransitionType.Scale:
                    activeTween?.Complete();

                    activeTween = (transform as RectTransform).DOShakeScale(duration, power, 4).SetEase(easeing).OnComplete(() => activeTween = null);
                    break;
            }
        }


    }

    internal enum TransitionType
    {
        None,
        Rotation,
        Scale
    }
}