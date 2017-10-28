using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            if(GUI.RepeatButton(new Rect(0, 0, 60, 80), "EBear"))
            {
                objManagerScript.SetSelectedDollIndex((int)Object_Manager_Script.eDoll.EBear);
                Debug.Log("EBEAR");
            }
            if (GUI.RepeatButton(new Rect(60, 0, 60, 80), "ERabbit"))
            {
                objManagerScript.SetSelectedDollIndex((int)Object_Manager_Script.eDoll.ERabbit);
                Debug.Log("ERabbit");
            }
            if (GUI.RepeatButton(new Rect(120, 0, 60, 80), "EFox"))
            {
                objManagerScript.SetSelectedDollIndex((int)Object_Manager_Script.eDoll.EFox);
                Debug.Log("EFox");
            }


            if (GUI.RepeatButton(new Rect(330, 0, 60, 80), "Exit"))
            {
                GameManagerScript.Instance.inGameVars.isGameOver = true;
                
                Debug.Log("Exit");
            }

            if (GameManagerScript.Instance.inGameVars.isGameOver == true)
            {
                if (GUI.RepeatButton(new Rect(Screen.width/2, Screen.height / 2, 60, 80), "Result"))
                {
                    // Load Result Scene
                    SceneManager.LoadScene("Result");
                    Debug.Log("Result");
                }
            }
        }
    }
}
