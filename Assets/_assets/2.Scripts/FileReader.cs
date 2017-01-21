using UnityEngine;
using System.Collections;
using System.IO;
using System;

public class FileReader : MonoBehaviour
{


    public static int[,] ReadWaveShape(string path, int x, int y)
    {
        int[,] waveMap = new int[x,y];
        try
        {   // Open the text file using a stream reader.
            using (StreamReader sr = new StreamReader(path))
            {
                int i = 0;
                // Read the stream to a string, and write the string to the console.
                while (sr.Peek() >= 0)
                {
                    string line = sr.ReadLine();
                    for (int j = 0; j < x; j++)
                    {
                        Debug.Log(line[i]);
                        waveMap[i,j] = line[i];
                    }
                    i++;
                }

                
                
            }

            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    Debug.Log(waveMap[i,j]);
                }
            }
        }
        catch (Exception e)
        {
            Debug.Log("The file could not be read:");
            Debug.Log(e.Message);
        }

        return waveMap;
    }

}
