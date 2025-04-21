/*-------------------------
* Script Name: SequenceListener.cs
* Author: Connor
* -------------------------
* DESCRIPTION:
* SequenceListener essentially monitors the SequenceEmitter until a matching Sequence is emitted.
* It then sends the message to Toggle any IAction component attached to this GameObject.
* 
* NOTES:
* SequenceListener will ALWAYS send the Toggle. If you want specific behaviour, use a oneshot property on a new IAction.
*/
using System.Collections.Generic;
using UnityEngine;

public class SequenceListener : MonoBehaviour
{
    public Sequence targetSequence;

    private void Start()
    {
        if (targetSequence == null) Debug.LogWarning(gameObject.name + ": Sequence Listener has no Sequence set. No actions will toggle.");
    }

    private void OnEnable()
    {
        SequenceEmitter.onEmitSequence += ReceiveSequence;
    }
    private void OnDisable()
    {
        SequenceEmitter.onEmitSequence -= ReceiveSequence;
    }

    private void ReceiveSequence(List<Element> inputSequence)
    {
        if (targetSequence.Match(inputSequence))
        {
            SequenceEmitter.valid = true;
            Debug.Log(gameObject.name + " matches the given sequence.");
            SendMessage("Toggle");
        }
    }
}
