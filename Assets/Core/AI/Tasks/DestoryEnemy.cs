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
        public bool isLong_Distance = true;
        public override void OnStart()
        {
            animator.SetTrigger("isDead");
            hazardCollider.SetActive(false);
            if(isLong_Distance)
            body.bodyType = RigidbodyType2D.Dynamic;
        }

        public override TaskStatus OnUpdate()
        {
            return isDestoryed ? TaskStatus.Success : TaskStatus.Running;
        }
    }
}