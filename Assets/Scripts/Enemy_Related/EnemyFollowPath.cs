// using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wave_Related;
using Managers;

namespace Enemy_Related
{
    public class EnemyFollowPath : EnemyMovement
    {
        private List<WayPoint> _wayPoints;
        private WayPoint _target;
        private int _pathIndex;
        private int _wpIndex;

        protected override void Awake()
        {
            base.Awake();
            this._enemyRoot = transform.parent;
        }

        protected override void Update()
        {
            base.Update();
            MoveToTarget();
        }

        public void SetPath(int index)
        {
            this._pathIndex = index;
            _wpIndex = 0;
            _wayPoints = PathManager.Instance.GetPath(_pathIndex).waypoints;
            _target = _wayPoints[_wpIndex]; //first waypoint in the list
        }

        private void MoveToTarget()
        {
            if (_wpIndex < _wayPoints.Count)
            {
                //move towards the point
                _target = _wayPoints[_wpIndex];
                _enemyRoot.position = Vector2.MoveTowards(_enemyRoot.position, _target.transform.position,
                    this.GetMoveSpeed() * Time.deltaTime);
                CheckDistanceToNextPoint();
            }
            else
            {
                GameManager.Instance.TakeDamage(this.enemyCtrl.EnemyDamageSender.GetDamage()); //player health goes down
                this.enemyCtrl.EnemyStatus.ReachedEndPath();
            }
        }

        private void CheckDistanceToNextPoint()
        {
            if (Vector2.Distance(_enemyRoot.position, _target.transform.position) <= 0.2f)
            {
                _wpIndex++;
                if (_wpIndex >= _wayPoints.Count) return;
                this.FaceTo(_wayPoints[_wpIndex].faceDirection);
            }
        }
    }
}
