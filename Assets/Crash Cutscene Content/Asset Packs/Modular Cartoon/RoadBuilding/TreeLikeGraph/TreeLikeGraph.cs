using UnityEngine;
using System.Collections.Generic;

public class TreeLikeGraph
{
	List<GraphNode> nodes = new List<GraphNode>();

	public List<GraphNode> getNodes()
	{
		return nodes;
	}

	public override string ToString()
	{
		string s = "";
		foreach (GraphNode node in nodes)
		{
			s += node.ToString() + "\n";
		}
		return s;
	}

	public GraphNode newNode(List<MazeSquare> id)
	{
		GraphNode newNode = new GraphNode (id);
		nodes.Add (newNode);
		
		return newNode;
	}

	public bool deleteNode(GraphNode node)
	{
		// set all children as roots by setting their parent properties to null
		foreach (GraphNode conn in node.getConnections())
			conn.removeConnection(node);

		// will return false if it doesn't remove it (e.g., it is not in the list)
		return nodes.Remove (node);
	}

	// TODO - this depends on both nodes existing
	public void connect(GraphNode node1, GraphNode node2)
	{
		node1.addConnection (node2);
		node2.addConnection (node1);
	}
}
