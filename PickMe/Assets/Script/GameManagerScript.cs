using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Doll
{
    public class GameManagerScript : MonoBehaviour
    {
        private static GameManagerScript self = null;
        public static GameManagerScript Instance
        {
            get { return self; }
        }

        [System.Serializable]
        public class GameSetting
        {
            public int GameTime = 60;
            public int ResourceChargingSpeed = 3;
            public int ResourceMaxCount = 10;
            public Object_Manager_Script.eDoll objectiveDollType = Object_Manager_Script.eDoll.Bear;
            public float DollSpeed = 10f;
        }
        public GameSetting gameSetting;

        [System.Serializable]
        public class InGameVars
        {
            public float remainTime;
            public float elapsedTime;
            public int score;
            public int curResource;
            public float resourceChargeTime;
            //public int difficulty;

            public bool isGameOver;
        }
        public InGameVars inGameVars;

        public void InitInGameVars()
        {
            inGameVars.remainTime = gameSetting.GameTime;
            inGameVars.elapsedTime = 0;
            inGameVars.score = 0;
            inGameVars.curResource = 0;
            inGameVars.resourceChargeTime = 0;
            inGameVars.isGameOver = false;
        }

        void Awake()
        {
            if (null == self)
            {
                self = this;
            }
            else
            {
                Debug.LogWarning("Must be single monobehaviour.");
                Destroy(gameObject);
                return;
            }
        }

        void SetElapsedTime()
        {
            if (inGameVars.elapsedTime >= gameSetting.GameTime)
            {
                inGameVars.isGameOver = true;
                return;
            }

            inGameVars.elapsedTime += Time.deltaTime;
        }

        void SetRemainTime()
        {
            inGameVars.remainTime = gameSetting.GameTime - inGameVars.elapsedTime;
        }

        void CheckAddResource()
        {
            if (inGameVars.curResource >= gameSetting.ResourceMaxCount)
                return;

            inGameVars.resourceChargeTime += Time.deltaTime;
            if (inGameVars.resourceChargeTime > gameSetting.ResourceChargingSpeed)
            {
                inGameVars.curResource++;
            }
        }

        public void AddResource()
        {
            inGameVars.curResource++;
        }

        public void SubResource()
        {
            inGameVars.curResource--;
        }

        public void AddScore()
        {
            inGameVars.score++;
        }

        public void SubScore()
        {
            inGameVars.score--;
        }

        // Use this for initialization
        void Start()
        {
            DontDestroyOnLoad(gameObject);
            InitInGameVars();
        }

        // Update is called once per frame
        void Update()
        {
            if (inGameVars.isGameOver == false)
            {
                SetElapsedTime();
                SetRemainTime();
                CheckAddResource();
            }
            else
            {
                //Show Time Over UI
                
            }
            
        }
    }
}
