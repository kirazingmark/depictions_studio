using System.Collections.Generic;
using UnityEngine;

public class GraphNode 
{
	private List<MazeSquare> occupiedSpaces;
	private List<GraphNode> connections;

	public GraphNode (List<MazeSquare> occupiedSpaces)
	{
		connections = new List<GraphNode> ();
		this.occupiedSpaces = occupiedSpaces;
		for (int i = 0; i < this.occupiedSpaces.Count; i++) 
		{
			this.occupiedSpaces [i].owner = this;
		}
	}

	/*
	 * An open connection is the MazeSquare that must be occupied by a piece attaching to this piece!
	 */
	public List<MazeSquare> getOpenConnections()
	{
		List<MazeSquare> openConns = new List<MazeSquare> ();
		foreach (MazeSquare ms in occupiedSpaces)
		{
			if (ms.isConnection  && !ms.connected) 
			{
				openConns.Add(ms);
			}
		}
		return openConns;
	}

	public List<GraphNode> getConnections()
	{
		return connections;
	}

	public List<MazeSquare> getOccupiedSpaces()
	{
		return occupiedSpaces;
	}

	public void addConnection(GraphNode conn)
	{
		// add the connection to the list of connections
		connections.Add (conn);

		// set the relevant occupied spaces to have a connection label
		foreach (MazeSquare connSquare in conn.getOccupiedSpaces()) 
		{
			for (int i = 0; i < occupiedSpaces.Count; i++) 
			{
				if (Mathf.Sqrt (Mathf.Pow(occupiedSpaces [i].x - connSquare.x, 2) + Mathf.Pow(occupiedSpaces [i].y - connSquare.y, 2)) <= 1) 
				{
					if (occupiedSpaces [i].isConnection)
						occupiedSpaces [i].connected = true;
					else
						Debug.LogError ("Cannot set connected if not isConnection! " + this.GetHashCode());
				}
			}
		}
	}

	public void removeConnection(GraphNode conn)
	{
		connections.Remove (conn);
		Debug.LogWarning ("Not fully implemented - GraphNode.removeConnection()");
	}

	public bool isConnectedTo(GraphNode potentialConn)
	{
		return connections.Contains(potentialConn);
	}
}
