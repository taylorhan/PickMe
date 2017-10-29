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

            layermask = (-1) - (1 << LayerMask.NameToLayer("Ground"));

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
					newObj.transform.GetChild (0).gameObject.SetActive(true);
					newObj.transform.GetChild (1).gameObject.SetActive(true);
					newObj.transform.GetChild (2).gameObject.SetActive(false);
					newObj.transform.GetChild (3).gameObject.SetActive(false);
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
            if (Input.GetMouseButtonUp(0))
            {
                float posX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
                float posY = Camera.main.ScreenToWorldPoint(Input.mousePosition).y;
                RaycastHit2D hit = Physics2D.Raycast(new Vector2(posX, posY), Vector2.zero, 0f);
                if (hit == false)
                {
                    Debug.Log("Hit Error - Null");
                    return;
                }

                if (hit.transform.tag == "Ground")
                {
                    if (GameManagerScript.Instance.inGameVars.curResource < 1)
                    {
                        Debug.Log("Not Enough Resource");
                        return;
                    }

                    Debug.Log(string.Format("Hit Obj :: {0}", hit.transform.gameObject.name));
                    Insert_Obj(SelectedDollIndex, hit.point);
                }
            }
        }

        public void SetSelectedDollIndex(int index)
        {
            SelectedDollIndex = index;
        }

        bool AddDollToFloor(Transform tDoll)
        {
            if (InGameManager.Instance.floor == null)
            {
                Debug.Log("Object_Manager_Script::AddDollToFloor - Floor is Null");
                return false;
            }

            tDoll.parent = InGameManager.Instance.floor.transform;

            GameManagerScript.Instance.SubResource();
            InGameManager.Instance.SetResourceUI();

            return true;
        }

        public List<GameObject> GetDollTypeList(int eDollIndex)
        {
            switch (eDollIndex)
            {
                case (int)eDoll.Bear:
                    {
                        return BearList;
                    }
                case (int)eDoll.Rabbit:
                    {
                        return RabbitList;
                    }
                case (int)eDoll.Fox:
                    {
                        return FoxList;
                    }
                case (int)eDoll.EBear:
                    {
                        return EBearList;
                    }
                case (int)eDoll.ERabbit:
                    {
                        return ERabbitList;
                    }
                case (int)eDoll.EFox:
                    {
                        return EFoxList;
                    }
                default:
                    return BearList;
            }
        }

        public void Insert_Obj(int eDollIndex, Vector3 hitInfo)
        {
			if (Dolls_obj [eDollIndex].transform.childCount <= 0) {
				instantiatePrefabFake (Count, eDollIndex);
			}
                
            GameObject dollObj = Dolls_obj[eDollIndex].transform.GetChild(0).gameObject as GameObject;
            Doll dollComp = dollObj.GetComponent<Doll>();
            dollComp.Set_Information(arrDollCount[eDollIndex]++, eDollIndex);

            //Add To List
            List<GameObject> list = GetDollTypeList(eDollIndex);
            list.Add(dollObj);

            //Add To Floor & Activate GameObj
            bool isAdded = AddDollToFloor(dollObj.transform);
			dollObj.transform.position = new Vector3 (hitInfo.x, hitInfo.y , 0);
            dollObj.SetActive(isAdded);
			dollObj.transform.GetChild (2).transform.localScale = new Vector2 (0, 0);
			dollObj.transform.GetChild (2).gameObject.SetActive (true);
			StartCoroutine (MakePow (dollObj.transform.GetChild(2).transform, new Vector3 (8,8,0)));

            //Set Idle State
            dollComp.SetDollState(Doll.eDollState.Idle);
        }
		IEnumerator MakePow(Transform obj, Vector3 vec){
			while (obj.localScale.x < vec.x) {
				obj.localScale = new Vector3 (obj.localScale.x + 0.01f * Time.deltaTime, obj.localScale.y + 0.01f * Time.deltaTime, 0); 
			}
			yield return new WaitForSeconds(0.2f);
			obj.gameObject.SetActive (false);
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
			list.Remove (obj);

			Transform tParent = Dolls_obj [obj.GetComponent<Doll> ().Type].transform;

			if (tParent.childCount > 10)
            {
				Destroy (obj);
			}
            else
            {
				obj.transform.parent = tParent;
				obj.SetActive (false);
			}
        }
    }
}