using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


namespace DOTween
{
    public sealed class DOTweenButtonView : Button
    {

        #region Fields

        public static string ButtonRectField = nameof(_buttonRectTransform);
        public static string Window = nameof(_windowForClosing);

        private readonly float _duration = 0.5f;
        private readonly float _strength = 5.0f;

        private PointerEventData _eventData;

        [SerializeField]
        private RectTransform _buttonRectTransform;

        [SerializeField]
        private DOTweenWindowView _windowForClosing;

        #endregion


        #region UnityMethods

        public override void OnPointerClick(PointerEventData eventData)
        {
            _eventData = eventData;
            RunDOTWeenAnimation();
        }

        #endregion


        #region Methods

        private void RunDOTWeenAnimation()
        {
            var sequence = DG.Tweening.DOTween.Sequence();
            sequence.Insert(0.0f, _buttonRectTransform.DOShakeAnchorPos(_duration, strength: _strength));
            if (_windowForClosing != null)
            {
                sequence.Insert(0.0f, _windowForClosing.CloseWindow());
            }
            sequence.OnComplete(OnAnimationFinish);
        }

        private void OnAnimationFinish()
        {
            base.OnPointerClick(_eventData);
        }

        #endregion

    }
}