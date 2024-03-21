using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

class GameManager: MonoBehaviour
{
    private static ArrayList Bombs = new ArrayList();
    public static int NumberOfPlayers { get; set; } = 0;
    private Vector3[] SpawnPositions;
    [SerializeField] private GameObject Player;

    private void Start()
    {
        SpawnPositions = new Vector3[] {new Vector3(-6.5f, -3.4f, 0f), new Vector3(6.5f, -3.4f, 0f), new Vector3(6.5f, 3.6f, 0f), new Vector3(-6.5f, 3.6f, 0f)};
        for (int i = 0; i < NumberOfPlayers; i++)
        {
            Player.GetComponent<PlayerScript>().PlayerID = i + 1;
            Instantiate(Player, SpawnPositions[i], Quaternion.identity);
        }

    }
    public static bool DeployBomb(GameObject bomb, Vector3 pos, PlayerScript player)
    {
        if (!CheckForBomb(pos))
        {
            var newBomb = BombScript.Deploy(bomb, pos);
            newBomb.GetComponent<BombScript>().Player = player;
            Bombs.Add(newBomb);
            return true;
        }
        return false;
    }
    public static void RemoveBomb(GameObject bomb) => Bombs.Remove(bomb);


    public static bool CheckForBomb(Vector3 pos)
    {
        foreach (GameObject Bomb in Bombs)
        {
            if (Bomb.transform.position == pos) return true;
            Debug.Log(Bomb);
        }
        return false;
    }
}
