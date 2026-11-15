using UnityEngine;
using VContainer;

/// <summary>
/// ѕосредник между машиной состо€ний перемещени€ игрока и Unity.
/// ќтвечает за вызов метода Tick() машины состо€ний на каждом физическом кадре (FixedUpdate).
/// </summary>
public class PlayerMovementStateMachineDriver : MonoBehaviour
{
    [Inject] private PlayerMovementStateMachine stateMachine;

    private void FixedUpdate()
    {
        stateMachine.Tick(Time.fixedDeltaTime);
    }
}