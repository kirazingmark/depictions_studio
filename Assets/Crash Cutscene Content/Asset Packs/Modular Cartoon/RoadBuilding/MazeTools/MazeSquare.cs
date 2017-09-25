using UnityEngine;
using System.Collections;

public class MazeSquare
{
	public int x = 0;                  // the x-coordinate of the grid space
	public int y = 0;                  // the y-coordinate of the grid space
	public bool isConnection = false;  // whether or not this coordinate can connect to another piece
	public bool connected = false;     // whether or not this coordinate is connected to another piece
	public GraphNode owner;

	public MazeSquare()
	{
		x = 0;
		y = 0;
		isConnection = false;
		connected = false;
	}

	public MazeSquare(MazeSquare ms)
	{
		x = ms.x;
		y = ms.y;
		isConnection = ms.isConnection;
		connected = ms.connected;
	}

	public MazeSquare(int x, int y, bool isConnection)
	{
		this.x = x;
		this.y = y;
		this.isConnection = isConnection;
		this.connected = false;
	}

	public override string ToString()
	{
		return "[x:" + x + ", y:" + y + ", isConnection:" + isConnection + "]";
	}

	public bool Equals(MazeSquare mzSquare)
	{
		return x == mzSquare.x && y == mzSquare.y && isConnection == mzSquare.isConnection;
	}
	public bool Borders(MazeSquare mzSquare)
	{
		return (Mathf.Abs (x - mzSquare.x) < 1 && mzSquare.y == y) || (Mathf.Abs (y - mzSquare.y) < 1 && mzSquare.x == x);
	}
}
