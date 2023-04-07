using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using Core.Character;
using Core.Combat;
using DG.Tweening;

namespace Core.AI
{
    public class Patrol : EnemyAction
    {
        public int RangeX;
        public int RangeY;
        
        public string animationTriggerName;
        
        public float buildupTime;
        public float PatrolTime;
        //是否远程怪
        public bool isLong_Distance = true;

        private bool hasLanded;
        private int OffsetX;
        private int OffsetY;
        private Tween buildupTween;
        private Tween jumpTween;
        private float baseScaleX;

        public override void OnAwake()
        {
            base.OnAwake();
            
            baseScaleX = transform.localScale.x;
        }

        public override void OnStart()
        {
            OffsetX = Random.Range(-RangeX, RangeX);
            OffsetY = Random.Range(-RangeY, RangeY);
            //Debug.Log(OffsetX);
            buildupTween = DOVirtual.DelayedCall(buildupTime, StartPatrol, false);              
        }

        private void StartPatrol()
        {
            
            //animator.SetBool(animationTriggerName, true);
            //transform.DOMoveX(OffsetX, 7f);      
            jumpTween = DOVirtual.DelayedCall(PatrolTime, () =>
            {
                //transform.Translate(new Vector2(5f * Time.deltaTime, 0));
                if(isLong_Distance)
                {
                    transform.DOMove(new Vector2(OffsetX, OffsetY), 5f, false);
                    //body.velocity = new Vector2(OffsetX, OffsetY);
                }
                else
                {
                    body.velocity = new Vector2(OffsetX, 0);
                }
                animator.SetTrigger(animationTriggerName);
                hasLanded = true;
            }, false);
        }

        public override TaskStatus OnUpdate()
        {
            var scale = transform.localScale;
            scale.x = OffsetX > 0 ? -baseScaleX : baseScaleX;
            transform.localScale = scale;
            return hasLanded ? TaskStatus.Success : TaskStatus.Running;
        }

        public override void OnEnd()
        {
            buildupTween?.Kill();
            jumpTween?.Kill();
            hasLanded = false;
        }
    }
}