using System.Collections;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using Core.Character;
using Core.Util;
using DG.Tweening;

namespace Core.AI
{
    public class DestoryEnemy : EnemyAction
    {
        private bool isDestoryed;
        public GameObject hazardCollider;
        public override void OnStart()
        {
            animator.SetTrigger("isDead");
            hazardCollider.SetActive(false);
        }

        public override TaskStatus OnUpdate()
        {
            return isDestoryed ? TaskStatus.Success : TaskStatus.Running;
        }
    }
}