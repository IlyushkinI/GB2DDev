namespace AI
{
    public interface IObservable
    {
        void Subscribe(IObserver observer);
        void Unsubscribe(IObserver observer);
        void Notify(PlayerDataType dataType);
    }
}
