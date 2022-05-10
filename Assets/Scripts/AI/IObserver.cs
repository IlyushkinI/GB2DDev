namespace AI
{
    public interface IObserver
    {
        void Catch(PlayerDataTypes dataType, IPlayerData data);
    }
}