using System.Collections.Generic;
using UnityEngine;

public class MazeDataGenerator
{
    //Chance of empty space
    public float placementThreshold;    

    public MazeDataGenerator()
    {
        placementThreshold = .1f;
    }

    public int[,] FromDimensions(int sizeRows, int sizeCols)
    {
        int[,] maze = new int[sizeRows, sizeCols];
        
            int rMax = maze.GetUpperBound(0);
    int cMax = maze.GetUpperBound(1);

    for (int i = 0; i <= rMax; i++)
    {
        for (int j = 0; j <= cMax; j++)
        {
                //For every grid cell, the code first checks if the current cell is on the outside of the grid
                //(that is, if either index is on the array boundaries). If so, assign 1 for wall
                if (i == 0 || j == 0 || i == rMax || j == cMax)
            {
                maze[i, j] = 1;
            }

                //Checks if the coordinates are evenly divisible by 2 in order to operate on every other cell. And placementThreshold to skip a cell
                else if (i % 2 == 0 && j % 2 == 0)
            {
                if (Random.value > placementThreshold)
                {
                        //Assigns 1,0 or -1 (adjancent cell) to both the current cell and a randomly chosen adjacent cell
                        maze[i, j] = 1;

                    int a = Random.value < .5 ? 0 : (Random.value < .5 ? -1 : 1);
                    int b = a != 0 ? 0 : (Random.value < .5 ? -1 : 1);
                    maze[i+a, j+b] = 1;
                }
            }
        }
    }
        return maze;
    }
}