using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Doll
{
    public class Picker_Manager_Script : MonoBehaviour
    {

	    public float DownPeriod;
	    public float elapsedTime;
	    public bool Stop_Check;

        Vector2 Picker_Pos;

        float previous;
        public float speed;
        bool Check = false;

        Vector3 Destiny;
        GameObject floor;

        void Start ()
        {
		    DownPeriod = 3f;
		    elapsedTime = 0f;
		    Stop_Check = false;
			speed = 10f;

            Destiny = Vector3.zero;
            floor = InGameManager.Instance.floor;
        }

        void Update ()
        {
	        if (Stop_Check == false)
            {
		        elapsedTime += Time.deltaTime;
		        if (elapsedTime > DownPeriod)
                {
                    elapsedTime = 0;
                    previous = this.transform.position.y;

			        Stop_Check = true;

                    Destiny = InGameManager.Instance.Set_Random_Destiny();
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
	    public	void DownPicker()
        {
		    if (Destiny.x != this.transform.localPosition.x)
            {
			    transform.localPosition = Vector2.MoveTowards (new Vector2 (transform.localPosition.x, transform.localPosition.y), new Vector2 (Destiny.x, transform.localPosition.y), speed);
		    }
            else if (Destiny.y != this.transform.localPosition.y)
            {
			    transform.localPosition = Vector2.MoveTowards (new Vector2 (transform.localPosition.x, transform.localPosition.y), new Vector2 (Destiny.x, Destiny.y), speed);
		    }
            else
            {
			    Check = true;
		    }
	    }
	    public void ReturnPicker()
        {
		    if (previous != this.transform.localPosition.y)
            {
			    transform.localPosition = Vector2.MoveTowards (new Vector2 (transform.localPosition.x, transform.localPosition.y), new Vector2 (transform.localPosition.x, previous), speed);
		    }
            else
            {
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
