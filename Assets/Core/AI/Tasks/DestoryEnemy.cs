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
        private Tween DestoryTween;
        private Tween DestorystartTween;
        public override void OnStart()
        {
            animator.SetTrigger("isDead");
            hazardCollider.SetActive(false);
            if(isLong_Distance)
            body.bodyType = RigidbodyType2D.Dynamic;
            DestoryTween = DOVirtual.DelayedCall(3f, StartDestory, false);
        }

        public override TaskStatus OnUpdate()
        {
            return isDestoryed ? TaskStatus.Success : TaskStatus.Running;
        }

        private void StartDestory()
        {
            DestorystartTween = DOVirtual.DelayedCall(0f, () =>
            {
                GameObject.Destroy(this.gameObject);
            }, false);
        }
        public override void OnEnd()
        {
            DestoryTween?.Kill();
            DestorystartTween?.Kill();
        }

    }
}