using UnityEngine;

namespace Movement
{
    public class HangingMovement : MonoBehaviour
    {
        public void Move(Vector3 velocity)
        {
            Debug.Log("Swinging");
        }
        public void Jump(float force, float cooldown)
        {
            Debug.Log("Jumping");
        }
    }
}