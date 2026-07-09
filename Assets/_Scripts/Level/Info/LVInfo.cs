using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LVInfo : MonoBehaviour
{
    [Header("Info")]
    [SerializeField] private GameObject door;
    [SerializeField] private GameObject redButton;

    public Transform playerSpawn;

    protected DoorState doorState = DoorState.Close;
    protected ButtonState buttonState = ButtonState.Unpressed;

    protected IEnumerator DoorAnimationRoutine(string playAnimName, string idleAnimName, DoorState finalState)
    {
        Animator doorAnim = door.GetComponentInChildren<Animator>();
        doorAnim.Play(playAnimName);
        yield return null;

        float animLength = doorAnim.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(animLength);

        doorAnim.Play(idleAnimName);
        doorState = finalState;
    }

    protected IEnumerator ButtonAnimationRoutine(string playAnimName, string idleAnimName, ButtonState finalState)
    {
        Animator buttonAnim = redButton.GetComponentInChildren<Animator>();
        buttonAnim.Play(playAnimName);
        yield return null;

        float animLength = buttonAnim.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(animLength);

        buttonAnim.Play(idleAnimName);
        buttonState = finalState;
    }
}

public enum DoorState
{
    Open,
    Close,
    Opening,
    Closing
}

public enum ButtonState
{
    Pressed,
    Unpressed,
    Pressing,
    Unpressing
}
