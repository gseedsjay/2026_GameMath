using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class EscapeGameManager : MonoBehaviour
{
    [Header("전투 설정")]
    public float enemyHP = 300f;
    public float playerAtk = 30f;
    public float critProb = 0.3f;

    [Header("아이템 확률 (초기값)")]
    private float probLegend = 0.05f;
    private float probEpic = 0.15f;
    private float probRare = 0.30f;
    private float probCommon = 0.50f;

    [Header("UI 연결")]
    public Text statusText;     // 현재 전투 상황 로그
    public Text hpText;         // 적 체력 표시
    public Text probText;       // 현재 전설 확률 표시
    public Text inventoryText;  // 획득 아이템 목록

    private List<string> inventory = new List<string>();

    void Start() => UpdateUI("적 등장! 탈출을 시작하세요.");

    // [공격 버튼에 연결]
    public void OnAttackButton()
    {
        if (enemyHP <= 0) return;

        // 1. 치명타 판정
        bool isCrit = Random.value < critProb;
        float damage = isCrit ? playerAtk * 2 : playerAtk;
        enemyHP -= damage;

        string log = isCrit ? $"<color=yellow>크리티컬! {damage} 데미지!</color>" : $"{damage} 데미지.";
        UpdateUI(log);

        if (enemyHP <= 0) OnEnemyDeath();
    }

    void OnEnemyDeath()
    {
        DetermineDrop();
        enemyHP = 300f; // 새로운 적 등장
        UpdateUI("<color=red>적 처치! 새로운 적이 나타났습니다.</color>");
    }

    void DetermineDrop()
    {
        float rand = Random.value;
        string result = "";

        // [과제] 가변 확률 로직을 구현하세요.
        // 힌트: 누적 확률(Cumulative Probability) 방식을 사용하면 편리합니다.
        if (rand < probLegend)
        {
            result = "전설";
            ResetProbabilities(); // 전설 획득 시 초기화
        }
        else if (rand < probLegend + probEpic)
        {
            result = "희귀";
            IncreaseLegendChance(); // 실패 시 보정
        }
        else if (rand < probLegend + probEpic + probRare)
        {
            result = "고급";
            IncreaseLegendChance();
        }
        else
        {
            result = "일반";
            IncreaseLegendChance();
        }

        inventory.Add(result);
        UpdateInventoryUI();
    }

    void IncreaseLegendChance()
    {
        // [과제] 전설 확률 +1.5%, 나머지는 각 -0.5% 감소 로직 작성
        probLegend += 0.015f;
        probEpic -= 0.005f;
        probRare -= 0.005f;
        probCommon -= 0.005f;
    }

    void ResetProbabilities()
    {
        probLegend = 0.05f;
        probEpic = 0.15f;
        probRare = 0.30f;
        probCommon = 0.50f;
    }

    // UI 갱신 함수들...
    void UpdateUI(string msg) { /* 생략 */ }
    void UpdateInventoryUI() { /* 인벤토리 리스트를 화면에 출력 */ }
}