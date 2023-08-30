using Definition;
using System.Collections;
using UnityEngine;
using Control;

namespace Manager
{
    public class GameSingleton : MonoBehaviour
    {
        #region Singleton
        private static GameSingleton instance;

        // 게임 싱글톤 인스턴스에 접근하는 프로퍼티
        public static GameSingleton Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<GameSingleton>();

                    if (instance == null)
                    {
                        GameObject singletonObject = new GameObject();
                        instance = singletonObject.AddComponent<GameSingleton>();
                        singletonObject.name = "GameSingleton";
                        DontDestroyOnLoad(singletonObject);
                    }
                }
                return instance;
            }
        }
        #endregion

        #region Components

        #region Player
        private KeyboardMove k;
        private MouseRotation m;
        private ComboAttack c;
        #endregion

        #endregion

        #region Test

        #endregion
        public TestWindowProperty testWindowProperty;

        private void Start()
        {
            testWindowProperty = new TestWindowProperty();
            StartCoroutine(Test_AddEvent());
        }

        #region 승원이 테스트용 (나중에 필요없으면 지울것)

        [SerializeField]
        GameObject myUnit;

        public void Test_SetPlayerValue(TestWindowProperty t)
        {
            testWindowProperty.MoveSpeed = t.MoveSpeed;
            testWindowProperty.RotationSpeed = t.RotationSpeed;
            testWindowProperty.MaxComboAttacks = t.MaxComboAttacks;
        }

        IEnumerator Test_AddEvent()
        {
            while (k == null && m == null && c == null) 
            {
                k = myUnit?.GetComponent<KeyboardMove>();
                m = myUnit?.GetComponent<MouseRotation>();
                c = myUnit?.GetComponent<ComboAttack>();

                if (k != null && m != null)
                    AddEvent();
                yield return new WaitForEndOfFrame();
            }     

            void AddEvent()
            {
                testWindowProperty.Test_ChangedMoveSpeed += HandleMoveSpeed;
                testWindowProperty.Test_ChangedRotationSpeed += HandleRotationSpeed;
                testWindowProperty.Test_ChangeMaxComboAttacks += HandleMaxComboAttacks;
            }
        }
        private void HandleMoveSpeed(float newMoveSpeed) => k.moveSpeed = newMoveSpeed;
        private void HandleRotationSpeed(float newRotationSpeed) => m.rotationSpeed = newRotationSpeed;
        private void HandleMaxComboAttacks(int maxComboAttacks) => c.maxComboAttacks = maxComboAttacks;


        #endregion


    }
}