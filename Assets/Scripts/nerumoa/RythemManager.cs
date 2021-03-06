using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 曲とリズムの同期を行うクラス
/// </summary>
public class RythemManager : MonoBehaviour
{
    [SerializeField] AudioClip[] BGM = default;
    [SerializeField] GameObject BGMs = default;
    [SerializeField] Player2Manager player = default;
    [SerializeField] EnemyManager enemy = default;
    [SerializeField] BlockReader block = default;

    public double totalTime = 0d;           // トータル経過時間 (sec)

    private int count = 0;                  // BGM切り替え用
    private double elaspedTime;             // 1回毎の経過時間 (sec)
    private double bufferTime;              // 緩衝時間 (タイミングの同期用)
    private double justTime;                // 時間調整用
    private double bpm170 = 120 / 170d;
    private bool cooldown = false;          // 連続実行の防止用
    private AudioSource audio_BGM;

    public double aTime;        // デバッグ用

    private void Start()
    {
        audio_BGM = BGMs.GetComponent<AudioSource>();   // BGMの管理
        audio_BGM.clip = BGM[0];
        audio_BGM.Play();

        bufferTime = bpm170 * 0.92;
    }

    private void FixedUpdate()
    {
        totalTime = audio_BGM.time;
        elaspedTime = totalTime % bpm170;

        GetRightTiming();
        block.ConvertLocal(elaspedTime);
    }


    /// <summary>
    /// 適正タイミングの算出・実行
    /// </summary>
    private void GetRightTiming()
    {
        if (elaspedTime >= bufferTime) {
            if (!cooldown) {
                justTime = bpm170 - elaspedTime;
                Invoke("JustTiming", (float)justTime);
                cooldown = true;

                aTime = totalTime + (bpm170 - elaspedTime);     // デバッグ用 (曲の経過時間)
            }
        } else if (cooldown) {
            cooldown = false;
        }
    }

    private void JustTiming()
    {
        CallAutoJump();

        if (audio_BGM.clip == BGM[0]) {
            count++;
            if (count == 4) {
                ChangeBGM();
            }
        }
    }

    private void CallAutoJump()
    {
        player.AutoJump();
        enemy.AutoJump();
    }

    private void ChangeBGM()
    {
        audio_BGM.clip = BGM[1];
        audio_BGM.Play();
    }
}