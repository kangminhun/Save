using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

enum Player_State
{
    Ga, Ba, Bo
};

public class GM : MonoBehaviour
{
    public GameObject gaBaBoGameUi;
    public GameObject game;

    Player_State _player_State;
    Player_State _enemy_State;

    public Sprite gawe;
    public Sprite bawe;
    public Sprite bo;

    public GameObject enemy;
    public GameObject result;

    public Text result_text;

    //유저의 버튼
    public GameObject player_Ga_Button;
    public GameObject player_Ba_Button;
    public GameObject player_Bo_Button;

    //승부 버튼
    public GameObject stop_Button;


    //게임의 시작 유무를 체크
    bool gameStart = false;

    //프레임마다 이미지를 바꾸기 위한 카운트
    int _cnt;

    // Use this for initialization
    void Start()
    {
        //카운트 코루틴 호출
        StartCoroutine(Count());
    }

    // Update is called once per frame
    void Update()
    {
        //게임 시작이 되면 이미지를 마구 섞어주는 효과를 준다.
        if (gameStart)
        {
            _cnt++;

            if (_cnt > 0)
            {
                enemy.GetComponent<Image>().sprite = gawe;
            }
            if (_cnt > 1)
            {
                enemy.GetComponent<Image>().sprite = bawe;
            }
            if (_cnt > 2)
            {
                enemy.GetComponent<Image>().sprite = bo;
                _cnt = 0;
            }
        }
    }

    //카운트 코루틴
    IEnumerator Count()
    {
        //시작된 이후에는, 가위바위보 선택버튼과 승부 버튼을 On 해준다.
        player_Ga_Button.gameObject.SetActive(true);
        player_Ba_Button.gameObject.SetActive(true);
        player_Bo_Button.gameObject.SetActive(true);

        stop_Button.gameObject.SetActive(true);
        stop_Button.GetComponent<Button>().onClick.AddListener(GameEndButton);
        //게임 시작을 알려줌.
        gameStart = true;
        yield return null;
    }
    //유저가 버튼을 눌러 이미지를 변경하고, 플레이어의 상태도 바꿔주는 메소드 모음
    public void PlayerButtonImgChange_Ga()
    {
        _player_State = Player_State.Ga;
    }

    public void PlayerButtonImgChange_Ba()
    {
        _player_State = Player_State.Ba;
    }

    public void PlayerButtonImgChange_Bo()
    {
        _player_State = Player_State.Bo;
    }

    //게임의 승부를 결정짓는다.
    public void GameEndButton()
    {
        //게임이 끝났으므로 다시 Start를 false로 바꿔준다.
        gameStart = false;

        //적이 어떤 것을 낼지 결정하고 보여준다.
        int _randomNum = Random.Range(0, 3);

        if (_randomNum == 0)
        {
            enemy.GetComponent<Image>().sprite = gawe;
            _enemy_State = Player_State.Ga;
        }
        else if (_randomNum == 1)
        {
            enemy.GetComponent<Image>().sprite = bawe;
            _enemy_State = Player_State.Ba;
        }
        else if (_randomNum == 2)
        {
            enemy.GetComponent<Image>().sprite = bo;
            _enemy_State = Player_State.Bo;
        }

        //플레이어와 적이 낸 것을 비교한 후, 결과 화면을 보여준다.

        //결과 화면 이미지를 On해준다.
        result.SetActive(true);

        //플레이어가 가위를 냈을때
        if (_player_State == Player_State.Ga)
        {
            if (_enemy_State == Player_State.Ga)//적이 가위를 냄
            {
                result_text.text = "Draw";
            }
            else if (_enemy_State == Player_State.Ba)//적이 바위를 냄
            {
                result_text.text = "Lose";
            }
            else if (_enemy_State == Player_State.Bo)//적이 보를 냄
            {
                result_text.text = "Win";
            }
        }

        //플레이어가 바위를 냈을때
        if (_player_State == Player_State.Ba)
        {
            if (_enemy_State == Player_State.Ga)//적이 가위를 냄
            {
                result_text.text = "Win";
            }
            else if (_enemy_State == Player_State.Ba)//적이 바위를 냄
            {
                result_text.text = "Draw";
            }
            else if (_enemy_State == Player_State.Bo)//적이 보를 냄
            {
                result_text.text = "Lose";
            }
        }


        //플레이어가 보를 냈을때
        if (_player_State == Player_State.Bo)
        {
            if (_enemy_State == Player_State.Ga)//적이 가위를 냄
            {
                result_text.text = "Lose";
            }
            else if (_enemy_State == Player_State.Ba)//적이 바위를 냄
            {
                result_text.text = "Win";
            }
            else if (_enemy_State == Player_State.Bo)//적이 보를 냄
            {
                result_text.text = "Draw";
            }
        }

        //도박 ㄱ???

        //if(result_text.text== "Win")
        //{
        //    PointScore.instance.PointUp(10000);
        //}

        //쓸데없는 버튼들과 UI는 모두 Off를 해준다
        player_Ga_Button.gameObject.SetActive(false);
        player_Ba_Button.gameObject.SetActive(false);
        player_Bo_Button.gameObject.SetActive(false);

        stop_Button.gameObject.SetActive(false);

    }
    public void restart()
    {
        player_Ga_Button.gameObject.SetActive(true);
        player_Ba_Button.gameObject.SetActive(true);
        player_Bo_Button.gameObject.SetActive(true);
        stop_Button.gameObject.SetActive(true);
        result.SetActive(false);
        _player_State = Player_State.Ga;
        gameStart = true;

        //도박 ㄱ???

        //if (PointScore.instance.scoreValue >= 100)
        //{
        //    PointScore.instance.PointDown(100);
        //    player_Ga_Button.gameObject.SetActive(true);
        //    player_Ba_Button.gameObject.SetActive(true);
        //    player_Bo_Button.gameObject.SetActive(true);
        //    stop_Button.gameObject.SetActive(true);
        //    result.SetActive(false);
        //    _player_State = Player_State.Ga;
        //    gameStart = true;
        //}
        //else
        //    return;
    }
    public void Exit()
    {
        player_Ga_Button.gameObject.SetActive(true);
        player_Ba_Button.gameObject.SetActive(true);
        player_Bo_Button.gameObject.SetActive(true);
        stop_Button.gameObject.SetActive(true);
        result.SetActive(false);
        gameStart = true;
        _player_State = Player_State.Ga;
        gaBaBoGameUi.SetActive(true);
        game.SetActive(false);
        gaBaBoGameUi.GetComponent<GaBaBoGame>().npclookat.cameraMove = true;
    }
}