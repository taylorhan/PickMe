using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Doll
{
    public class Object_Manager_Script : MonoBehaviour
    {
		public GameObject[] DollsPrefabArray;
		public int Count;
		[HideInInspector]
        public GameObject[] Dolls_obj;

        public GameObject floor;
        
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
        public int SelectedDollIndex;

		public static int[] arrDollCount = new int[6];	

		bool Flag;
        // Use this for initialization
        void Start()
        {
            Dolls_obj = new GameObject[6];
            Dolls_obj[(int)eDoll.Bear] = GameObject.Find("Bear");
            Dolls_obj[(int)eDoll.Rabbit] = GameObject.Find("Rabbit");
            Dolls_obj[(int)eDoll.Fox] = GameObject.Find("Fox");
            Dolls_obj[(int)eDoll.EBear] = GameObject.Find("EBear");
            Dolls_obj[(int)eDoll.ERabbit] = GameObject.Find("ERabbit");
            Dolls_obj[(int)eDoll.EFox] = GameObject.Find("EFox");

            layermask = (-1) - (1 << LayerMask.NameToLayer("Monster"));

            SetSelectedDollIndex((int)eDoll.Bear);

            BearList = new List<GameObject>();
            BearList = new List<GameObject>();
            RabbitList = new List<GameObject>();
            FoxList = new List<GameObject>();
            EBearList = new List<GameObject>();
            ERabbitList = new List<GameObject>();
            EFoxList = new List<GameObject>();

			instantiatePrefab (Count);
			Flag = false;
        }

		public void instantiatePrefab(int count){
			for (int i = 0; i < DollsPrefabArray.Length; i++) {
				for (int j = 0; j < count; j++) {
					GameObject newObj = Instantiate (DollsPrefabArray [i], Vector2.zero, Quaternion.identity);
					newObj.GetComponent<Doll> ().Set_Information (0, i);
					newObj.SetActive (false);
					newObj.transform.parent = Dolls_obj [newObj.GetComponent<Doll> ().Type].transform;
				}
			}
		}
		public void instantiatePrefabFake(int count, int i){
			for (int j = 0; j < count; j++) {
				GameObject newObj = Instantiate (DollsPrefabArray [i], Vector2.zero, Quaternion.identity);
				newObj.GetComponent<Doll> ().Set_Information (0, i);
				newObj.SetActive (false);
				newObj.transform.parent = Dolls_obj [newObj.GetComponent<Doll> ().Type].transform;
			}
		}

        // Update is called once per frame
        void Update()
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Input.GetMouseButtonUp(0))
            {
                if (Physics.Raycast(ray, out hitInfo, 100f, layermask))
                {
                    if (hitInfo.transform.tag == "Ground")
                    {
                        Debug.Log(hitInfo.point);
                        Insert_Obj(SelectedDollIndex, hitInfo.point);
                    }
                }
            }
        }

        public void SetSelectedDollIndex(int index)
        {
            SelectedDollIndex = index;
        }

        bool AddDollToFloor(Transform tDoll)
        {
            if (floor == null)
                return false;

            tDoll.parent = floor.transform;
            return true;
        }

        public void Insert_Obj(int eDollIndex, Vector3 hitInfo)
        {
			if (Dolls_obj [eDollIndex].transform.childCount <= 0) {
				instantiatePrefabFake (Count, eDollIndex);
			}
                
            GameObject dollObj = Dolls_obj[eDollIndex].transform.GetChild(0).gameObject as GameObject;
            Doll dollComp = dollObj.GetComponent<Doll>();
            dollComp.Set_Information(arrDollCount[eDollIndex]++, eDollIndex);
            BearList.Add(dollObj);
            bool isAdded = AddDollToFloor(dollObj.transform);
			dollObj.transform.position = hitInfo;
            dollObj.SetActive(isAdded);
			dollObj.GetComponent<Doll> ().Limit_Time = 1.5f;
			dollObj.GetComponent<Doll> ().SetupSuper (true);
        }
			

        /*
        public GameObject Check_Num(List<GameObject> list, int index)
        {
            foreach (GameObject obj in list)
            {
                Doll doll = obj.GetComponent<Doll>();
                if (doll.index == index)
                {
                    return obj;
                }
            }

            return null;
        }
        */

        public void Delete_Obj(List<GameObject> list, GameObject obj)
        {
			list.Remove(obj);

			Transform tParent = Dolls_obj [obj.GetComponent<Doll> ().Type].transform;
			if (tParent.childCount > 10) {
				Destroy (obj);
			} else {
				obj.transform.parent = tParent;
				obj.SetActive(false);
			}
        }
    }
}