// using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Wave_Related
{
    public class PathManager : MyMonoBehaviour
    {
        private static PathManager _instance;
        public static PathManager Instance => _instance;
        public List<Path> paths;

        protected override void Awake()
        {
            base.Awake();
            _instance = this;
        }

        protected override void LoadComponents()
        {
            base.LoadComponents();
            paths = GetComponentsInChildren<Path>().ToList();
        }

        public Path GetPath(int index)
        {
            if (index < 0 || index >= paths.Count) return null;
            return paths[index];
        }
    }
}
