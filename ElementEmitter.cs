/*-------------------------
* Script Name: ElementEmitter.cs
* Author: Connor
* -------------------------
* DESCRIPTION:
* ElementEmitter is a script that sits on Phone Button prefabs generally. They store an
* Element, and then send that Element to a SequenceEmitter when told.
* 
* NOTES:
* ElementEmitter doesn't have to just be on Phone Buttons, you can emit an Element from anything that can call Emit().
*/
using UnityEngine;
using UnityEngine.UI;

public class ElementEmitter : MonoBehaviour
{
    /// <summary>
    /// Current display of the Element.
    /// </summary>
    private ElementDisplay elementDisplay;
    
    /// <summary>
    /// Currently stored Element.
    /// </summary>
    public Element currentElement;

    /// <summary>
    /// Button used to trigger Emit().
    /// </summary>
    private Button button;   

    /// <summary>
    /// Audio Player for playing the touch-tone.
    /// </summary>
    private AudioPlayer audioPlayer;

    private void Start()
    {
        button = GetComponent<Button>();
        elementDisplay = GetComponentInChildren<ElementDisplay>();
        audioPlayer = GetComponent<AudioPlayer>();

        if (currentElement != null)
        {
            SetElement(currentElement);
        }
    }

    public void SetInteractable(bool interactable)
    {
        button.interactable = interactable;
    }

    /// <summary>
    /// Sets the Emitter to hold the given element and refreshes the display.
    /// </summary>
    /// <param name="element">New element to hold.</param>
    public void SetElement(Element element)
    {
        currentElement = element;
        elementDisplay.SetElement(currentElement);
        audioPlayer.audioEvent = element.audioEvent;
        if (element.audioEvent == null) audioPlayer.disable = true;
    }

    public void Emit()
    {
        SequenceEmitter.AddElement(this);
        PlayAudio();
        Debug.Log("Emitting " + currentElement.elementId);
    }

    public void PlayAudio()
    {
        audioPlayer.Play();
    }
}
