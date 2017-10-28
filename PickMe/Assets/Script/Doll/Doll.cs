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
            Idle = 0, //대기
            Move, //이동 중
            Battle, //전투 중
            Invincible, //무적(집게 충돌 중)
            Pickup, //집게 올라가는 중
            Dead,
        }
        eDollState dollState = eDollState.Idle;

		Vector3 Destiny;

        public void Set_Information(int index, int Type)
        {
            //Debug.Log(string.Format("Set_Information index({0}), Type({1})", index, Type));

            this.index = index;
            this.Type = Type;
        }

        // Use this for initialization
        public void InitDoll()
        {
			Destiny = Vector3.zero;

            floor = InGameManager.Instance.floor;
            objManagerScript = InGameManager.Instance.objManagerScript;
        }

        public void SetDollState(eDollState state)
        {
            dollState = state;
        }

		public void Set_Random_Destiny()
        {
            float floorWidth = floor.GetComponent<BoxCollider2D>().size.x;
            float floorHeight = floor.GetComponent<BoxCollider2D>().size.y;
            float destPosX = Random.Range(-floorWidth / 2, floorWidth / 2);
            float destPosY = Random.Range(-floorHeight / 2, floorHeight / 2);
            Destiny = new Vector3 (destPosX, destPosY, 0);
		}

		public void SetMove()
        {
			if (Check_Super == true)
				return;

            float maxDistDt = Time.deltaTime * GameManagerScript.Instance.gameSetting.DollSpeed;
            Vector3 targetPos = new Vector3(Destiny.x, Destiny.y, Destiny.z);
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, targetPos, maxDistDt);
		}

        public void CheckState()
        {
            switch (dollState)
            {
                case eDollState.Idle:
                    {
                        //일정 시간 무적 적용
					SetDollState (eDollState.Move);
                        //이동 애니메이션 설정
                    }
                    break;
                case eDollState.Move:
                    {
					if (this.transform.position == Destiny || Destiny == Vector3.zero) {
						Set_Random_Destiny ();
					}
					SetMove ();

                    
                    }
                    break;
                case eDollState.Battle:
                    {
                        //전투 애니메이션 설정
                        //죽언ㅆ는지 살았는지 확인
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
            if (enemyObj.tag != "Doll")
                return false;

            if (enemyObj.GetComponent<Doll>().Type == (int)enemyType)
            {
                objManagerScript.Delete_Obj(list, selfObj);
                return true;
            }

            return false;
        }
		public float Timer;
		public float Limit_Time;
		public bool Check_Super;

		public void SetupSuper(bool start){
			if (start) {
				Check_Super = true;
				Timer = 0f;
			}
		}

		public bool invincible;

        void StartDeadAnim()
        {

        }
    }
}

