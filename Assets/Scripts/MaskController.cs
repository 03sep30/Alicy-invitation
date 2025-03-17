using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskController : MonoBehaviour
{
    public Transform player; // 따라갈 플레이어
    public RectTransform maskUI; // UI 마스크 (이미지)

    void Update()
    {
        if (player == null || maskUI == null) return;

        // 플레이어의 월드 좌표를 화면 좌표로 변환
        Vector2 screenPos = Camera.main.WorldToScreenPoint(player.position);

        // UI 마스크 위치를 화면 좌표에 맞게 이동
        maskUI.position = screenPos;
    }   
}
