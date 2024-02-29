using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class PlayerScript : MonoBehaviour
{
    internal Rigidbody2D RB;
    internal PlayerInput _playerInput;
    Vector2 dir;
    public GameObject Bomb; 
    internal int CountBombPlaced;
    int MaxBombPlaced;
    internal Tilemap _destructible;
    bool SpaceHold;

    void Start()
    {
        _playerInput = GetComponent<PlayerInput>();
        RB = GetComponent<Rigidbody2D>();
        _destructible = GetComponent<Tilemap>();;
        MaxBombPlaced = 5;
        CountBombPlaced = 0;
        SpaceHold = false;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        dir = _playerInput.actions["Move"].ReadValue<Vector2>();
        RB.velocity = new Vector2(dir.x * 5, dir.y * 5);

        if (_playerInput.actions["Bomb"].ReadValue<float>() == 1f && CountBombPlaced < MaxBombPlaced && !SpaceHold)
        {
            DropBomb(RB.position.x, RB.position.y);
            CountBombPlaced++;
            SpaceHold = true;
        }
        else if(_playerInput.actions["Bomb"].ReadValue<float>() == 0f && SpaceHold)
        {
            SpaceHold = false;
        }
    }

    void DropBomb(float x, float y)
    {
        Vector3 pos = new Vector3(Mathf.Floor(x) + 0.5f, Mathf.Floor(y) + 0.5f, (int)Layers.Bomb);
        if (!BombManager.CheckForBomb(pos))
        {
            var newBomb = BombScript.Deploy(Bomb, pos);
            Debug.Log(newBomb);
            newBomb.GetComponent<BombScript>().Player = this;
            BombManager.DeployBomb(newBomb);
        }
    }
}