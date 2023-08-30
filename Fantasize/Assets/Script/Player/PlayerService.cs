using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;
using Unity.VisualScripting;
using Control;

namespace Player
{
    public class PlayerService : MonoBehaviour
    {
        private void Start()
        {
            
            SetInit();
        }

        private void SetInit()
        {
            this.AddComponent<KeyboardMove>();
            this.AddComponent<MouseRotation>();
            this.AddComponent<ComboAttack>();
            this.AddComponent<LongClick>(); 
        }
    }
}