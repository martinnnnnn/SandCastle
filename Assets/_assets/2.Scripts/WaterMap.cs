using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaterMap {
    public List<WaterColumn> columns;

    public float cooldown;//damage
    public float vanishingTime;//after translating, idle time before destroy
    public int wantedYStep;//level step
    public float waitTime;//before showing

    public float speed;

    public int indexAudioSource;

    public WaterMap(List<List<int>> map)
    {
        cooldown = 2;
        vanishingTime = 5;
        speed = 5f;
        wantedYStep = 4;

        this.columns = new List<WaterColumn>();

        for (int j = 0; j < map.Count; j++)
        {
            columns.Add(new WaterColumn(j, map[j]));
        }
    }

    public WaterMap(int[][] map)
    {
        cooldown = 2;
        vanishingTime = 5;
        speed = 5f;
        wantedYStep = 4;

        columns = new List<WaterColumn>();

        for (int i = 0; i < map.Length; i++)
        {
            columns.Add(new WaterColumn(i, map[i]));
            
        }
    }
}
