namespace Reward
{
    public enum UIElement
    {
        None = 0b_0000_0001,
        ButtonGetReward = None << 1,
        ButtonResetTimer = None << 2,
        ButtonExit = None << 3,
    }
}