using System;
using UnityEngine;

[CreateAssetMenu(fileName = "PartyMemberSO", menuName = "GDEV2222/PartyMemberSO")]
public class PartyMemberSO : ScriptableObject
{
    public string characterName;
    public int level;
    public int currentHP;
    public int currentMP;

    public PlayerStats stats;
}

[Serializable]
public class PlayerStats
{
    public int maxHP;
    public int maxMP;
    public int str;
    public int def;
    public int mag;

  
}
