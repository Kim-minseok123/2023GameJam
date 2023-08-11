using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager s_instance = null;
    public static GameManager Instance { get { return s_instance; } }


    public int ColdGauge = 300;

    private float elapsedTime = 0f;
    private float timeToReduce;
    int reduced = 1;
    public GameObject Player;

    //ȸ�� ��ġ ����
    public int StoveNumber = 3;
    private void Awake()
    {
        if (s_instance != null) {
            Destroy(gameObject);
        }
        s_instance = this;
    }
    public void Start()
    {
        SpawnPlayer("Oscar");
    }
    public void Update()
    {
        if (Player != null) {

            elapsedTime += Time.deltaTime;  // ��� �ð��� ����

            if (elapsedTime >= timeToReduce)
            {  // ������ ���� �ð��� �������� Ȯ��
                ColdGauge -= reduced;  // ���� ����
                elapsedTime -= timeToReduce;  // ��� �ð��� �缳�� (�̰��� ��� �ð��� 0���� �ʱ�ȭ�ϴ� �ͺ��� �����÷ο츦 ����)
            }

            if (ColdGauge == 290)
            {
                //ColdGauge = 0;
                SpawnPlayer("Hansen");
            }
            Debug.Log(ColdGauge);
        }
    }

    public void GameOver() { }
    public void GameClear() { }

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
            p.ChangeCharacter();
            Camera.main.GetComponent<CameraController>().SetCameratoPlayer(p.transform);
            Destroy(old);
        }
        else
        {
            Debug.LogError("Failed to load the prefab from Resources");
        }
    }
}
