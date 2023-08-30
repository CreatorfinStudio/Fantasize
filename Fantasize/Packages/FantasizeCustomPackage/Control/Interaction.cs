using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Control
{
    public class Interaction : Controller
    {
        private bool checkInteractioning = false;
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.F) && checkInteractioning)
                Debug.Log("상호작용 On");
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.name.Equals("IntertactionCube"))
                checkInteractioning = true;
            else
                checkInteractioning = false;
        }
    }
}