using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Doll
{
    public class Doll : MonoBehaviour
    {
        public int index = 0;
        public int Type = 2;

		public GameObject floor;

        public enum eDollState
        {
            Idle = 0,
            Move, //이동 중
            Battle, //전투 중
            Invincible, //무적(집게 충돌 중)
            Pickup, //집게 올라가는 중
        }
        eDollState dollState;

		Vector3 Destiny;
		float Speed = 5f;

        public void Set_Information(int index, int Type)
        {
            Debug.Log(string.Format("Set_Information index({0}), Type({1})", index, Type));

            this.index = index;
            this.Type = Type;
        }

        // Use this for initialization
        void Start()
        {
			Destiny = Vector3.zero;
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

        void CheckState()
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
                default:
                    break;
            }
        }


		public Object_Manager_Script objManagerScript;

		public void Set_Advantage(GameObject Myobj, GameObject Enemyobj){
			switch(Myobj.GetComponent<Doll>().Type){
			case (int)Object_Manager_Script.eDoll.Bear:
				if(Enemyobj.GetComponent<Doll>().Type == (int)Object_Manager_Script.eDoll.ERabbit)
					this.GetComponent<Object_Manager_Script>().Delete_Obj(this.GetComponent<Object_Manager_Script>().BearList,this.gameObject);
				break;
			case (int)Object_Manager_Script.eDoll.Rabbit:
				if(Enemyobj.GetComponent<Doll>().Type == (int)Object_Manager_Script.eDoll.EFox)
					this.GetComponent<Object_Manager_Script>().Delete_Obj(this.GetComponent<Object_Manager_Script>().RabbitList,this.gameObject);
				break;
			case (int)Object_Manager_Script.eDoll.Fox:
				if(Enemyobj.GetComponent<Doll>().Type == (int)Object_Manager_Script.eDoll.EBear)
					this.GetComponent<Object_Manager_Script>().Delete_Obj(this.GetComponent<Object_Manager_Script>().FoxList,this.gameObject);
				break;
			case (int)Object_Manager_Script.eDoll.EBear:
				if(Enemyobj.GetComponent<Doll>().Type == (int)Object_Manager_Script.eDoll.Rabbit)
					this.GetComponent<Object_Manager_Script>().Delete_Obj(this.GetComponent<Object_Manager_Script>().EBearList,this.gameObject);
				break;
			case (int)Object_Manager_Script.eDoll.ERabbit:
				if(Enemyobj.GetComponent<Doll>().Type == (int)Object_Manager_Script.eDoll.Fox)
					this.GetComponent<Object_Manager_Script>().Delete_Obj(this.GetComponent<Object_Manager_Script>().ERabbitList,this.gameObject);
				break;
			case (int)Object_Manager_Script.eDoll.EFox:
				if(Enemyobj.GetComponent<Doll>().Type == (int)Object_Manager_Script.eDoll.Bear)
					this.GetComponent<Object_Manager_Script>().Delete_Obj(this.GetComponent<Object_Manager_Script>().EFoxList,this.gameObject);
				break;
			}
		}
        // Update is called once per frame
        void Update()
        {
            CheckState();
        }
    }
}

