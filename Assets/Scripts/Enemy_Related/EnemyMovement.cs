using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy_Related
{
    [Serializable]
    public class SlowOnHitTimeDetails
    {
        public SlowOnHitEffect effect;
        public float startTime;

        public SlowOnHitTimeDetails(SlowOnHitEffect effect, float startTime)
        {
            this.effect = effect;
            this.startTime = startTime;
        }
    }
    public class EnemyMovement : EnemyAbstract
    {
        protected Transform _enemyRoot;
        protected Animator _anim;

        [SerializeField] private float moveSpeed = 10f;
        private float curMoveSpeed;
        public float GetMoveSpeed() => curMoveSpeed;

        private List<SlowOnHitTimeDetails> slowDetails = new List<SlowOnHitTimeDetails>();

        protected override void Awake()
        {
            base.Awake();
            this._anim = this.enemyCtrl.Model.GetComponent<Animator>();
        }

        protected override void ResetValues()
        {
            base.ResetValues();
            this.curMoveSpeed = moveSpeed;
        }

        // public void SlowDown(float duration, float magnitude)
        // {
        //     StartCoroutine(StartSlowDown(duration, magnitude));
        // }

        protected virtual void Update()
        {
            float speed = 1;
            float maxMagnitude = 0;
            slowDetails.RemoveAll(details => Time.time - details.startTime > details.effect.duration);
            foreach (SlowOnHitTimeDetails details in slowDetails)
            {
                maxMagnitude = Mathf.Max(maxMagnitude, details.effect.magnitude);
            }
            speed -= maxMagnitude;
            curMoveSpeed = moveSpeed * speed;
            _anim.speed = speed;
            this.enemyCtrl.EnemyStatus.SetStatus(speed >= 1 ? EnemyState.normal : (speed > 0 ? EnemyState.slow : EnemyState.stun));
        }

        // private IEnumerator StartSlowDown(float duration, float magnitude)
        // {
        //     curMoveSpeed = moveSpeed * (1 - magnitude);
        //     _anim.speed = 1 - magnitude;
        //     yield return new WaitForSeconds(duration);
        //     curMoveSpeed = moveSpeed;
        //     _anim.speed = 1;
        // }

        protected virtual void FaceTo(FaceDirection direction)
        {
            switch (direction)
            {
                case FaceDirection.Right:
                    _enemyRoot.rotation = Quaternion.Euler(_enemyRoot.rotation.x, 0, _enemyRoot.rotation.z);
                    break;
                case FaceDirection.Left:
                    _enemyRoot.rotation = Quaternion.Euler(_enemyRoot.rotation.x, 180, _enemyRoot.rotation.z);
                    break;
                default:
                    break;
            }
        }

        public void ApplySlowEffect(SlowOnHitEffect effect)
        {
            // StopCoroutine(nameof(StartSlowDown));
            // SlowDown(effect.duration, effect.magnitude);
            slowDetails.Add(new SlowOnHitTimeDetails(effect, Time.time));
        }
    }
}
