using UnityEngine;

public class DragNDrop : MonoBehaviour
{
    Vector3 offset;
    public Collider2D collider2d;
    public Shaker shaker;
    

    void Start()
    {
        shaker = GetComponent<Shaker>();
    }
    
    private void OnMouseDown()
    {
        offset = transform.position - MouseWorldPosition();
    }

    private void OnMouseDrag()
    {
        transform.position = MouseWorldPosition() + offset;
    }

    private void OnMouseUp()
    {
        collider2d.enabled = false;
        Vector2 mouseWorldPos = MouseWorldPosition();
        Collider2D hitCollider = Physics2D.OverlapPoint(mouseWorldPos);

        Debug.Log("Element Dropped");

        if (hitCollider != null)
        {
            if (hitCollider.CompareTag("DropArea"))
            {
                shaker.checkAction(hitCollider.name);
                Debug.Log("Dropped onto: " + hitCollider.name);
            }
            else
            {
                Debug.Log("Dropped onto wrong object: " + hitCollider.name);
            }
        }
        collider2d.enabled = true;
        transform.position = new Vector3(-2f, -3.5f, 0f);
    }

    Vector3 MouseWorldPosition()
    {
        var mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = Camera.main.WorldToScreenPoint(transform.position).z;
        return Camera.main.ScreenToWorldPoint(mouseScreenPos);
    }
}
