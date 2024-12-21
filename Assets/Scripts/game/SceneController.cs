using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    public const int gridRows = 2;  // 設定行數為 2
    public const int gridCols = 2;  // 設定列數為 2
    public const float offsetX = 260f;
    public const float offsetY = 260f;

    [SerializeField] private MainCard originalCard; // 預製卡片
    [SerializeField] private Sprite[] images; // 圖片資源

    private bool _isTimerRunning = false; // 記錄計時器是否啟動
    private int _totalPairs; // 總共的卡片對數
    private int _matchedPairs; // 已經匹配的卡片對數

    private void Start()
    {
        Vector3 startPos = originalCard.transform.position; // 初始位置

        int[] numbers = { 0, 0, 1, 1 }; // 這裡設定 2x2 網格，共 4 張卡片
        numbers = ShuffleArray(numbers); // 隨機排序數字

        Transform parent = originalCard.transform.parent; // 取得原始卡片的父物件

        for (int i = 0; i < gridCols; i++) // 只遍歷 2 列
        {
            for (int j = 0; j < gridRows; j++) // 只遍歷 2 行
            {
                MainCard card;
                if (i == 0 && j == 0)
                {
                    card = originalCard; // 第一張卡片使用預製卡片
                }
                else
                {
                    card = Instantiate(originalCard, parent); // 複製卡片，並設置相同父物件
                }

                int index = j * gridCols + i;
                int id = numbers[index];
                card.ChangeSprite(id, images[id]); // 設定卡片的圖案

                float posX = (offsetX * i) + startPos.x;
                float posY = (offsetY * j) + startPos.y;
                card.transform.position = new Vector3(posX, posY, startPos.z); // 設定卡片位置
            }
        }

        // 設定總的卡片對數
        _totalPairs = gridRows * gridCols / 2;
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

    private MainCard _firstRevealed;
    private MainCard _secondRevealed;
    private int _score = 0;
    [SerializeField] private Text scoreLabel;

    public bool canReveal
    {
        get { return _secondRevealed == null; }
    }

    public void CardRevealed(MainCard card)
    {
        if (!_isTimerRunning)
        {
            _isTimerRunning = true; // 開始計時
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
        scoreLabel.text = _score.ToString(); // 使用 ToString() 轉換為字串

        if (_firstRevealed.Id == _secondRevealed.Id)  // 如果兩張卡片匹配
        {
            _matchedPairs++; // 增加已匹配的卡片對數
        }
        else
        {
            yield return new WaitForSeconds(0.5f);

            _firstRevealed.Unreveal();
            _secondRevealed.Unreveal();
        }

        // 清除翻開的卡片
        _firstRevealed = null;
        _secondRevealed = null;

        // 檢查是否所有卡片都匹配完成
        if (_matchedPairs == _totalPairs)
        {
            StopTimer(); // 停止計時
        }
    }

    private float _timer = 0f; // 計時器變數
    [SerializeField] private Text timerLabel; // 顯示時間的 UI Text

    private void timeStart()
    {
        if (_isTimerRunning)
        {
            _timer += Time.deltaTime;
            timerLabel.text = _timer.ToString("F2"); // 顯示計時，保留小數點後 2 位
        }
    }

    private void StopTimer()
    {
        _isTimerRunning = false; // 停止計時
        Debug.Log("Game Over! Total Time: " + _timer.ToString("F2")); // 顯示總時間
    }

    private void Update()
    {
        if (_isTimerRunning)
        {
            timeStart(); // 更新計時器
        }
    }
}
