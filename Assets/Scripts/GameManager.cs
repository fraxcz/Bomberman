using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

static class GameManager
{
    private static ArrayList Bombs = new ArrayList();


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
    public static void SpawnPlayers(int count)
    {

    }
}
