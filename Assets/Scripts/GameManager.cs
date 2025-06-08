using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class GameManager : MonoSingleton<GameManager>
{
    public TextMeshProUGUI scoreText;
    public int score;

    public List<Slot> slotList = new();

    public DragIcon dragIcon;

    protected override void Awake()
    {
        base.Awake();

        score = 0;
        GameStart();
    }

    private void Update()
    {
        scoreText.text = score.ToString();
    }

    public void GameStart()
    {
        foreach (var slot in slotList)
        {
            slot.InitSlot();
        }
    }

    public void AddScore()
    {
        score++;
    }

    public void CreateSlot()
    {
        Slot randomSlot = GetRandomEmptySlot();

        if (randomSlot == null)
        {
            return;
        }

        SlotType randomType = (SlotType)Random.Range(1, 4);
        SlotData data = new SlotData(randomType, 1);

        randomSlot.SetSlot(data);
    }

    private Slot GetRandomEmptySlot()
    {
        List<Slot> emptySlots = slotList.FindAll(slot => slot.data == null || slot.data.type == SlotType.NONE);

        if (emptySlots.Count == 0)
        {
            Debug.Log("빈 슬롯이 존재하지 않습니다.");
            return null;
        }

        return emptySlots[Random.Range(0, emptySlots.Count)];
    }
}
