// using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wave_Related
{
    public class Path : MyMonoBehaviour
    {
        public Transform startPoint;
        public List<WayPoint> waypoints;

        protected override void LoadComponents()
        {
            base.LoadComponents();
            SetupPath();
        }

        private void SetupPath()
        {
            waypoints = new List<WayPoint>();
            foreach (Transform point in transform)
            {
                if (point.gameObject.CompareTag("WayPoint") && point.gameObject.activeSelf == true)
                    waypoints.Add(point.GetComponent<WayPoint>());
                else if (point.gameObject.CompareTag("StartPoint") && point.gameObject.activeSelf == true)
                    startPoint = point;
            }
        }
    }
}
