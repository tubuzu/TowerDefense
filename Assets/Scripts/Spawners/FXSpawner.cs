using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Spawners
{
    public class FXSpawner : Spawner
    {
        private static FXSpawner instance;
        public static FXSpawner Instance => instance;
        public static string SpawnEffect = "SpawnEffect";
        public static string BloodEffect = "BloodEffect";
        public static string HitEffect = "HitEffect";
        protected override void Awake()
        {
            base.Awake();
            if (FXSpawner.Instance != null) Debug.LogError("Only 1 FXSpawner allow to exist!");
            FXSpawner.instance = this;
        }
    }
}
