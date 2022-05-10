namespace AI
{
    public enum PlayerDataType
    {
        None = 0b_0000_0001,
        Money = None << 1,
        Health = None << 2,
        Force = None << 3,
    }
}
