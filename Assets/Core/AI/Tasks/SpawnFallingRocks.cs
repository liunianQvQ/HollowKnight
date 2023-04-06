using System.Collections.Generic;
using UnityEngine;
using Core.Combat.Projectiles;
using BehaviorDesigner.Runtime.Tasks;
using Core.Character;
using DG.Tweening;

namespace Core.AI
{
    public class SpawnFallingRocks : EnemyAction
    {
        public Collider2D spawnAreaCollider;
        public AbstractProjectile rockPrefab;
        public int spawnCount = 4;
        public float spawnInterval = 0.3f;

        public override TaskStatus OnUpdate()
        {
            var sequence = DOTween.Sequence();
            for(int i = 0; i<spawnCount; i++)
            {
                sequence.AppendCallback(SpawnRock);
                sequence.AppendInterval(spawnInterval);
            }

            return TaskStatus.Success;
        }

        private void SpawnRock()
        {
            var randomX = Random.Range(spawnAreaCollider.bounds.min.x, spawnAreaCollider.bounds.max.x);
            var rock = Object.Instantiate(rockPrefab, new Vector3(randomX, spawnAreaCollider.bounds.min.y - 1f), Quaternion.identity);
            rock.SetForce(Vector2.zero);
        }

    }
}
