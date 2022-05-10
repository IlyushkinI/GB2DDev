namespace AI
{
    public interface IObserver
    {
        void Catch(float data);
        void Catch(PlayerDataType dataType, IPlayerData data);
    }
}