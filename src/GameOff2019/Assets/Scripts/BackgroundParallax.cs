using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundParallax : MonoBehaviour
{
    public GameObject ship;

    private float parallaxMultiplier = 0.00003f;

    private Renderer quadRenderer;
    private Rigidbody2D shipRb2d;

    // Start is called before the first frame update
    void Start()
    {
        quadRenderer = GetComponent<Renderer>();
        shipRb2d = ship.GetComponent<Rigidbody2D>();

        quadRenderer.sortingLayerName = "Default";
        quadRenderer.sortingOrder = -1;
    }

    // Update is called once per frame
    void Update()
    {
        quadRenderer.material.mainTextureOffset += new Vector2(shipRb2d.velocity.x * parallaxMultiplier, shipRb2d.velocity.y * parallaxMultiplier);
    }
}
