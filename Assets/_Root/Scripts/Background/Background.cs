using UnityEngine;

namespace RaceMobile.Background
{
    internal sealed class Background : MonoBehaviour
    {
        [SerializeField]
        private float leftBorder, rightBorder;

        [SerializeField]
        private float relativeSpeed;

        public void Move(float value)
        {
            transform.position += Vector3.right * relativeSpeed * value;

            var position = transform.position;

            if (position.x <= leftBorder)
                transform.position = new Vector3(rightBorder - (leftBorder - position.x), position.y, position.z);
            else if (position.x >= rightBorder)
                transform.position = new Vector3(leftBorder + (rightBorder - position.x), position.y, position.z);

        }

    }
}
