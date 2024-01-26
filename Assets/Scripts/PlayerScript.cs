using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class PlayerScript : MonoBehaviour
{
    Rigidbody2D RB;
    public PlayerInput _playerInput;
    Vector2 dir;
    public static bool BombPlaced;
    public GameObject Bomb;
    public Tilemap _destructible;

    void Start()
    {
        _playerInput = GetComponent<PlayerInput>();
        RB = GetComponent<Rigidbody2D>();
        _destructible = GetComponent<Tilemap>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        dir = _playerInput.actions["Move"].ReadValue<Vector2>();
        RB.velocity = new Vector2(dir.x * 10, dir.y * 10);

        if (_playerInput.actions["Bomb"].ReadValue<float>() == 1f && !BombPlaced)
        {
            DropBomb(RB.position.x, RB.position.y);
        }
    }

    void DropBomb(float x, float y)
    {
        Vector2 pos = new Vector2(Mathf.Round(x), Mathf.Round(y));
        Instantiate(Bomb, RB.position, Quaternion.identity);
        BombPlaced = true;
    }
}
