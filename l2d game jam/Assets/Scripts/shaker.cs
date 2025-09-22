using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class Shaker : MonoBehaviour
{
    private List<string> bottles = new List<string>();
    public TMP_Text shakerContentsText;

    void Start()
    {
        // Find the TMP text once at start
        shakerContentsText = GameObject.Find("shaker contents").GetComponent<TMP_Text>();
        contentsDisplay();
    }

    public void OnMouseDown()
    {
        Debug.Log("Shaker clicked");

        // ✅ Use the static reference from Bottle class
        Bottle selectedBottle = Bottle.currentlySelectedBottle;

        if (selectedBottle != null)
        {
            bottles.Add(selectedBottle.gameObject.name);
            Debug.Log("Added " + selectedBottle.gameObject.name + " to shaker");

            // Optionally deselect after adding
            selectedBottle.Deselect();
            Bottle.currentlySelectedBottle = null;

            contentsDisplay();
        }
        else
        {
            Debug.Log("No bottle selected");
        }
    }

    public void contentsDisplay()
    {
        if (bottles.Count > 0)
        {
            shakerContentsText.text = "Shaker Contents: " + string.Join(", ", bottles);
        }
        else
        {
            shakerContentsText.text = "Shaker Contents: Empty";
        }
    }
}
