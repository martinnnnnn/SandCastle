using UnityEngine;
using System.Collections;
using System.IO;
using System;

public class FileReader : MonoBehaviour
{


    public static int[][] ReadWaveShape(string path, int x, int y)
    {
        int[][] waveMap = new int[x][];
        for (int i = 0; i < x; i++)
        {
            waveMap[i] = new int[y];
        }
        try
        {   // Open the text file using a stream reader.
            using (StreamReader sr = new StreamReader(path))
            {
                int i = 0;
                // Read the stream to a string, and write the string to the console.
                while (sr.Peek() >= 0)
                {
                    string line = sr.ReadLine();

                    for (int j = 0; j < line.Length; j++)
                    {
                        //Debug.Log(line[i]);
                        if (line[j] == '1')
                        {
                            waveMap[j][i] = 1;
                        } 
                        else
                        {
                            waveMap[j][i] = 0;
                        }
                    }
                    i++;
                }
            }
        }
        catch (Exception e)
        {
            Debug.Log("The file could not be read:");
            Debug.Log(e.Message);
        }

        //for (int i = 0; i < waveMap.Length; i++)
        //{
        //    for (int j = 0; j < waveMap[i].Length; j++)
        //    {
        //        Debug.Log(waveMap[i][j]);
        //    }
        //}
        return waveMap;
    }

}
