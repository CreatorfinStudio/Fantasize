using UnityEngine;
using System.Collections;

namespace Control
{
    public class LongClick : Controller
    {
        private float longClickThreshold = 2.0f;
        private Coroutine longClickCoroutine;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (longClickCoroutine != null)
                {
                    StopCoroutine(longClickCoroutine);
                }
                longClickCoroutine = StartCoroutine(CheckLongClick());
            }
        }

        private IEnumerator CheckLongClick()
        {
            float startTime = Time.time;

            while (Time.time - startTime < longClickThreshold)
            {
                if (!Input.GetMouseButton(0))
                {
                    break;
                }

                yield return null;
            }

            if (Time.time - startTime >= longClickThreshold)
            {
                isLongClicking = true;
                Debug.Log("Long Click!");
            }
            else
            {
                isLongClicking = false;
            }
        }
    }
}