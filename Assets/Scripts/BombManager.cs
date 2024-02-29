using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

static class BombManager
{
   private static ArrayList Bombs = new ArrayList(); 

    public static void DeployBomb(GameObject bomb) => Bombs.Add(bomb);
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
