﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Doll
{
    public class InGameManager : MonoBehaviour
    {
        private static InGameManager self = null;
        public static InGameManager Instance
        {
            get { return self; }
        }

        public Object_Manager_Script objManagerScript;
        public GameObject floor;

        [System.Serializable]
        public class InGameUI
        {
            public Text text_Timer;
            public Text text_Score;
            public Slider slider_Resource;
        }
        public InGameUI inGameUI;

        [System.Serializable]
        public class ButtonEffect
        {
            public GameObject effect_Bear;
            public GameObject effect_Rabbit;
            public GameObject effect_Fox;
        }
        public ButtonEffect buttonEffect;

        [System.Serializable]
        public class ResultGameUI
        {
            public Canvas canvas_Result;
            public Text text_ResultScore;
        }
        public ResultGameUI resultGameUI;

        void Awake()
        {
            if (null == self)
            {
                self = this;
            }
            else
            {
                Debug.LogWarning("Must be single monobehaviour.");
                return;
            }
        }

        private void Start()
        {
            resultGameUI.canvas_Result.gameObject.SetActive(false);
            GameManagerScript.Instance.inGameVars.isGameStart = true;
            OnClick_SelectBear();
        }

        public void OnClick_Retry()
        {
            SceneManager.LoadScene("Title");
            GameManagerScript.Instance.InitInGameVars();
        }

        public void OnClick_SelectBear()
        {
            objManagerScript.SetSelectedDollIndex((int)Object_Manager_Script.eDoll.Bear);
            buttonEffect.effect_Bear.SetActive(true);
            buttonEffect.effect_Rabbit.SetActive(false);
            buttonEffect.effect_Fox.SetActive(false);

            GameManagerScript.Instance.SubResource();
            SetResourceUI();
        }

        public void OnClick_SelectRabbit()
        {
            objManagerScript.SetSelectedDollIndex((int)Object_Manager_Script.eDoll.Rabbit);
            buttonEffect.effect_Bear.SetActive(false);
            buttonEffect.effect_Rabbit.SetActive(true);
            buttonEffect.effect_Fox.SetActive(false);

            GameManagerScript.Instance.SubResource();
            SetResourceUI();
        }

        public void OnClick_SelectFox()
        {
            objManagerScript.SetSelectedDollIndex((int)Object_Manager_Script.eDoll.Fox);
            buttonEffect.effect_Bear.SetActive(false);
            buttonEffect.effect_Rabbit.SetActive(false);
            buttonEffect.effect_Fox.SetActive(true);

            GameManagerScript.Instance.SubResource();
            SetResourceUI();
        }

        public Vector3 Set_Random_Destiny()
        {
            float floorWidth = floor.GetComponent<BoxCollider2D>().size.x;
            float floorHeight = floor.GetComponent<BoxCollider2D>().size.y;
            float destPosX = Random.Range(-floorWidth / 2, floorWidth / 2);
            float destPosY = Random.Range(floor.GetComponent<BoxCollider2D>().offset.y - (floorHeight / 2), floor.GetComponent<BoxCollider2D>().offset.y + (floorHeight / 2));

            return new Vector3(destPosX, destPosY, 0);
        }

        void SetRemainTime()
        {
            float remainTime = GameManagerScript.Instance.inGameVars.remainTime;
            int Sec = Mathf.FloorToInt(remainTime);
            if (Sec <= 0)
            {
                Sec = 0;
            }
            int mSec = Mathf.FloorToInt((remainTime * 100) - (Sec * 100));
            if (mSec <= 0)
            {
                mSec = 0;
            }

            inGameUI.text_Timer.text = string.Format("{0}:{1}", Sec.ToString("D2"), mSec.ToString("D2"));
        }

        void SetResourceUI()
        {
            int maxResource = GameManagerScript.Instance.gameSetting.ResourceMaxCount;
            int curResource = GameManagerScript.Instance.inGameVars.curResource;
            float perResource = curResource / maxResource;

            inGameUI.slider_Resource.value = perResource;
        }

        // Update is called once per frame
        void Update()
        {
            if (GameManagerScript.Instance.inGameVars.isGameOver == false && GameManagerScript.Instance.inGameVars.isGameStart == true)
            {
                GameManagerScript.Instance.SetElapsedTime();
                GameManagerScript.Instance.SetRemainTime();
                GameManagerScript.Instance.CheckAddResource();
            }
            else if (GameManagerScript.Instance.inGameVars.isGameOver == true)
            {
                //Show Time Over UI
                resultGameUI.canvas_Result.gameObject.SetActive(true);
            }

            SetRemainTime();
            SetResourceUI();
        }
    }
}