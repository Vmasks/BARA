using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExhibitionController : MonoBehaviour
{
    [SerializeField]
    private List<Sprite> spriteList;
    [SerializeField]
    private Button lBtn, rBtn;
    [SerializeField]
    private Text pageNum;
    [SerializeField]
    private Image currentImg;
    private int currentPageNum, totalPageNum;

    private void Awake()
    {
        // 从一个目录里把所有图片都读过来，也可以直接拖进去，还挺方便
        totalPageNum = spriteList.Count;
        currentPageNum = 1;
        currentImg.sprite = spriteList[currentPageNum - 1];
        pageNum.text = currentPageNum + " / " + totalPageNum;

        // 左箭头 减少
        lBtn.onClick.AddListener(()=> {
            currentPageNum = currentPageNum - 1;
            if (currentPageNum < 1)
            {
                currentPageNum = totalPageNum;
            }
            currentImg.sprite = spriteList[currentPageNum - 1];
            pageNum.text = currentPageNum + " / " + totalPageNum;
        });
        // 右箭头 增加
        rBtn.onClick.AddListener(() => {
            currentPageNum = currentPageNum + 1;
            if (currentPageNum > totalPageNum)
            {
                currentPageNum = 1;
            }
            currentImg.sprite = spriteList[currentPageNum - 1];
            pageNum.text = currentPageNum + " / " + totalPageNum;
        });
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
