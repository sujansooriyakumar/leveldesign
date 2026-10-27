using System;
using UnityEngine;

[Serializable]
public class PartyMemberData
{
    public string characterName;
    public int level;
    public int currentHP;
    public int currentMP;

    public PlayerStats stats;

    public PartyMemberData(PartyMemberSO config_)
    {
        characterName = config_.characterName;
        level = config_.level;
        currentHP = config_.currentHP;
        currentMP = config_.currentMP;
        stats = config_.stats;
    }
}
