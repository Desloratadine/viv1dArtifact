using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using XNode;
/// <summary>
/// 在对话框，ui控件拖动赋值，选项按钮是动态生成的。
/// 进入对话有两种方法，一种是直接EnterChapterGraph，另一种是从NPC实例中获取对话图并进入对话，
/// </summary>
public class DialogManager : MonoBehaviour
{
    public GameObject optionbutton;
    public GameObject buttonpanel;

    public NodeGraph NodeGraph;
    public Node CurrentNode;

    public string[] DialogRow;//把文件里的文本分割储存到数组
    public int index;//记录对话进行到哪一行
    //public UnityEvent<Node> NextLine;
    public static DialogManager instance;

    public UnityEvent<NodeGraph> GetGraph;
    private bool isReceiveGraph;
    public AddDialogueGraph graphs;

    [SerializeField, Header("显示角色名的文本")] public TextMeshProUGUI Name;
    [SerializeField, Header("显示对话内容的文本")] public TextMeshProUGUI Text;
    [SerializeField,Header("立绘")]public Image[] CharacterImage;

    AssetBundleLoader loader;
    #region#旧的代码
    //[SerializeField, Header("头像的图片")] public SpriteRenderer CharacterImage;

    //public TMP_Text Text;
    //[SerializeField, Header("文本栏图片")] public GameObject TextObject;




    //[SerializeField,Header("next按钮")]    public Button Button;

    //public Button OptionButton;

    //[SerializeField, Header("文字栏预制体")] public GameObject DialogPrefab;//
    //public Transform DialogGroup;
    //public GameObject NewDialog = null;
    #endregion
    private void Awake()//初始化所有图
    {
        if (instance == null) instance = new DialogManager();
        //if (NextLine == null) NextLine = new UnityEvent<Node>();
        if(loader == null)
        {
            loader = new AssetBundleLoader("test");
        }
        //GetGraph.AddListener(InitContentNode);
        foreach(NodeGraph graph in graphs.NodeGraph)
        {
            Debug.Log("init_" + graph.name);
            InitContentNode(graph);
        }

    }
    void Start()//对话框显示默认章节
    {
        EnterChapterGraph(FindGraph("default"));
        //OnPlaying();
        #region#旧的代码
        //Text = TextObject.GetComponentInChildren<TMP_Text>();
        //ReadText(Dialog);
        //ShowDialogs();
        #endregion
    }

    
    void Update()
    {
            if (Printer.instance.IsOnPrinting)//这里触发转默认的时候效果有点奇怪，后期修改一下ui控件激活条件可能会好一点
            {
                return;
            }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayNode();
        }
    }
    
    public NodeGraph FindGraph(string GraphName)//查找章节名字
    {
        foreach (NodeGraph graph in graphs.NodeGraph)
        {
            if (GraphName == graph.name)
            {
                return graph;
            }
        }
        Debug.Log("未找到" + GraphName);
        return null;
    }
    public void EnterChapterGraph(NodeGraph graph)//获取起始节点下的第一段对话，重置索引
    {
        foreach (Transform child in buttonpanel.transform) Destroy(child.gameObject);
        CurrentNode = GetStartNode(graph);
        CurrentNode = GetNextNode(CurrentNode);
        index = -1;//因为要跟数组同步，先设为-1，在CheckNodeUpdate方法还要+1，变成0
        PlayNode();
    }
    public void EnrerChapterByNPC(string NPC)//先查找实例绑定的图后进入指定的对话
    {
        EnterChapterGraph(FindGraph(NPC));
        Debug.Log("success attach to graph" + NPC);
    }

    public void PlayNode()//特别莫名其妙
    {
        CheckNodeUpdate(CurrentNode);
        run(CurrentNode);
    }
     public void run(Node node)
    {

        if(node is Options option)//当前节点是选项
        {
            Debug.Log("button");
            //生成按钮，
            foreach (var choice in option.options)
            {
                GameObject button = Instantiate(optionbutton, buttonpanel.transform);
                button.GetComponentInChildren<TextMeshProUGUI>().text = choice;
                button.GetComponentInChildren<Button>().onClick.AddListener(() =>
                    OnOptionSelected(option, button.GetComponentInChildren<Button>()));
            }
            return;
        }
        if(node is Content content)//当前节点是对话节点，加载当前对话之后更新索引
        {

            //提取对话列表
            string line = content.Dialogues[index];
            string[] parts = line.Split(':');
            //string[] parts = line.Split(new[] {';'},2);
            //if (parts.Length < 2)
            //{
            //    Debug.LogError($"因为冒号引起的分割错误:{line}");
            //}
            Name.text = parts[0];
            CharacterImage[int.Parse(parts[2])].sprite = GetCharacterImg(parts[1]);
            Text.text = parts[3];
            Printer.instance.StartPrintText(Text.text,Text);
            
        }

    }
public void CheckNodeUpdate(Node node)//只当该节点是对话节点并且没有进行完时不更新节点
    {
      
        if (node is Content content)
        {
            if(index>= content.Dialogues.Count-1)
            {
                CurrentNode = GetNextNode(CurrentNode);

            }
            else if (index < content.Dialogues.Count)
            {
                index++;
            }

        }

    }
    public void InitContentNode(NodeGraph nodeGraph)
    {
        //foreach (Content content1 in nodeGraph.nodes)
        //{

        //        content1.ReadFile();

        //}
        foreach (Node node in nodeGraph.nodes)
        {
            if (node is Content content)
            {
                content.ReadFile();
            }
            else continue;
        }
    }
    public Node GetStartNode(NodeGraph nodeGraph)
    {
        foreach(Node node in nodeGraph.nodes)
        {
            if(node is StartNode)
            {
                return node;
            }
        }
            Debug.LogError("找不到起始节点");
            return null;
    }

    public Node GetNextNode(Node node)
    {
        index = 0;
        NodePort Next = node.GetOutputPort("Out");
        if (Next!=null && Next.IsConnected)
        {
            return Next.Connection.node;
        }

        Debug.Log("没有更多对话节点");
        return null;
    }
    private void OnOptionSelected(Options optionNode, Button btn)
    {
        int optionIndex = btn.transform.GetSiblingIndex();
        NodePort selectedPort = optionNode.GetOutputPort($"options {optionIndex}");//获取输出index的端口
        if (selectedPort.IsConnected)
        {
            CurrentNode = selectedPort.Connection.node;
            PlayNode(); // 继续播放选定分支
        }
        // 清除旧按钮
        foreach (Transform child in buttonpanel.transform) Destroy(child.gameObject);
    }
    //播放对话内容
   
    public Sprite GetCharacterImg(string imgTag)
    {
        AssetBundle bundle = loader.GetBundle();

        foreach (string assetName in loader.assetNames)
        {
            string fileName = System.IO.Path.GetFileNameWithoutExtension(assetName);//获取文件名不带扩展名

            //Debug.Log("Asset Name: " + assetName);
            if (imgTag == fileName)
            {
                Debug.Log("找到资源：" + name);
                Sprite sprite = bundle.LoadAsset<Sprite>(assetName);
                return sprite;
            }
        }
        Debug.Log("没有找到对应的角色图片：" + imgTag);
        return null;
    }
    
    #region#旧的分割和读取文本
    public void ReadText(Node node)   //行分割读取文本
    {
        if(node is Content ContentNode)//判断是否为对话节点
        {
           // DialogRow = ContentNode.Dialog.text.Split('\n');
        }

    }
    public void UpdateText(string name,string text)//把文本赋值到UI控件
    {
        //Name.text = name;
        //Text.text = text;
    }
    public void ShowDialogs()
    {
        //类型标识符，id，角色名称，文本内容
        for (int i = 0; i < DialogRow.Length; i++)
        {
            string[] cells = DialogRow[i].Split(',');
            if (int.TryParse(cells[1], out int id))
            {
                if (id == index && cells[0] == "#")
                {
                    UpdateText(cells[2], cells[4]);
                    index = int.Parse(cells[5]);
                    break;
                }
                else if (cells[0] == "END")
                {
                    //TextObject.SetActive(false);
                }
            }

        }
    }


    public void onClick()
    {
        //ShowDialogs();
        
        //NewDialog = null;
    }
    #endregion
}
