namespace AI
{
    public interface IObserver
    {
        void Catch(PlayerDataType dataType, IPlayerData data);
    }
}