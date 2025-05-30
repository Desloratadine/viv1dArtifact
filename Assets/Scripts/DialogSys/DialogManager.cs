using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using XNode;

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
    private void Awake()
    {
        if (instance == null) instance = new DialogManager();
        //if (NextLine == null) NextLine = new UnityEvent<Node>();

        //GetGraph.AddListener(InitContentNode);
        foreach(NodeGraph graph in graphs.NodeGraph)//初始化所有图
        {
            Debug.Log("init_" + graph.name);
            InitContentNode(graph);
        }

    }
    void Start()
    {
        EnterChapter(FindGraph("default"));
        OnPlaying();
        #region#旧的代码
        //Text = TextObject.GetComponentInChildren<TMP_Text>();
        //ReadText(Dialog);
        //ShowDialogs();
        #endregion
    }
    public void ReceiveNPC(string NPC)
    {
        foreach (NodeGraph graph in graphs.NodeGraph)
        {
            if (NPC == graph.name)
            {
                Debug.Log(NPC + "success attach to graph" + graph.name);
                NodeGraph = graph;
                EnterChapter(graph);

            }

        }
    }
    private void OnEnable()
    {

 
        //run(CurrentNode);//自动跳转到起始节点后的第一个对话节点并且播放第一条内容                                         
    }
    
    void Update()
    {
        //if (isReceiveGraph)
        //{
        //    OnPlaying();
        //    isReceiveGraph = false;
        //}
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnPlaying();
            //补充空格跳过打印功能
        }

    }
    //查找章节名字
    public NodeGraph FindGraph(string GraphName)
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
    public void EnterChapter(NodeGraph graph)//获取起始节点下的第一段对话，重置索引
    {
        CurrentNode = GetStartNode(graph);
        CurrentNode = GetNextNode(CurrentNode);
        index = -1;
        OnPlaying();
    }
    public void OnPlaying()
    {
        UpdateNode(CurrentNode);
        run(CurrentNode);
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
            OnPlaying(); // 继续播放选定分支
        }
        // 清除旧按钮
        foreach (Transform child in buttonpanel.transform) Destroy(child.gameObject);
    }
    //播放对话内容
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
            Text.text = parts[1];
            Printer.instance.StartPrintText(Text.text,Text);
            
        }

    }
    public void UpdateNode(Node node)//只当该节点是对话节点并且没有进行完时不更新节点
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
