using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hover : SaiMonoBehaviour
{
    private static Hover instance;
    public static Hover Instance => instance;

    private SpriteRenderer spriteRenderer;
    [SerializeField] private Camera mainCamera;
    protected override void Awake()
    {
        base.Awake();
        if (Hover.instance != null) Debug.LogError("Only 1 Hover Warning");
        Hover.instance = this;
    }

    protected override void Start()
    {
        base.Start();
        this.spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected override void Update()
    {
        base.Update();
        FollowMouse();
    }

    private void FollowMouse()
    {
        if (spriteRenderer.enabled)
        {
            transform.position = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        }
    }

    public void Activate(Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
        spriteRenderer.enabled = true;
    }

    public void Deactivate()
    {
        spriteRenderer.sprite = null; // Set sprite to null when deactivating
        spriteRenderer.enabled = false;
    }
}
