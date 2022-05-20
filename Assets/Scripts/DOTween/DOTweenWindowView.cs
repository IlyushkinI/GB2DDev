using DG.Tweening;
using UnityEngine;


namespace DOTween
{
    public sealed class DOTweenWindowView : MonoBehaviour
    {

        [SerializeField]
        private RectTransform _rectTransform;

        private float _duration = 2.0f;

        private void OnEnable()
        {
            OpenWindow();
        }

        public Tween OpenWindow()
        {
            _rectTransform.localScale = Vector3.zero;
            return _rectTransform.DOScale(Vector3.one, _duration);
        }

        public Tween CloseWindow()
        {
            return _rectTransform.DOScale(Vector3.zero, _duration);
        }
    }
}