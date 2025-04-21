/*-------------------------
* Script Name: SequenceEmitter.cs
* Author: Connor
* -------------------------
* DESCRIPTION:
* SequenceEmitter is a singleton that sits on the phone GameObject. It handles registering Elements
* as the player punches them in via ElementEmitters. It also displays them on the Phone.
* The Phone is the Sequence Emitter, without it, the player cannot toggle Sequences.
* 
* NOTES:
* You don't need to set ElementEmitters or SequenceListeners. They'll handle that on their own.
* AddElement adds the ElementEmitter itself so we can track specific AudioClips to play.
* Special cases like confirm and cancel have to be hard-coded here. (But I think confirm and cancel should be all we need?)
*/
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Collections;

public class SequenceEmitter : MonoBehaviour
{
    /// <summary>
    /// Maximum number of elements allowed in the sequence.
    /// </summary>
    public const int MAX_ELEMENTS = 8;

    /// <summary>
    /// Singleton instance for the SequenceEmitter (One phone per game)
    /// </summary>
    public static SequenceEmitter instance;

    /// <summary>
    /// Current string of element_ids stored in the instance.
    /// </summary>
    public static List<ElementEmitter> currentSequence = new List<ElementEmitter>();

    /// <summary>
    /// Reference to the parent transform of the display box. Set in Editor.
    /// </summary>
    public GameObject sequenceDisplay;
    
    /// <summary>
    /// Reference to the prefab for a single element display. Set in Editor.
    /// </summary>
    public GameObject elementDisplayPrefab;

    private static bool emitting;
    public static bool lockDisplay;
    public static bool valid;

    public AudioPlayer validPlayer;
    public AudioPlayer invalidPlayer;

    #region Singleton Setup
    private void Start()
    {
        if (instance != null)
        {
            Destroy(this);
            return;
        }

        instance = this;
    }

    private void OnDestroy()
    {
        if (instance != null && instance == this)
        {
            instance = null;
        }
    }
    #endregion

    public static Action<List<Element>> onEmitSequence;

    /// <summary>
    /// Adds a given element to the current sequence. Also handles special elements like "confirm" and "cancel".
    /// </summary>
    /// <param name="newElement"> Element to add to the sequence.</param>
    public static void AddElement(ElementEmitter newElement)
    {
        if (emitting) return;
        bool handledElement = false;
        switch (newElement.currentElement.elementId) //This switch statement is for any special-case elements that aren't meant to display anything. Confirm, Cancel, etc.
        {
            case "confirm":
                handledElement = true;
                if (instance) instance.StartCoroutine("EmitSequence");
                break;
            case "cancel":
                if (lockDisplay) return;
                handledElement = true;
                if (instance) instance.CancelSequence();
                break;
        }

        if (lockDisplay) return;
        if (handledElement == false)
        {
            if (currentSequence.Count > MAX_ELEMENTS) currentSequence.Clear();
            
            currentSequence.Add(newElement);
            if (instance) instance.UpdateDisplay(newElement);            
        }
    }

    /// <summary>
    /// Instantiate a display for the most recently inputted element.
    /// </summary>
    /// <param name="newElement">New element to display.</param>
    public void UpdateDisplay(ElementEmitter newElement)
    {
        GameObject new_display = Instantiate(elementDisplayPrefab, sequenceDisplay.transform, false);
        new_display.GetComponent<ElementDisplay>().SetElement(newElement.currentElement);
    }

    /// <summary>
    /// Send the signal out to any SequenceListeners.
    /// </summary>
    public IEnumerator EmitSequence()
    {
        emitting = true;
        valid = false;
        string idSequence = "Final Sequence:";
        List<Element> elementSequence = new List<Element>();
        yield return new WaitForSeconds(0.25f);
        foreach (ElementEmitter emitter in currentSequence)
        {
            idSequence += "| " + emitter.currentElement.elementId + " |";
            emitter.PlayAudio();
            elementSequence.Add(emitter.currentElement);
            yield return new WaitForSeconds(0.15f);
        }

        Debug.Log(idSequence);

        onEmitSequence?.Invoke(elementSequence);
        currentSequence.Clear();
        ClearDisplay();
        emitting = false;

        if (valid) validPlayer.Play();
        else invalidPlayer.Play();
    }

    /// <summary>
    /// Clear the active sequence.
    /// </summary>
    public void CancelSequence()
    {
        currentSequence.Clear();
        ClearDisplay();
    }

    public void SetDisplay(Sequence sequence)
    {
        CancelSequence();
        foreach (Element element in sequence.elements)
        {
            GameObject new_display = Instantiate(elementDisplayPrefab, sequenceDisplay.transform, false);
            new_display.GetComponent<ElementDisplay>().SetElement(element);
        }
    }

    /// <summary>
    /// Remove all display elements.
    /// </summary>
    private void ClearDisplay()
    {
        foreach (ElementDisplay element in sequenceDisplay.GetComponentsInChildren<ElementDisplay>())
        {
            Destroy(element.gameObject);
        }
    }
}
