using UnityEngine;
public class GroundScroller : MonoBehaviour
{
    public float scrollSpeed = 1f;
    private float tileWidth; //Stores The Width of the Background Image to switch Back after some interval
                             // Start is called before the first frame update               
    void Start()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
             tileWidth = spriteRenderer.bounds.size.x; 
        }
        else
        {
            Debug.LogError("The SpriteRenderer Component is Missing Form the Background Object");
            enabled = false; //Disable the Script at Start
        }

    }

    void Update()
    {
        transform.Translate(Vector3.left * scrollSpeed * Time.deltaTime);
        if (transform.position.x <= -tileWidth)
        {
            RepositionTile();
        }

    }

    void RepositionTile()
    {
        Vector3 Offset = new Vector3(tileWidth * 2f, 0, 0);
        transform.position += Offset;

    }
}