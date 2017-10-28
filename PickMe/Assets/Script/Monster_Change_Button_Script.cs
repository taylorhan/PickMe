using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Doll
{
    public class Monster_Change_Button_Script : MonoBehaviour
    {
        public Object_Manager_Script objManagerScript;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
        void OnGUI()
        {
            if (GUI.RepeatButton(new Rect(0, 0, 60, 80), "Bear"))
            {
                objManagerScript.SetSelectedDollIndex((int)Object_Manager_Script.eDoll.Bear);
                Debug.Log("BEAR");
            }
            if (GUI.RepeatButton(new Rect(60, 0, 60, 80), "Rabbit"))
            {
                objManagerScript.SetSelectedDollIndex((int)Object_Manager_Script.eDoll.Rabbit);
                Debug.Log("Rabbit");
            }
            if (GUI.RepeatButton(new Rect(120, 0, 60, 80), "Fox"))
            {
                objManagerScript.SetSelectedDollIndex((int)Object_Manager_Script.eDoll.Fox);
                Debug.Log("Fox");
            }

        }
    }
}
