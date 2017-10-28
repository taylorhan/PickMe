using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_Manager_Script : MonoBehaviour {
	public GameObject[] Dolls_obj;

	public GameObject floor;

	public class Doll : MonoBehaviour
	{
		public int index = 0;
		public int Type = 2;

		public void Set_Information(int index, int Type){
			this.index = index;
			this.Type = Type;
		}

	}
	public class BearDoll : Doll{

	}
	public class RabbitDoll : Doll{

	}
	public class FoxDoll : Doll{

	}
	public class EBearDoll : Doll{

	}
	public class ERabbitDoll : Doll{

	}
	public class EFoxDoll : Doll{

	}

	public List<GameObject> BearList;
	public List<GameObject> RabbitList;
	public List<GameObject> FoxList;
	public List<GameObject> EBearList;
	public List<GameObject> ERabbitList;
	public List<GameObject> EFoxList;

	public enum eDoll
	{
		Bear = 0,
		Rabbit,
		Fox,
		EBear,
		ERabbit,
		EFox,
		Max,
	}

	int layermask;
	public int Change_Mon_Num;

	public static int Bear_Count = 0;
	public static int Rabbit_Count = 0;
	public static int Fox_Count = 0;
	public static int EBear_Count = 0;
	public static int ERabbit_Count = 0;
	public static int EFox_Count = 0;

	// Use this for initialization
	void Start () {
		Dolls_obj = new GameObject[6];
		Dolls_obj[(int)eDoll.Bear] = GameObject.Find ("Bear");
		Dolls_obj[(int)eDoll.Rabbit] = GameObject.Find ("Rabbit");
		Dolls_obj[(int)eDoll.Fox] = GameObject.Find ("Fox");
		Dolls_obj[(int)eDoll.EBear] = GameObject.Find ("EBear");
		Dolls_obj[(int)eDoll.ERabbit] = GameObject.Find ("ERabbit");
		Dolls_obj[(int)eDoll.EFox] = GameObject.Find ("EFox");

		layermask = (-1) - (1 << LayerMask.NameToLayer ("Monster"));

		Change_Mon_Num = 5;

		BearList = new List<GameObject> ();
		BearList = new List<GameObject> ();
		RabbitList = new List<GameObject> ();
		FoxList = new List<GameObject> ();
		EBearList = new List<GameObject> ();
		ERabbitList = new List<GameObject> ();
		EFoxList = new List<GameObject> ();

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

	void AddDollToFloor(Transform tDoll)
	{
		if (floor == null)
			return;

		tDoll.parent = floor.transform;
	}

	public void Insert_Obj(int num, Vector3 hitInfo){
		switch (num) {
		case 1:
			if (Dolls_obj [(int)eDoll.Bear].transform.childCount > 0) {
				GameObject newBear = Dolls_obj [(int)eDoll.Bear].transform.GetChild (0).gameObject as GameObject;
				BearDoll bearDollComp = newBear.GetComponent<BearDoll> ();
				bearDollComp.Set_Information (Bear_Count++ , (int)eDoll.Bear);
				BearList.Add (newBear);
				AddDollToFloor (newBear.transform);
			}
			break;
		case 2:
			if (Dolls_obj [(int)eDoll.Rabbit].transform.childCount > 0) {
				GameObject newRabbit = Dolls_obj [(int)eDoll.Rabbit].transform.GetChild (0).gameObject as GameObject;
				RabbitDoll rabbitDollComp = newRabbit.GetComponent<RabbitDoll> ();
				rabbitDollComp.Set_Information (Rabbit_Count++, (int)eDoll.Rabbit);
				RabbitList.Add (newRabbit);
				AddDollToFloor (newRabbit.transform);
			}
			break;
		case 3:
			if (Dolls_obj [(int)eDoll.Fox].transform.childCount > 0) {
				GameObject newFox = Dolls_obj [(int)eDoll.Fox].transform.GetChild (0).gameObject as GameObject;
				FoxDoll foxDollComp = newFox.GetComponent<FoxDoll> ();
				foxDollComp.Set_Information (Fox_Count++, (int)eDoll.Fox);
				FoxList.Add (newFox);
				AddDollToFloor (newFox.transform);
			}
			break;
		case 4:
			if (Dolls_obj [(int)eDoll.EBear].transform.childCount > 0) {
				GameObject newEBear = Dolls_obj [(int)eDoll.EBear].transform.GetChild (0).gameObject as GameObject;
				EBearDoll ebearDollComp = newEBear.GetComponent<EBearDoll> ();
				ebearDollComp.Set_Information (EBear_Count++, (int)eDoll.EBear);
				EBearList.Add (newEBear);
				AddDollToFloor (newEBear.transform);
			}
			break;
		case 5:
			if (Dolls_obj [(int)eDoll.ERabbit].transform.childCount > 0) {
				GameObject newERabbit = Dolls_obj [(int)eDoll.ERabbit].transform.GetChild (0).gameObject as GameObject;
				ERabbitDoll erabbitDollComp = newERabbit.GetComponent<ERabbitDoll> ();
				erabbitDollComp.Set_Information (ERabbit_Count++, (int)eDoll.ERabbit);
				ERabbitList.Add (newERabbit);
				AddDollToFloor (newERabbit.transform);
			}
			break;
		case 6:
			if (Dolls_obj [(int)eDoll.EFox].transform.childCount > 0) {
				GameObject newEFox = Dolls_obj [(int)eDoll.EFox].transform.GetChild (0).gameObject as GameObject;
				EFoxDoll efoxDollComp = newEFox.GetComponent<EFoxDoll> ();
				efoxDollComp.Set_Information (EFox_Count++, (int)eDoll.EFox);
				EFoxList.Add (newEFox);
				AddDollToFloor (newEFox.transform);
			}
			break;
		}
			
	}


	public GameObject Check_Num(List<GameObject> list, int index){
		foreach(GameObject obj in list)
		{
			Doll doll = obj.GetComponent<Doll> ();
			if (doll.index == index)
			{
				return obj;
			}
		}

		return null;
	}
	public void Delete_Obj(List<GameObject> list, GameObject obj){

		list.Remove (obj);
		obj.SetActive (false);
	}
}
