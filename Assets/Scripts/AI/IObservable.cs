namespace AI
{
    public interface IObservable
    {
        void Subscribe(IObserver enemy);
        void Unsubscribe(IObserver enemy);
        void Notify(PlayerDataType dataType);
    }
}
