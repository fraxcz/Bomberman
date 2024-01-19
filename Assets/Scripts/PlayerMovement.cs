using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] LayerMask wall;
    public PlayerInput playerInput;
    Vector2 dir;
    BoxCollider2D collide;
    public Sprite BombSprite;
    public GameObject Bomb;

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody2D>();
        collide = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        dir = playerInput.actions["Move"].ReadValue<Vector2>();
        Debug.Log(dir);
        rb.velocity = new Vector2(dir.x * 10, dir.y * 10);
    }

    void DropBomb(float x, float y)
    {
        GameObject.CreatePrimitive(PrimitiveType.Sphere);
        Bomb.GetComponent<SpriteRenderer>().sprite = BombSprite;
    }
}
