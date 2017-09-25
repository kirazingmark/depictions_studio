using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ListMazeBuilder : MonoBehaviour {

	public ListMazeBuilder(int[] maze)
	{
		for (int i = 0; i < maze.Length; i++) 
		{
			this.buildBlock ();
		}
	}

	public void buildBlock()
	{
		
	}
}
