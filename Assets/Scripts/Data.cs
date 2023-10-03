using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Data
{
    public class GameData
    {
        //PLAYER DATA
        public float playerHealth = 120;
        public float playerMaxHealth = 120;
        public Vector2 playerPosition = Vector2.zero;
        public int playerAmmo = 300;

        //ENEMIES DATA
        public int enemiesAmount = 0;
        public Vector2[] enemiesPosition;
        public float[] enemiesHealth;

        //INVENTORY DATA
        public int slotsCount = 0;
        public Item[] items;
        public int[] amount;
    }
}
