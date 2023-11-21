using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Definition
{
    public static class CoroutineWait
    {
        public static WaitForSeconds wait0001 = new WaitForSeconds(.001f);
        public static WaitForSeconds wait001 = new WaitForSeconds(.01f);
        public static WaitForSeconds wait01 = new WaitForSeconds(.1f);
        public static WaitForSeconds wait03 = new WaitForSeconds(.3f);
        public static WaitForSeconds wait05 = new WaitForSeconds(.5f);
        public static WaitForSeconds wait1 = new WaitForSeconds(1f);
        public static WaitForSeconds wait15 = new WaitForSeconds(1.5f);
        public static WaitForSeconds wait20 = new WaitForSeconds(2f);                
    }
}