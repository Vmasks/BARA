using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Story : MonoBehaviour
{
    public GameObject dialogPanel1;
    public GameObject dialogPanel2;
    public Text dialog1Text;
    public Text dialog2Text;
    //储存两个人对话的内容
    private List<string> dialog1;
    private List<string> dialog2;
    //二人对话内容的索引
    private int index1;
    private int index2;
    //谁先说话，true 1 先说，false 2 先说
    private bool whoFirst;
    //当前说话的人， true 是 1，false 是 2
    private bool currentTalk;
    private bool is1End;
    private bool is2End;
    private void Awake()
    {
        dialog1 = new List<string> {"贵公司新的信息系统我们已经开发完毕了！","怎么可能？？这个系统完全符合当时提出的用户需求，这些需求可都是你们亲自提出来的。", "......(╯▔皿▔)╯"};
        dialog2 = new List<string> { "这是什么？尚且不说它能不能完成具体的业务，这界面风格和布局和我们想象的差距太大了。", "可是这样完全没法用啊，你们回去修改吧。"};
        index1 = 0;
        index2 = 0;
        whoFirst = true;
        currentTalk = whoFirst;
        is1End = false;
        is2End = false;
        //dialog1Text.text = dialog1[index1];
        //dialog2Text.text = dialog2[index2];
    }
    //返回true说明对话未结束 返回false说明对话结束
    private void Next()
    {
        if (is1End && is2End)
        {
            dialogPanel1.SetActive(false);
            dialogPanel2.SetActive(false);
            Invoke("ReStart", 5.0f);
            return;
        }
        if (currentTalk)
        {
            dialog1Text.text = dialog1[index1++];
            if (index1 >= dialog1.Count)
            {
                is1End = true;
            }
        }
        else
        {
            dialog2Text.text = dialog2[index2++];
            if (index2 >= dialog2.Count)
            {
                is2End = true;
            }
        }
        //显示 或 隐藏1的对话框
        dialogPanel1.SetActive(currentTalk);
        //显示 或 隐藏2的对话框
        dialogPanel2.SetActive(!currentTalk);

        //交替说话
        currentTalk = !currentTalk;
        return;
    }
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Next", 4.0f, 8.0f);
    }
    private void ReStart()
    {
        index1 = 0;
        index2 = 0;
        currentTalk = whoFirst;
        is1End = false;
        is2End = false;
    }


}
