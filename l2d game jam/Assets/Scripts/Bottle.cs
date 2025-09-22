using UnityEngine;

public class Bottle : MonoBehaviour
{
    private static Bottle currentlySelectedBottle;

    private bool isSelected = false;

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
        }
        else
        {
            MoveUp();
        }
    }

    void Select()
    {
        isSelected = true;
        currentlySelectedBottle = this;

        // OPTIONAL: add a visual cue for selection
        GetComponent<Renderer>().material.color = Color.yellow;
    }

    void Deselect()
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
