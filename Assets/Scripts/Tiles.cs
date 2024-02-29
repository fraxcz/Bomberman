using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Tilemaps;


public enum Layers
{
    Indestructible = 0,
    Destructible = 1,
    Bomb = 2,

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

    public static void Destruct(Vector2Int position)
    {
        Tilemap.SetTile(new Vector3Int(position.x, position.y, (int)Layers.Destructible), null);
    }
} 
