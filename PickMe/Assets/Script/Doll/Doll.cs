using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Doll
{
    public class Doll : MonoBehaviour
    {
        public int index = 0;
        public int Type = 2;

		GameObject floor;
        [HideInInspector]
        public Object_Manager_Script objManagerScript;

        public enum eDollState
        {
            Idle = 0,
            Move, //이동 중
            Battle, //전투 중
            Invincible, //무적(집게 충돌 중)
            Pickup, //집게 올라가는 중
            Dead,
        }
        eDollState dollState = eDollState.Idle;

		Vector3 Destiny;
		float Speed = 5f;

        public void Set_Information(int index, int Type)
        {
            Debug.Log(string.Format("Set_Information index({0}), Type({1})", index, Type));

            this.index = index;
            this.Type = Type;
        }

        // Use this for initialization
        public void InitDoll()
        {
			Destiny = Vector3.zero;

            floor = GameManagerScript.Instance.floor;
            objManagerScript = GameManagerScript.Instance.objManagerScript;
        }

        public void SetDollState(eDollState state)
        {
            dollState = state;
        }

		public void Set_Random_Destiny(GameObject obj){
			Destiny = new Vector3 (Random.Range (-floor.transform.localScale.x + obj.transform.GetChild (0).transform.localScale.x, 
				floor.transform.localScale.x - obj.transform.GetChild (0).transform.localScale.x)/2,
				Random.Range (-floor.transform.localScale.y + obj.transform.GetChild (0).transform.localScale.y, 
					floor.transform.localScale.y - obj.transform.GetChild (0).transform.localScale.y)/2, 0);
		}
		public void SetMove(){
			transform.position = Vector3.MoveTowards (transform.position, new Vector3 (Destiny.x, Destiny.y, Destiny.z), Time.deltaTime * Speed);
		}

        public void CheckState()
        {
            switch (dollState)
            {
                case eDollState.Idle:
                    {
					SetDollState (eDollState.Move);
                    }
                    break;
                case eDollState.Move:
                    {
					if (this.transform.position == Destiny || Destiny == Vector3.zero) {
						Set_Random_Destiny (this.gameObject);
					}
					SetMove ();
                    }
                    break;
                case eDollState.Battle:
                    {

                    }
                    break;
                case eDollState.Invincible:
                    {

                    }
                    break;
                case eDollState.Pickup:
                    {

                    }
                    break;
                case eDollState.Dead:
                    {
                        //Animation 연출
                    }
                    break;
                default:
                    break;
            }
        }

        public bool CheckIsDead(Object_Manager_Script.eDoll enemyType, List<GameObject> list, GameObject selfObj, GameObject enemyObj)
        {
            if (enemyObj.GetComponent<Doll>().Type == (int)enemyType)
            {
                objManagerScript.Delete_Obj(list, selfObj);
                return true;
            }

            return false;
        }

        void StartDeadAnim()
        {

        }
    }
}

