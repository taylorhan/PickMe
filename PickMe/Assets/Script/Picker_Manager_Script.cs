using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Doll
{
    public class Picker_Manager_Script : MonoBehaviour
    {

	    public float Down_Time;
	    public float Time_Check;
	    public bool Stop_Check;

        Vector2 Picker_Pos;

	    void Start () {
		    Down_Time = 3f;
		    Time_Check = 0f;
		    Stop_Check = false;
			speed = 3f;
			//newVec = Vector2.zero;

            Destiny = Vector3.zero;
            floor = InGameManager.Instance.floor;
        }

	    //Vector2 newVec;
	    float previous;
	    public float speed;
	    bool Check = false;

        Vector3 Destiny;
        GameObject floor;

        // Update is called once per frame
        public void Set_Random_Destiny()
        {
            float floorWidth = floor.GetComponent<BoxCollider2D>().size.x;
            float floorHeight = floor.GetComponent<BoxCollider2D>().size.y;
            float destPosX = Random.Range(-floorWidth / 2, floorWidth / 2);
            float destPosY = Random.Range(-floorHeight / 2, floorHeight / 2);
            Destiny = new Vector3(destPosX, destPosY, 0);
        }

        void Update () {
	        if (Stop_Check == false) {
		        Time_Check += Time.deltaTime;
		        if (Time_Check > Down_Time)
                {
			        previous = this.transform.position.y;

			        Stop_Check = true;

                    /*
			        float X = Random.Range (-floor.transform.localScale.x + this.transform.localScale.x, floor.transform.localScale.x - this.transform.localScale.x);
			        float Y = Random.Range (-floor.transform.localScale.y + this.transform.localScale.y, floor.transform.localScale.y - this.transform.localScale.y);
                    */
                    //newVec = new Vector2 (X / 2, Y / 2);

                    Set_Random_Destiny();
		        }
	        }
            else
            {
	            if (Check == false)
                {
                    DownPicker();
                }
	            if (Check == true)
                {
                    ReturnPicker();
                }
            }
	    }
	    public	void DownPicker(){
		    if (Destiny.x != this.transform.position.x) {
			    transform.position = Vector2.MoveTowards (new Vector2 (transform.position.x, transform.position.y), new Vector2 (Destiny.x, transform.position.y), speed * Time.deltaTime);
		    } else if (Destiny.y != this.transform.position.y) {
			    transform.position = Vector2.MoveTowards (new Vector2 (transform.position.x, transform.position.y), new Vector2 (Destiny.x, Destiny.y), speed * Time.deltaTime);
		    } else {
			    Check = true;
		    }
	    }
	    public void ReturnPicker(){
		    if (previous != this.transform.position.y) {
			    transform.position = Vector2.MoveTowards (new Vector2 (transform.position.x, transform.position.y), new Vector2 (transform.position.x, previous), speed * Time.deltaTime);
		    } else {
			    Time_Check = 0;
			    Check = false;
			    Stop_Check = false;
		    }
	    }
	    void OnTriggerEnter2D(Collider2D col)
        {
		    Debug.Log ("! : " + col.tag);
	    }
    }
}
