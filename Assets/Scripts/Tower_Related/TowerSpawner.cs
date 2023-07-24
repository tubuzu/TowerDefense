using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Spawners
{
    public class TowerSpawner : Spawner
    {
        private static TowerSpawner instance;
        public static TowerSpawner Instance => instance;

        protected override void Awake()
        {
            instance = this;
        }
    }
}
