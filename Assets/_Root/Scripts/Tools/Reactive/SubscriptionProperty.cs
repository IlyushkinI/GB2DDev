using System;

namespace Game.Tools.Reactive
{
    internal class SubscriptionProperty<T> : IReadOnlySubscriptionProperty<T>
    {
        private T value;

        public Action<T> OnChangeValue;
        public T Value {
            get { return value; }
            set
            {
                this.value = value;
                OnChangeValue?.Invoke(this.value);
            }
        }

        public void SubscribeOnChange(Action<T> subsriptionAction)
        {
            OnChangeValue += subsriptionAction;
        }

        public void UnsubscribeOnChange(Action<T> unsubscriptionAction)
        {
            OnChangeValue -= unsubscriptionAction;
        }
    }


}
