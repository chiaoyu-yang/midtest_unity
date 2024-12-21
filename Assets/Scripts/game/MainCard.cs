using System.Collections;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class MainCard : MonoBehaviour
{
    private bool flipped = false; // 記錄卡片是否翻面

    // [SerializeField] private SceneController controller;
    [SerializeField] private CardGameController controller; // 基底控制器
    [SerializeField] private Button Card_Back; // 卡片背面按鈕
    [SerializeField] private Button Card_front; // 卡片正面按鈕

    private int _id; // 卡片的唯一 ID

    public int Id
    {
        get { return _id; }
    }

    private void Start()
    {
        Flip();

        // 綁定翻面的事件
        Card_Back.onClick.AddListener(() =>
        {
            if (controller.canReveal)
            {
                Flip();
                controller.CardRevealed(this);
            }
        });
    }

    private void Flip()
    {
        flipped = !flipped;
        transform.DORotate(new Vector3(0, flipped ? 0f : 180f, 0), 0.25f, RotateMode.FastBeyond360);
    }

    public void ChangeSprite(int id, Sprite image)
    {
        _id = id;

        // 修改背面圖片，確保顯示正確的圖案
        Card_front.GetComponent<Image>().sprite = image;
    }

    public void Unreveal()
    {
        Flip();
    }
} 