using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_Manager_Script : MonoBehaviour {
	public GameObject Bear;
	public GameObject Rabbit;
	public GameObject Fox;
	public GameObject EBear;
	public GameObject ERabbit;
	public GameObject EFox;

	public class Doll
	{
		public int index = 0;
		public int Type = 2;

	}

	public List<Transform> EBList;
	public List<Transform> ERList;
	public List<Transform> EFList;
	public List<Transform> BList;
	public List<Transform> RList;
	public List<Transform> FList;
	public List<Doll> RList2;



	int layermask;
	public int Change_Mon_Num;

	// Use this for initialization
	void Start () {
		Bear = GameObject.Find ("Bear");
		Rabbit = GameObject.Find ("Rabbit");
		Fox = GameObject.Find ("Fox");
		EBear = GameObject.Find ("EBear");
		ERabbit = GameObject.Find ("ERabbit");
		EFox = GameObject.Find ("EFox");

		layermask = (-1) - (1 << LayerMask.NameToLayer ("Monster"));

		Change_Mon_Num = 5;

		RList2 = new List<Doll>();
	}
	
	// Update is called once per frame
	void Update () {
		
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hitInfo;
		if (Input.GetMouseButtonUp (0)) {
			if (Physics.Raycast (ray, out hitInfo, 100f, layermask)) {
				if (hitInfo.transform.tag == "Ground") {
					Debug.Log (hitInfo.point);
					Insert_Obj (Change_Mon_Num, hitInfo.point);
				}
			}
		}
	}

	public void Insert_Obj(int num, Vector3 hitInfo){
		
		switch (num) {
		case 1:
			if (Bear.transform.childCount > 0) {
				BList.Add (Bear.transform.GetChild (0));
				Bear.transform.GetChild (0).transform.position = hitInfo;
				Bear.transform.GetChild (0).gameObject.SetActive (true);
				Bear.transform.GetChild (0).parent = null; 
			}
			break;
		case 2:
			if (Rabbit.transform.childCount > 0) {
				RList.Add (Rabbit.transform.GetChild (0));
				Rabbit.transform.GetChild (0).transform.position = hitInfo;
				Rabbit.transform.GetChild (0).gameObject.SetActive (true);
				Rabbit.transform.GetChild (0).parent = null; 
			}
			break;
		case 3:
			if (Fox.transform.childCount > 0) {
				FList.Add (Fox.transform.GetChild (0));
				Fox.transform.GetChild (0).transform.position = hitInfo;
				Fox.transform.GetChild (0).gameObject.SetActive (true);
				Fox.transform.GetChild (0).parent = null; 
			}
			break;
		case 4:
			if (EBear.transform.childCount > 0) {
				EBList.Add (ERabbit.transform.GetChild (0));
				EBear.transform.GetChild (0).transform.position = hitInfo;
				EBear.transform.GetChild (0).gameObject.SetActive (true);
				EBear.transform.GetChild (0).parent = null; 
			}
			break;
		case 5:
			if (ERabbit.transform.childCount > 0) {
				ERList.Add (ERabbit.transform.GetChild (0));
				ERabbit.transform.GetChild (0).transform.position = hitInfo;
				ERabbit.transform.GetChild (0).gameObject.SetActive (true);
				ERabbit.transform.GetChild (0).parent = null; 
			}
			break;
		case 6:
			if (EFox.transform.childCount > 0) {
				EFList.Add (EFox.transform.GetChild (0));
				EFox.transform.GetChild (0).transform.position = hitInfo;
				EFox.transform.GetChild (0).gameObject.SetActive (true);
				EFox.transform.GetChild (0).parent = null; 
			}
			break;
		}


	}

	public void Delete_Obj(int num){
		switch (num) {
		case 1:
			if (BList.Count > 0) {
				Transform newTrans = BList [BList.Count-1].transform;
				newTrans.gameObject.SetActive (false);
				newTrans.parent = Bear.transform;
				BList.RemoveAt(BList.Count-1);
				//Debug.Log(BList.Count);
			}
			break;
		case 2:
			if (RList.Count > 0) {
				Transform newTrans = RList [RList.Count-1].transform;
				newTrans.gameObject.SetActive (false);
				newTrans.parent = Rabbit.transform;
				RList.RemoveAt(RList.Count-1);
			}
			break;
		case 3:
			if (FList.Count > 0) {
				Transform newTrans = FList [FList.Count-1].transform;
				newTrans.gameObject.SetActive (false);
				newTrans.parent = Fox.transform;
				FList.RemoveAt(FList.Count-1);
			}
			break;
		case 4:
			if (EBList.Count > 0) {
				Transform newTrans = EBList [EBList.Count-1].transform;
				newTrans.gameObject.SetActive (false);
				newTrans.parent = EBear.transform;
				BList.RemoveAt(BList.Count-1);
			}
			break;
		case 5:
			if (ERList.Count > 0) {
				Transform newTrans = ERList [ERList.Count-1].transform;
				newTrans.gameObject.SetActive (false);
				newTrans.parent = ERabbit.transform;
				ERList.RemoveAt(ERList.Count-1);
			}
			break;
		case 6:

			if (EFList.Count > 0) {
				Transform newTrans = EFList [EFList.Count-1].transform;
				newTrans.gameObject.SetActive (false);
				newTrans.parent = EFox.transform;
				EFList.RemoveAt(EFList.Count-1);
			}
			break;
		}

	}
}
