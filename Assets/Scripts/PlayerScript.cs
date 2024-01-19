using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;

public class PlayerScript : MonoBehaviour
{
    Rigidbody2D RB;
    public PlayerInput _playerInput;
    Vector2 dir;
    public GameObject Bomb;

    void Start()
    {
        _playerInput = GetComponent<PlayerInput>();
        RB = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        dir = _playerInput.actions["Move"].ReadValue<Vector2>();
        RB.velocity = new Vector2(dir.x * 10, dir.y * 10);

        if (_playerInput.actions["Bomb"].ReadValue<float>() == 1f)
        {
            DropBomb(RB.position.x, RB.position.y);
        }
    }

    void DropBomb(float x, float y)
    {
        Vector2 pos = new Vector2(Mathf.Round(x), Mathf.Round(y));
        Debug.Log(pos);
        Instantiate(Bomb, RB.position, Quaternion.identity);
    }
}
