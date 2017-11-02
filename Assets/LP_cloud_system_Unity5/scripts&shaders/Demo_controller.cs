using UnityEngine;
using System.Collections;

public class Demo_controller : MonoBehaviour {
	public GameObject cloud_system1;
	public GameObject cloud_system2;
	public GameObject cloud_system3;
	public GameObject cloud_system4;

	private GameObject cur_cloud_system;
	// Use this for initialization
	void Start () {
		cur_cloud_system = cloud_system3;
		cur_cloud_system = Instantiate(cur_cloud_system,this.transform.position,Quaternion.identity) as GameObject;
		cur_cloud_system.name = cloud_system3.name;
	}

	void OnGUI(){
		GUIStyle blStyle = new GUIStyle();
		blStyle.normal.textColor = Color.black;
		GUI.Label(new Rect(10,65,200,20),"Controls: WASD",blStyle);
		GUI.Label(new Rect(10,85,200,20),"10x faster: Hold Shift",blStyle);
		GUI.Label(new Rect(10,105,200,20),"Rotation: Left Mous Btn",blStyle);
		GUI.Label(new Rect(10,125,200,20),"Cloud Type: "+cur_cloud_system.name,blStyle);
		if (GUI.Button(new Rect(10,150,200,20),"cloud_type 1") && cur_cloud_system.name!= cloud_system1.name){
			

			Destroy(cur_cloud_system);
			cur_cloud_system = Instantiate(cloud_system1,this.transform.position,Quaternion.identity) as GameObject;
			cur_cloud_system.name = cloud_system1.name;
		}
		if (GUI.Button(new Rect(10,180,200,20),"cloud_type 2") && cur_cloud_system.name!=cloud_system2.name){
			Destroy(cur_cloud_system);
			cur_cloud_system = Instantiate(cloud_system2,this.transform.position,Quaternion.identity) as GameObject;
			cur_cloud_system.name = cloud_system2.name;
		}
		if (GUI.Button(new Rect(10,210,200,20),"cloud_type 3") && cur_cloud_system.name!=cloud_system3.name){
			Destroy(cur_cloud_system);
			cur_cloud_system = Instantiate(cloud_system3,this.transform.position,Quaternion.identity) as GameObject;
			cur_cloud_system.name = cloud_system3.name;
		}
		if (GUI.Button(new Rect(10,240,200,20),"cloud_type 4") && cur_cloud_system.name!=cloud_system4.name){
			Destroy(cur_cloud_system);
			cur_cloud_system = Instantiate(cloud_system4,this.transform.position,Quaternion.identity) as GameObject;
			cur_cloud_system.name = cloud_system4.name;
		}
		//in case of additional cloud types
		/*
		if (GUI.Button(new Rect(10,270,200,20),"cloud_type 5") && cur_cloud_system.name!=cloud_system5.name){
			Destroy(cur_cloud_system);
			cur_cloud_system = Instantiate(cloud_system5,this.transform.position,Quaternion.identity) as GameObject;
			cur_cloud_system.name = cloud_system5.name;
		}
		if (GUI.Button(new Rect(10,300,200,20),"cloud_type 6") && cur_cloud_system.name!=cloud_system6.name){
			Destroy(cur_cloud_system);
			cur_cloud_system = Instantiate(cloud_system6,this.transform.position,Quaternion.identity) as GameObject;
			cur_cloud_system.name = cloud_system6.name;
		}
		if (GUI.Button(new Rect(10,330,200,20),"clouds_mixed") && cur_cloud_system.name!=cloud_system7.name){
			Destroy(cur_cloud_system);
			cur_cloud_system = Instantiate(cloud_system7,this.transform.position,Quaternion.identity) as GameObject;
			cur_cloud_system.name = cloud_system7.name;

		}
		*/

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
