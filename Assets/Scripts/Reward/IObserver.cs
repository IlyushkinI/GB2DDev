namespace Reward
{
    public interface IObserver
    {
        void Catch(Currency currency, int value);
    }
}