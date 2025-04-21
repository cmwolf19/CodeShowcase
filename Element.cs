/*-------------------------
* Script Name: Element.cs
* Author: Connor
* -------------------------
* DESCRIPTION:
* Element is a script that represents a symbol that can be inputted into a Sequence.
* This is just the ScriptableObject, and holds no functionality (see ElementEmitter.cs).
* 
* NOTES:
* 
*/
using UnityEngine;

[CreateAssetMenu(fileName = "Element", menuName = "Sequences/New Element", order = 2)]
public class Element : ScriptableObject
{
    /// <summary>
    /// Behind-the-scenes ID for this button. Never shown to players. Used to track what Element is what.
    /// </summary>
    public string elementId;

    /// <summary>
    /// Explains what kind of display this element should have.
    /// </summary>
    public enum eDisplayTypes {TEXT, GRAPHIC, BOTH}
    public eDisplayTypes displayType;

    /// <summary>
    /// Text to display if display_type is TEXT or BOTH.
    /// </summary>
    public string displayText;

    /// <summary>
    /// Text to display if display_texture is GRAPHIC or BOTH.
    /// </summary>
    public Sprite displayTexture;

    /// <summary>
    /// Audio to play when this element is emitted.
    /// </summary>
    public GameObject audioEvent;
}
