using System;
using UnityEngine;
using UnityEngine.U2D.Animation;

public class BombScript : MonoBehaviour
{
    DateTime StartingTime;
    int Size;
    public PlayerScript Player;
    BoxCollider2D boxCollider;
    public GameObject Explosion;
    SpriteLibrary spriteLibrary;
    [SerializeField] SpriteLibraryAsset[] spriteLibraryAsset;
    SpriteRenderer spriteRender;
    void Start()
    {
        StartingTime = DateTime.Now;
        Size = 2;
        spriteRender = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
        spriteLibrary = GetComponent<SpriteLibrary>();
        changeSkin(1);
    }

    // Update is called once per frame
    void Update()
    {
        TimeSpan ts = DateTime.Now - StartingTime;
        if (ts.Seconds >= 3)
        {
            Explode(Size);
            //if Player isn't dead, call this function to enable him to place a bomb again
            if (Player != null)
            {
                Player.BombExploded();
            }
            GameManager.RemoveBomb(gameObject);
            Destroy(gameObject);

        }
        if (Player == null)
        {
            boxCollider.isTrigger = false;
        }
        else if (Math.Floor(Player.transform.position.x) != Math.Floor(transform.position.x) ||  Math.Floor(Player.transform.position.y) != Math.Floor(transform.position.y))
        {
            boxCollider.isTrigger = false;
        }

    }

    public static GameObject Deploy(GameObject bomb, Vector3 pos)
    {
        return Instantiate(bomb, pos, Quaternion.identity);
    }

    void changeSkin(int skin)
    {
        spriteLibrary.spriteLibraryAsset = spriteLibraryAsset[skin];
    }


    public void Explode(int explosionSize, bool left = true, bool right = true, bool up = true, bool down = true, int i = 0)
    {
        if(i <= explosionSize)
        {
            //firstly check the bomb pos for a player, then recursively check tiles aroud the bomb in a + shape
            //if indestructible tile is found, set the current dir to false (otherwise other destructibe tile behind an indestructible tile would be destroyed)
            if(i == 0)
            {
                GameManager.CheckForPlayers((int)Math.Floor(transform.position.x), (int)Math.Floor(transform.position.y));
                Instantiate(Explosion, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
                i++;
            }
            if(left == true && Tiles.Tilemap.GetTile(new Vector3Int((int)(transform.position.x - 0.5f) - i, (int)(transform.position.y - 0.5f), (int)Layers.Indestructible)) == null)
            {
                Tiles.Destruct((int)(transform.position.x - 0.5f) - i, (int)(transform.position.y - 0.5f));
                Instantiate(Explosion,new Vector3(transform.position.x - i, transform.position.y, transform.position.z), Quaternion.identity);
                GameManager.CheckForPlayers((int)(transform.position.x - 0.5f) - i, (int)(transform.position.y - 0.5f));
            }
            else left = false;

            if (right == true && Tiles.Tilemap.GetTile(new Vector3Int((int)(transform.position.x - 0.5f) + i, (int)(transform.position.y - 0.5f), (int)Layers.Indestructible)) == null)
            {
                Tiles.Destruct((int)(transform.position.x - 0.5f) + i, (int)(transform.position.y - 0.5f));
                Instantiate(Explosion, new Vector3(transform.position.x + i, transform.position.y, transform.position.z), Quaternion.identity);
                GameManager.CheckForPlayers((int)(transform.position.x - 0.5f) + i, (int)(transform.position.y - 0.5f));
            }

            else right = false;

            if (up == true && Tiles.Tilemap.GetTile(new Vector3Int((int)(transform.position.x - 0.5f), (int)(transform.position.y - 0.5f) + i, (int)Layers.Indestructible)) == null)
            {
                Tiles.Destruct((int)(transform.position.x - 0.5f), (int)(transform.position.y - 0.5f) + i);
                Instantiate(Explosion, new Vector3(transform.position.x, transform.position.y + i, transform.position.z), Quaternion.identity);
                GameManager.CheckForPlayers((int)(transform.position.x - 0.5f), (int)(transform.position.y - 0.5f) + i);
            }

            else up = false;

            if (down == true && Tiles.Tilemap.GetTile(new Vector3Int((int)(transform.position.x - 0.5f), (int)(transform.position.y - 0.5f) - i, (int)Layers.Indestructible)) == null)
            {
                Tiles.Destruct((int)(transform.position.x - 0.5f), (int)(transform.position.y - 0.5f) - i);
                Instantiate(Explosion, new Vector3(transform.position.x, transform.position.y - i, transform.position.z), Quaternion.identity);
                GameManager.CheckForPlayers((int)(transform.position.x - 0.5f), (int)(transform.position.y - 0.5f) - i);
            }

            else down = false;
            Explode(explosionSize, left, right, up, down, i + 1);
        }
    }
}
