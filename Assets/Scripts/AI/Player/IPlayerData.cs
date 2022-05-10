namespace AI
{
    public interface IPlayerData
    {
        bool TrySetData(PlayerDataTypes dataType, int value);
        int GetData(PlayerDataTypes dataType);
    }
}