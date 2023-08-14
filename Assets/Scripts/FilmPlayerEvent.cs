using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FilmPlayerEvent : MonoBehaviour
{
    public string NextScene;
    public AudioSource AudioSource;
    public AudioClip Camera;
    public GameObject Images;
    public GameObject Concept;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) {
            // 필름 사진 찍기.
            // 3초간 보여주고 자동으로 다음 스테이지로
            Destroy(collision.gameObject);
            StartCoroutine(ShowImage());
        }
    }
    IEnumerator ShowImage() {
        Images.SetActive(true);
        AudioSource.PlayOneShot(Camera);
        Image bgimage = Images.GetComponent<Image>();
        Image concept = Concept.GetComponent<Image>();
        float Fadealpha = 0f;
        while (Fadealpha < 1f) {
            Fadealpha += 0.01f;
            yield return new WaitForSeconds(0.01f);
            bgimage.color = new Color(1, 1, 1, Fadealpha);
            concept.color = new Color(1, 1, 1, Fadealpha);
        }
        yield return new WaitForSeconds(2f);
        SceneLoader.Instance.SwitchScene(NextScene);
    }
}
