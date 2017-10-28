using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Change_Button_Script : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnGUI(){
		if(GUI.RepeatButton(new Rect(850,650,150,80),"Bear")){
			GameObject.Find ("Object Manager").GetComponent<Object_Manager_Script> ().Change_Mon_Num = 1;
			Debug.Log("BEAR");
		}
		if(GUI.RepeatButton(new Rect(650,650,150,80),"Rabbit")){
			GameObject.Find ("Object Manager").GetComponent<Object_Manager_Script> ().Change_Mon_Num = 2;
			Debug.Log("BEAR");
		}
		if(GUI.RepeatButton(new Rect(450,650,150,80),"Fox")){
			GameObject.Find ("Object Manager").GetComponent<Object_Manager_Script> ().Change_Mon_Num = 3;
			Debug.Log("BEAR");
		}

	}
}
