using UnityEngine;

public class PlayerCrouching : MonoBehaviour
{
    CharacterController controller;

    private void Awake()
    {
        controller =  GetComponent<CharacterController>();
    }
    public void Toggle(float crouchHeight, float centerMult)
    {
        controller.height = crouchHeight;
        controller.center = new Vector3(0, crouchHeight * centerMult, 0);
    }
}
