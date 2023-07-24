using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
    NONE,
    E_GnollShaman,
    E_GnollScout,
    E_Imp,
    E_Lizard,
    E_MushroomSmall,
    M_Centaur,
    M_Ent,
    M_GnollOverseer,
    M_Wolf,
    H_Bear,
    H_Golem,
    H_LargeMushroom,
    H_Troll,
    B_ForestGuardian,
    B_LargeKnight,
}

public enum FaceDirection
{
    NONE,
    Right,
    Left,
}

public enum ProjectileType
{
    NONE,
    Arrow,
    Fireball,
    FairyMagicSpell,
    KingMagicSpell,
    ElfMagicSpell,
    Hammer,
    Axe,
    Knife,
}

[Serializable]
public enum TowerType
{
    NONE,
    Ranger,
    King,
    Fairy,
    Bishop,
    HighElf,
    Wizard,
    Assassin,
    Knight,
}

[Serializable]
public enum ExplodeEffectType
{
    NONE,
    HitEffect,
    CartoonBoomEffect,
    CircleBoomEffect,
    ExplosionEffect,
    GreatBoomEffect,
    SmallHitEffect,
}

[Serializable]
public enum SpawnEffectType
{
    NONE,
    SpawnEffect,
}

[Serializable]
public enum DeathEffectType
{
    NONE,
    BloodEffect,
}
