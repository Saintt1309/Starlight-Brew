using UnityEngine;
using TMPro;

public class Bottle : MonoBehaviour
{
    public static Bottle currentlySelectedBottle;

    public bool isSelected = false;
    public TMP_Text selectedBottle;
    public BottleData bottleData;

    void Start()
    {
        selectedBottle = GameObject.Find("selected bottle").GetComponent<TMP_Text>();

        var renderer = GetComponent<SpriteRenderer>();
        if (renderer != null)
        {
            renderer.sprite = bottleData.texture;
        }

        Debug.Log("Loaded bottle: " + bottleData.bottleName);
    }
    

    public void OnMouseDown()
    {
        Debug.Log("Bottle clicked");
        if (!isSelected)
        {
            if (currentlySelectedBottle != null)
            {
                currentlySelectedBottle.Deselect();
            }

            Select();
            Debug.Log("Bottle selected");

            selectedBottle.text = "Selected Bottle: " + currentlySelectedBottle;
        }
        else
        {
            currentlySelectedBottle.Deselect();
            selectedBottle.text = "Selected Bottle: " + currentlySelectedBottle;
        }
    }

    public void Select()
    {
        isSelected = true;
        currentlySelectedBottle = this;

        GetComponent<Renderer>().material.color = Color.yellow;
    }

    public void Deselect()
    {
        isSelected = false;

        GetComponent<Renderer>().material.color = Color.white;
    }
}
