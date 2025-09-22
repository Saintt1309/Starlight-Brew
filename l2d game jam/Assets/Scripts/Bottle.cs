using UnityEngine;
using TMPro;

public class Bottle : MonoBehaviour
{
    public static Bottle currentlySelectedBottle;

    public bool isSelected = false;
    public TMP_Text selectedBottle;

    void Start()
    {
        selectedBottle = GameObject.Find("selected bottle").GetComponent<TMP_Text>();
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

            selectedBottle.text = "Selected Bottle: " + gameObject.name;
        }
        else
        {
            MoveUp();
        }
    }

    public void Select()
    {
        isSelected = true;
        currentlySelectedBottle = this;

        // OPTIONAL: add a visual cue for selection
        GetComponent<Renderer>().material.color = Color.yellow;
    }

    public void Deselect()
    {
        isSelected = false;

        // OPTIONAL: reset visual cue
        GetComponent<Renderer>().material.color = Color.white;
    }

    void MoveUp()
    {
        transform.position = new Vector3(
            transform.position.x,
            transform.position.y + 20f,
            transform.position.z
        );
    }
}
