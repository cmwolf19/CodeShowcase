/*-------------------------
* Script Name: ElementDisplay.cs
* Author: Connor
* -------------------------
* DESCRIPTION:
* ElementDisplay handles the actual visible parts of the element. It is configurable
* so that an Element can display a graphic, text, or graphic with a text label (set in Element.cs).
* 
* NOTES:
* ElementDisplay relies on the Element being set up properly. If it looks broken, check there first.
*/
using UnityEngine;

public class ElementDisplay : MonoBehaviour
{
    private GameObject textOnly;
    private GameObject imageOnly;
    private GameObject pairText;
    private GameObject pairImage;

    /// <summary>
    /// Sets the display to show the given element.
    /// </summary>
    /// <param name="element"></param>
    public void SetElement(Element element)
    {
        if (textOnly == null)
        {
            textOnly = transform.Find("Text Only").gameObject; //this is probably not the best way to do this, but it works.
            imageOnly = transform.Find("Image Only").gameObject;
            pairText = transform.Find("Pair Text").gameObject;
            pairImage = transform.Find("Pair Image").gameObject;
        }

        if (element == null)
        {
            textOnly.SetActive(false);
            imageOnly.SetActive(false);
            pairText.SetActive(false);
            pairImage.SetActive(false);
            return;
        }

        switch (element.displayType)
        {
            case Element.eDisplayTypes.TEXT:
                textOnly.SetActive(true);
                imageOnly.SetActive(false);
                pairText.SetActive(false);
                pairImage.SetActive(false);
                textOnly.GetComponent<TMPro.TextMeshProUGUI>().text = element.displayText;
                break;
            case Element.eDisplayTypes.GRAPHIC:
                textOnly.SetActive(false);
                imageOnly.SetActive(true);
                pairText.SetActive(false);
                pairImage.SetActive(false);
                imageOnly.GetComponent<UnityEngine.UI.Image>().sprite = element.displayTexture;
                break;
            case Element.eDisplayTypes.BOTH:
                textOnly.SetActive(false);
                imageOnly.SetActive(false);
                pairText.SetActive(true);
                pairImage.SetActive(true);
                pairImage.GetComponent<UnityEngine.UI.Image>().sprite = element.displayTexture;
                pairText.GetComponent<TMPro.TextMeshProUGUI>().text = element.displayText;
                break;
        }
    }
}
