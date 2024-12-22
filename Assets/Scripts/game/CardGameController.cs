using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public abstract class CardGameController : MonoBehaviour
{
    public abstract int GridRows { get; }
    public abstract int GridCols { get; }
    public abstract int[] Numbers { get; }

    public const float OffsetX = 290f;
    public const float OffsetY = 330f;

    [SerializeField] private MainCard originalCard; // 預製卡片
    [SerializeField] private Sprite[] images; // 圖片資源
    [SerializeField] private Text scoreLabel;
    [SerializeField] private Text timerLabel;

    private int _totalPairs;
    private int _matchedPairs;
    private int _score;
    private float _timer;
    private bool _isTimerRunning = false;

    public bool canReveal
    {
        get { return _secondRevealed == null; }
    }

    private MainCard _firstRevealed;
    private MainCard _secondRevealed;

    protected virtual void Start()
    {
        SetupCards();
    }

    private void SetupCards()
    {
        Vector3 startPos = originalCard.transform.position;
        Transform parent = originalCard.transform.parent;

        int[] shuffledNumbers = ShuffleArray(Numbers);

        for (int i = 0; i < GridCols; i++)
        {
            for (int j = 0; j < GridRows; j++)
            {
                MainCard card;
                if (i == 0 && j == 0)
                {
                    card = originalCard;
                }
                else
                {
                    card = Instantiate(originalCard, parent);
                }

                int index = j * GridCols + i;
                int id = shuffledNumbers[index];
                card.ChangeSprite(id, images[id]);

                float posX = (OffsetX * i) + startPos.x;
                float posY = (OffsetY * j) + startPos.y;
                card.transform.position = new Vector3(posX, posY, startPos.z);
            }
        }

        _totalPairs = GridRows * GridCols / 2;
    }

    private int[] ShuffleArray(int[] numbers)
    {
        int[] newArray = numbers.Clone() as int[];
        for (int i = 0; i < newArray.Length; i++)
        {
            int tmp = newArray[i];
            int r = Random.Range(i, newArray.Length);
            newArray[i] = newArray[r];
            newArray[r] = tmp;
        }
        return newArray;
    }

    public void CardRevealed(MainCard card)
    {
        if (!_isTimerRunning)
        {
            _isTimerRunning = true;
        }

        if (_firstRevealed == null)
        {
            _firstRevealed = card;
        }
        else
        {
            _secondRevealed = card;
            StartCoroutine(CheckMatch());
        }
    }

    private IEnumerator CheckMatch()
    {
        _score++;
        scoreLabel.text = _score.ToString();

        if (_firstRevealed.Id == _secondRevealed.Id)
        {
            _matchedPairs++;
        }
        else
        {
            yield return new WaitForSeconds(0.5f);

            _firstRevealed.Unreveal();
            _secondRevealed.Unreveal();
        }

        _firstRevealed = null;
        _secondRevealed = null;

        if (_matchedPairs == _totalPairs)
        {
            StopTimer();
        }
    }

    private void Update()
    {
        if (_isTimerRunning)
        {
            _timer += Time.deltaTime;
            timerLabel.text = _timer.ToString("F2");
        }
    }

    

    private void StopTimer()
    {
        _isTimerRunning = false;
        global.step = _score.ToString();
        global.time = _timer.ToString("F2");
        Debug.Log("Game Over! Total Time: " + _timer.ToString("F2"));

        SendToGoogle sendToGoogle = new SendToGoogle();
        StartCoroutine(sendToGoogle.Post(global.username, global.level, global.step, global.time));

        StartCoroutine(ShowResult());
    }

    private IEnumerator ShowResult()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("Result");
    }
}