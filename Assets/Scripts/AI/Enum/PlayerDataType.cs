namespace AI
{
    public enum PlayerDataType
    {
        None = 0b_0000_0001,
        Money = None << 1,
        Health = None << 2,
        Force = None << 3,
    }

    public static class Extensions
    {
        public static string ToUnit(this PlayerDataType playerData)
        {
            string result = "";
            switch (playerData)
            {
                case PlayerDataType.Money:
                    result = "$";
                    break;
                case PlayerDataType.Health:
                    result = "♥";
                    break;
                case PlayerDataType.Force:
                    result = "†";
                    break;
                default:
                    break;
            }
            return result;
        }
    }
}
