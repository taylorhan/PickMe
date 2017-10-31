using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Doll
{
    public class StageManagerScript : MonoBehaviour
    {

        private static StageManagerScript self = null;
        public static StageManagerScript Instance
        {
            get { return self; }
        }

        private float enemyCreateCoolTime = 3;
        private float elapsedTime = 0;

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

        // Use this for initialization
        void Start()
        {
            SetEnemyCoolTime();
        }

        // Update is called once per frame
        void Update()
        {
            CheckStageChange();
            CreateEnemyChar();
        }

        void SetEnemyCoolTime()
        {
            enemyCreateCoolTime = 1f * GameManagerScript.Instance.gameSetting.ResourceChargingSpeed;
        }

        void CreateEnemyChar()
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime > enemyCreateCoolTime)
            {
                elapsedTime = 0;
                int eDollIndex = Random.Range((int)Object_Manager_Script.eDoll.EBear, (int)Object_Manager_Script.eDoll.Max);
                Vector3 ranPos = InGameManager.Instance.Set_Random_Destiny();
                InGameManager.Instance.objManagerScript.Insert_Obj(eDollIndex, ranPos, true);
            }
        }

        void CheckStageChange()
        {
            if (GameManagerScript.Instance.inGameVars.difficulty == 3 || GameManagerScript.Instance.inGameVars.isGameOver == true)
                return;

            int remainSec = Mathf.FloorToInt(GameManagerScript.Instance.inGameVars.remainTime);

            if (remainSec < 40 && GameManagerScript.Instance.inGameVars.difficulty < 2)
            {
                GameManagerScript.Instance.inGameVars.difficulty = 2;
                SetEnemyCoolTime();
                GameManagerScript.Instance.gameSetting.DollSpeed = 6;
            }
            else if (remainSec < 15 && GameManagerScript.Instance.inGameVars.difficulty < 3)
            {
                GameManagerScript.Instance.inGameVars.difficulty = 3;
                SetEnemyCoolTime();
                GameManagerScript.Instance.gameSetting.DollSpeed = 7;
            }
            else
            {
                GameManagerScript.Instance.inGameVars.difficulty = 1;
                SetEnemyCoolTime();
            }

            GameManagerScript.Instance.gameSetting.ResourceChargingSpeed = 3 / GameManagerScript.Instance.inGameVars.difficulty;
        }
    }

}
