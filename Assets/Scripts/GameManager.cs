using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour, Subject
{
    private static GameManager _instance;
    public static GameManager Instance()
    {
        return _instance;
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            if (this != _instance)
            {
                Destroy(this.gameObject);
            }
        }
    }

    private int _gameRound = 0;
    private string _whoseTurn = "Enemy";
    private bool _isEnd = false;

    // 1. SceneUI가 GameManager 접근 할 수 있도록 캐릭터 딕셔너리 선언0
    private Dictionary<string, Character> _characterList = new Dictionary<string, Character>();

    private delegate void TurnHandler(int round, string turn);
    private TurnHandler _turnHandler;
    private delegate void FinishHandler(bool isFinish);
    private FinishHandler _finishHandler;
    // 2. UIHandler 선언 (이번에는 round, turn, isFinish 모두 받는다)0
    private delegate void UIHandler(int round, string turn, bool isFinish);
    private UIHandler _uiHandler;

    public void RoundNotify()
    {
        if (!_isEnd)
        {
            if (_whoseTurn == "Enemy")
            {
                _gameRound++;
                Debug.Log($"GameManager: Round {_gameRound}.");
            }
            TurnNotify();
        }
    }

    public void TurnNotify()
    {
        _whoseTurn = _whoseTurn == "Enemy" ? "Player" : "Enemy";
        Debug.Log($"GameManager: {_whoseTurn} turn.");
        _turnHandler(_gameRound, _whoseTurn);
        // 2. _uiHandler 호출
        _uiHandler(_gameRound, _whoseTurn, _isEnd);
    }

    public void EndNotify()
    {
        _isEnd = true;
        _finishHandler(_isEnd);
        // 2. _uiHandler 호출 0
        _uiHandler(_gameRound, _whoseTurn, _isEnd);
        Debug.Log("GameManager: The End");
        Debug.Log($"GameManager: {_whoseTurn} is Win!");
    }

    public void AddCharacter(Character character)
    {
        _turnHandler += new TurnHandler(character.TurnUpdate);
        _finishHandler += new FinishHandler(character.FinishUpdate);
        // 1. _characterList에 추가
        _characterList.Add(character._myName, character); //키값 추가 요망
    }
    // 3.
    // : SceneUI 옵저버로 등록 0
    public void AddUI(SceneUI ui)
    {
        _uiHandler += new UIHandler(ui.UIUpdate);        
    }

    /// <summary>
    /// 4. GetChracter: 넘겨 받은 name의 Character가 있다면 해당 캐릭터 반환
    /// 1) _characterList 순회하며
    /// 2) if 문과 ContainsKey(name) 이용
    /// 3) 없다면 null 반환 //Why isn't it working?
    /// </summary>
    public Character GetCharacter(string name)
    {
        if (_characterList.ContainsKey(name))
        {
            
            Character _character; //Value
            _characterList.TryGetValue(name, out _character);
            return _character;
        }
        else
        {
            return null;
        }
    }
}