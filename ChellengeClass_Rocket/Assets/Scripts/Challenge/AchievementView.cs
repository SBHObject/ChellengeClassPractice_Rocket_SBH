using System;
using System.Collections.Generic;
using UnityEngine;

public class AchievementView : MonoBehaviour
{
    [SerializeField] private GameObject achievementSlotPrefab;  // 업적 슬롯 프리팹
    private Dictionary<int, AchievementSlot> achievementSlots = new();

    [SerializeField]
    private Transform slotParent;

    public void CreateAchievementSlots(AchievementSO[] achievements)
    {
        // achievement 데이터에 따라 슬롯을 생성함
        for(int i = 0; i < achievements.Length; i++)
        {
            GameObject newAchievement = Instantiate(achievementSlotPrefab, slotParent);
            achievementSlots[i] = newAchievement.GetComponent<AchievementSlot>();
            achievementSlots[i].Init(achievements[i]);
        }

    }

    public void UnlockAchievement(int threshold)
    {
        // UI 반영 로직
    }
}