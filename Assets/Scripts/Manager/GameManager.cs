using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager s_instance = null;
    public static GameManager Instance { get { return s_instance; } }

    public GameObject settingsWindow; 

    public bool isPaused = false;

    public int MaxGauge;
    public int ColdGauge = 300;

    private float elapsedTime = 0f;
    private float timeToReduce;
    int reduced = 1;
    public GameObject Player;

    //회로 설치 갯수
    public int StoveNumber = 3;
    private void Awake()
    {
        if (s_instance != null) {
            Destroy(gameObject);
        }
        s_instance = this;
        MaxGauge = ColdGauge;
        DontDestroyOnLoad(gameObject);
    }

    public void Update()
    {
        if (Player != null) {

            elapsedTime += Time.deltaTime;  // 경과 시간을 누적

            if (elapsedTime >= timeToReduce)
            {  // 설정된 감소 시간이 지났는지 확인
                ColdGauge -= reduced;  // 값을 감소
                elapsedTime -= timeToReduce;  // 경과 시간을 재설정 (이것은 경과 시간을 0으로 초기화하는 것보다 오버플로우를 방지)
            }

            if (ColdGauge <=0)
            {
                ColdGauge = 0;

            }
            Debug.Log(ColdGauge);
        }
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            TogglePause();
        }
    }
    
    public void GameOver() { 
        //게임매니저 삭제해야함.
    }
    public void GameClear() {
        //게임매니저 삭제해야함.
    }

    public void SetPlayer(GameObject Player, float reduced) { 
        this.Player = Player;
        timeToReduce = 1f / reduced;
    }
    public void SpawnPlayer(string name) {
        Vector3 ts = Vector3.zero;
        GameObject old = null;
        if (Player != null) {
            ts = Player.transform.position;
            old = Player;
            Player = null;
        }
        var player = Resources.Load($"Prefabs/Player/{name}") as GameObject;
        if (player != null)
        {
            var p = Instantiate(player, ts, Quaternion.identity).GetComponent<BaseController>();
            p.name = p.name.Split("(Clone)")[0];
            p.ChangeCharacter();
            Camera.main.GetComponent<CameraController>().SetCameratoPlayer(p.transform);
            Destroy(old);
        }
        else
        {
            Debug.LogError("Failed to load the prefab from Resources");
        }
    }
    public void SpawnPlayer(Vector3 ts)
    {
        GameObject old = null;
        string name = "Amundsen";
        if (Player != null)
        {
            old = Player;
            Player = null;
            name = old.name;
        }
        var player = Resources.Load($"Prefabs/Player/{name}") as GameObject;
        if (player != null)
        {
            var p = Instantiate(player, ts, Quaternion.identity).GetComponent<BaseController>();
            p.name = p.name.Split("(Clone)")[0];
            p.ChangeCharacter();
            Camera.main.GetComponent<CameraController>().SetCameratoPlayer(p.transform);
            if (old != null)
                Destroy(old);
        }
        else
        {
            Debug.LogError("Failed to load the prefab from Resources");
        }
    }
    public void TogglePause()
    {
        if (isPaused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }

    void PauseGame()
    {
        Time.timeScale = 0f; // 게임을 일시 중지
        isPaused = true;
        settingsWindow.SetActive(true); // 캐릭터 변경 UI를 활성화
    }

    void ResumeGame()
    {
        Time.timeScale = 1f; // 게임 일시 중지 해제
        isPaused = false;
        SpawnPlayer(settingsWindow.GetComponent<SelectController>().GetSelectName());
        settingsWindow.SetActive(false); // 캐릭터 UI를 비활성화
    }
    public void Damage(int value) {
        ColdGauge -= value;
        if(ColdGauge < 0)
        { ColdGauge = 0; }
    }
}
