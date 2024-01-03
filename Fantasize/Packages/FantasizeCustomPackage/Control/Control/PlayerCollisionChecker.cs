using Definition;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Control
{
    public class PlayerCollisionChecker : Controller
    {
        private void OnCollisionEnter2D(Collision2D collision)
        {
            switch(collision.gameObject.tag)
            {
                case "Monster":
                    DefinitionManager.Instance.iplayerInfo.SetHp(-1);
                    break;
            }
        }
        //private void OnTriggerEnter2D(Collider2D collision)
        //{
        //    switch (collision.tag)
        //    {
        //        case "Bullet":
        //            if (iplayerInfo?.GetMoveFSM() == PlayerState.Block)

        //                break;
        //    }
        //}


    }
}