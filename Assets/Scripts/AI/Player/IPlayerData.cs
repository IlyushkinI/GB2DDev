namespace AI
{
    public interface IPlayerData
    {
        bool TrySetData(PlayerDataType dataType, int value);
        int GetData(PlayerDataType dataType);
    }
}