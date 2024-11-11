using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public UIManager MyUIManager;

    public GameObject Character;
    public GameObject CamObj;
    
    const float CharacterSpeed = 3f;

    public int NowScore = 0;

    void Awake()
    {
        MyUIManager.DisplayScore(NowScore);
        MyUIManager.DisplayMessage("", 0);
    }
    
    // Update is called once per frame
    void Update()
    {
        MoveCharacter();
    }

    // For smooth cam moving, it's good to use LateUpdate.
    void LateUpdate()
    {
        MoveCam();
    }

    void MoveCam()
    {
        // CamObj는 Character의 x, y position을 따라간다.
        // ---------- TODO ---------- 
        if (CamObj && Character) // CamObj와 Character가 존재하면
        {
            CamObj.transform.position = new Vector3(Character.transform.position.x, Character.transform.position.y, CamObj.transform.position.z);
        }
        // -------------------- 
    }

    void MoveCharacter()
    {
        // Character는 초당 CharacterSpeed의 속도로 우측으로 움직인다.
        // ---------- TODO ---------- 
        if (Character) // Character가 존재하면
        {
            //Time.deltaTime 안 곱하면 초당 이동이 아님
            Character.transform.position += new Vector3(CharacterSpeed, 0, 0) * Time.deltaTime;
        }
        // -------------------- 
    }

    public void GameOver()
    {
        // Character를 삭제하고, "Game Over!"라는 메시지를 3초간 띄우고, RestartButton을 활성화한다.
        // ---------- TODO ---------- 
        if (Character) // Character가 존재하면
        {
            Destroy(Character);
            MyUIManager.DisplayMessage("Game Over!", 3);//parameter: message, duration
            MyUIManager.RestartButton.SetActive(true);
        }
        // -------------------- 
    }

    public void GetPoint(int point)
    {
        // point만큼 점수를 증가시키고 UI에 표시한다.
        // ---------- TODO ---------- 
        NowScore += point;
        MyUIManager.DisplayScore(NowScore);
        // -------------------- 
    }

    // Restart the game
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}