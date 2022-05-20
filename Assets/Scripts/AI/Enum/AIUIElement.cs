namespace AI
{
    public enum AIUIElement
    {
        None = 0b_0000_0001,
        ButtonHealthAdd = None << 1,
        ButtonHealthSub = None << 2,
        ButtonMoneyAdd = None << 3,
        ButtonMoneyhSub = None << 4,
        ButtonForceAdd = None << 5,
        ButtonForceSub = None << 6,
        ButtonFight = None << 7,
        SliderCrime = None << 8,
        ButtonGo = None << 9,
        ButtonExit = None << 10,
        ButtonRestart = None << 11,

        ConfigElements = 
            ButtonHealthAdd | 
            ButtonHealthSub | 
            ButtonMoneyAdd | 
            ButtonMoneyhSub | 
            ButtonForceAdd | 
            ButtonForceSub |
            SliderCrime
    }
}
