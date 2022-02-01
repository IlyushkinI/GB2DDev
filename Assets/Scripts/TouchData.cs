using UnityEngine;

namespace Profile
{
    public struct TouchData
    {
        public int ID;
        public Vector2 Position;
        public TouchPhase Phase;

        public TouchData(Touch touch)
        {
            ID = touch.fingerId;
            Position = touch.position;
            Phase = touch.phase;
        }
    }
}