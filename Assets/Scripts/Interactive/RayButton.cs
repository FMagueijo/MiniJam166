using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RayButton : MonoBehaviour
{
    public bool bClickable = false;
    public bool bHovering = false;
    public UnityEvent OnClickEvent;
    [Range(0,1)] public float normalAlpha;
    [Range(0, 1)] public float onHoverAlpha;
    private SpriteRenderer spriteRenderer;


    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        
    }

    private void Update() {
        if (!bClickable) return;
        
        
        Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);

        if (hit.collider != null)
        {
            bHovering = true;
            if (Input.GetMouseButtonDown(0))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    OnClickEvent.Invoke();
                }

            }
        }
        else{
            bHovering = false;
        }
    }

    private void OnMouseEnter()
    {

        Color _temp = spriteRenderer.color;
        _temp.a = onHoverAlpha;

        spriteRenderer.color = _temp;
    }

    private void OnMouseExit()
    {
        Color _temp = spriteRenderer.color;
        _temp.a = normalAlpha;

        spriteRenderer.color = _temp;

    }
}
