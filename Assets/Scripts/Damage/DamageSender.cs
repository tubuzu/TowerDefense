using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Damage
{
    public class DamageSender : MyMonoBehaviour
    {
        [SerializeField] private int damage;

        public int GetDamage() => damage;
        public void SetDamage(int newDamage) => damage = newDamage;
    }
}
