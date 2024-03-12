using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Tilemaps;


public enum Layers
{
    Indestructible = 0,
    Destructible = 1,
    Bomb = 2,
    BombArea = 3,

}

public class Tiles : MonoBehaviour
{
    // Start is called before the first frame update
    public static Tilemap Tilemap;
    private void Start()
    {
        Tilemap = GetComponent<Tilemap>();
    }

    private void Update()
    {
        
    }

    public static void Destruct(int x, int y)
    {
        Tilemap.SetTile(new Vector3Int(x, y, (int)Layers.Destructible), null);
    }
} 
