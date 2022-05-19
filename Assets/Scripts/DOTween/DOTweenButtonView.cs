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

        private readonly float _duration = 1.0f;
        private readonly float _strength = 5.0f;

        [SerializeField]
        private RectTransform _buttonRectTransform;

        #endregion


        #region UnityMethods

        public override void OnPointerClick(PointerEventData eventData)
        {
            base.OnPointerClick(eventData);

            RunDOTWeenAnimation();
        }

        #endregion


        #region Methods

        private void RunDOTWeenAnimation()
        {
            _buttonRectTransform.DOShakeAnchorPos(_duration, strength: _strength);
        }

        #endregion
    
    }
}