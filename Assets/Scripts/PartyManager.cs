using System;
using System.Collections.Generic;
using UnityEngine;

public class PartyManager : MonoBehaviour
{
    public List<PartyMemberSO> partyMemberSOs;
    public Party party;
    public static PartyManager Instance; // <--- singleton design pattern

    private void Awake()
    {
        //Instance = this;
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        foreach (PartyMemberSO partyMember in partyMemberSOs)
        {
            party.AddToParty(partyMember);
        }
    }
}

[Serializable]
public class Party
{
    public List<PartyMemberData> party = new();


    public void AddToParty(PartyMemberSO partyMemberSO)
    {
        party.Add(new PartyMemberData(partyMemberSO));

    }
}
