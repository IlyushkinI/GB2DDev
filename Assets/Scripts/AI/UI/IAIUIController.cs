namespace AI
{
    public interface IAIUIController
    {
        void Win();
        void Lose();
        bool EnableButtonGo { set; }
    }
}