using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveWarning : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    float blink = 0.2f;
    float destroy = 1.25f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(removeWarning());
    }

    IEnumerator removeWarning()
    {
        while (true)
        {
            spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(blink);
            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(blink);
            spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(blink);
            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(destroy);
            Destroy(this.gameObject);
            blink -= 0.1f;
            destroy -= 0.1f;
        }
    }
}
