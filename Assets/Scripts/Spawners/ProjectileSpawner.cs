// using System.Collections;
// using System.Collections.Generic;
using UnityEngine;

namespace Spawners
{
    public class ProjectileSpawner : Spawner
    {
        private static ProjectileSpawner instance;
        public static ProjectileSpawner Instance => instance;
        protected override void Awake()
        {
            base.Awake();
            if (ProjectileSpawner.Instance != null) Debug.LogError("Only 1 ProjectileSpawner allow to exist!");
            ProjectileSpawner.instance = this;
        }
    }
}
