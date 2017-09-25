using UnityEngine;
using System.Collections.Generic;

public class UltiMaze
{
	private int height = 10;
	private int width = 10;

	private TreeLikeGraph maze;

	public UltiMaze()
	{
		maze = new TreeLikeGraph();
		maze.newNode(new List<MazeSquare>() { new MazeSquare (0, 1, false), new MazeSquare(0, 2, true), new MazeSquare(0, 0, true) });
		for (int i = 0; i < 10; i++)
		{
			// TODO - construct a binary array of connections for a better method...

			List<MazeSquare> totalOpenConnections = new List<MazeSquare> ();
			foreach (GraphNode node in maze.getNodes()) 
			{
				totalOpenConnections.AddRange (node.getOpenConnections ());
			}

			if (totalOpenConnections.Count > 0) 
			{
				// choose one of the connections
				int choice = Random.Range (0, totalOpenConnections.Count - 1);

				// TODO - choose which tile piece

				// assume the straight tile piece...for now
				List<MazeSquare> newNodeSpaces = new List<MazeSquare> () { new MazeSquare(totalOpenConnections [choice]) };
				newNodeSpaces.Add (new MazeSquare (totalOpenConnections [choice].x + 1, totalOpenConnections [choice].y + 1, false));
				newNodeSpaces.Add (new MazeSquare (totalOpenConnections [choice].x + 2, totalOpenConnections [choice].y + 2, true));
				foreach (MazeSquare ms in newNodeSpaces) 
				{
					Debug.Log (ms.isConnection);
				}

				// add and connect the new node
				GraphNode newNode = maze.newNode (newNodeSpaces);
				maze.connect (newNode, totalOpenConnections [choice].owner);
			}
			else
			{
				Debug.LogWarning ("No open connections!");
			}
		}
	}

	public TreeLikeGraph getRoadSystem()
	{
		return maze;
	}
}
