using System;

namespace RaceMobile.Tools.Reactive
{
    interface IReadOnlySubscriptionProperty<T>
    {
        T Value { get; }
        void SubscribeOnChange(Action<T> subsriptionAction);
        void UnsubscribeOnChange(Action<T> unsubscriptionAction);
    }


}
