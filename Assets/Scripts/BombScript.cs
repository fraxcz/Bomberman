using System;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class BombScript : MonoBehaviour
{
    DateTime StartingTime;
    int Size;
    public PlayerScript Player;
    BoxCollider2D boxCollider;
    bool PlayerOnBombInit;
    void Start()
    {
        StartingTime = DateTime.Now;
        Size = 2;
        boxCollider = GetComponent<BoxCollider2D>();
        PlayerOnBombInit = true;
    }

    // Update is called once per frame
    void Update()
    {
        TimeSpan ts = DateTime.Now - StartingTime;
        if (ts.Seconds >= 3)
        {
            Explode(Size);
            Player.BombExploded();
            GameManager.RemoveBomb(gameObject);
            Destroy(gameObject);

        }
        if (PlayerOnBombInit && Math.Floor(Player.transform.position.x) != Math.Floor(transform.position.x) || PlayerOnBombInit && Math.Floor(Player.transform.position.y) != Math.Floor(transform.position.y))
        {
            boxCollider.isTrigger = false;
            PlayerOnBombInit = false;
        }

    }

    public static GameObject Deploy(GameObject bomb, Vector3 pos)
    {
        return Instantiate(bomb, pos, Quaternion.identity);
    }

    public void Explode(int size, bool left = true, bool right = true, bool up = true, bool down = true, int i = 0)
    {
        if(i < size)
        {
            if(left == true && Tiles.Tilemap.GetTile(new Vector3Int((int)(transform.position.x - 1.5f) - i, (int)(transform.position.y - 0.5f), (int)Layers.Indestructible)) == null)
            {
                Tiles.Destruct((int)(transform.position.x - 1.5f) - i, (int)(transform.position.y - 0.5f));
            }
            else left = false;

            if (right == true && Tiles.Tilemap.GetTile(new Vector3Int((int)(transform.position.x + 0.5f) + i, (int)(transform.position.y - 0.5f), (int)Layers.Indestructible)) == null)
            {
                Tiles.Destruct((int)(transform.position.x + 0.5f) + i, (int)(transform.position.y - 0.5f));
            }

            else right = false;

            if (up == true && Tiles.Tilemap.GetTile(new Vector3Int((int)(transform.position.x - 0.5f), (int)(transform.position.y + 0.5f) + i, (int)Layers.Indestructible)) == null)
            {
                Tiles.Destruct((int)(transform.position.x - 0.5f), (int)(transform.position.y + 0.5f) + i);
            }

            else up = false;

            if (down == true && Tiles.Tilemap.GetTile(new Vector3Int((int)(transform.position.x - 0.5f), (int)(transform.position.y - 1.5f) - i, (int)Layers.Indestructible)) == null)
            {
                Tiles.Destruct((int)(transform.position.x - 0.5f), (int)(transform.position.y - 1.5f) - i);
            }

            else down = false;
            Explode(size, left, right, up, down, i + 1);
        }
    }

    void checkPlayers(int x, int y)
    {

    }
    public string toString()
    {
        return "a bumb";
    }
}
