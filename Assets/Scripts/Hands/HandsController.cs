using System;
using UnityEngine;

public class HandsController : MonoBehaviour
{
    private PlayerInput input;
    [SerializeField] Hand leftHand, rightHand;

    private void Awake()
    {
        input = GetComponent<PlayerInput>();
    }

    private void FixedUpdate()
    {
        leftHand.Toggle(input.IsLeftHandActive);
        rightHand.Toggle(input.IsRightHandActive);
    }
}
