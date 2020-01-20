using System.Collections;
using UnityEngine;

public class LampFall : MonoBehaviour
{
    public float speed = 3f;
    private Rigidbody2D rb;
    private Vector2 screenBounds;
    public SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0, -speed);
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -screenBounds.y)
        {
            Destroy(this.gameObject);
        }
    }

    public void Damage()
    {
        StartCoroutine(ColorChange());
    }

    IEnumerator ColorChange()
    {
        spriteRenderer.color = Color.red;

        yield return new WaitForSeconds(0.2f);

        spriteRenderer.color = Color.white;
    }
}
