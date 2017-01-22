using UnityEngine;
using System.Collections;
using System.IO;
using System;

public class FileReader : MonoBehaviour
{


    public static int[][] ReadWaveShape(TextAsset asset, int x, int y)
    {
        int[][] waveMap = new int[x][];

        for (int i = 0; i < x; i++)
        {
            waveMap[i] = new int[y];
        }

        string[] lineSeparators = new string[] { "\n" };
        string[] lines = asset.text.Split(lineSeparators, System.StringSplitOptions.RemoveEmptyEntries);


        for (int i = 0; i < lines.Length; i++)
        {
            for (int j = 0; j < lines[i].Length-1; j++)
            {
                
                if (lines[i][j] == '1')
                {
                    waveMap[j][i] = 1;
                }
                else
                {
                    waveMap[j][i] = 0;
                }
            }
        }
        
        return waveMap;
    }

}
