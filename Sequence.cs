/*-------------------------
* Script Name: Sequence.cs
* Author: Connor
* -------------------------
* DESCRIPTION:
* Sequences are ScriptableObjects stored in the project. They're simple data systems
* that just hold a list of Elements. They can also be checked via Match to see if a Sequence matches.
* 
* NOTES:
* This is JUST the ScriptableObject, not the functionality. See SequenceListener.cs.
*/
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Sequence", menuName = "Sequences/New Sequence", order = 1)]
public class Sequence : ScriptableObject
{
    /// <summary>
    /// List of elements needed in this sequence.
    /// </summary>
    public List<Element> elements;

    /// <summary>
    /// Check if a supplied sequence matches this sequence.
    /// </summary>
    /// <param name="inputElementIDs">List of element_ids to compare to this sequence.</param>
    /// <returns></returns>
    public bool Match(List<Element> inputElementIDs)
    {
        if (elements.Count != inputElementIDs.Count) return false;
        
        for (int i = 0; i != elements.Count; i++)
        {
            if (elements[i].elementId.ToLower().Equals(inputElementIDs[i].elementId) == false) return false;
        }
        return true;
    }
}
