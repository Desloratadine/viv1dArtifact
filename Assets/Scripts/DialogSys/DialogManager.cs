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

    public string[] DialogRow;//���ļ�����ı��ָ�浽����
    public int index;//��¼�Ի����е���һ��
    //public UnityEvent<Node> NextLine;
    public static DialogManager instance;

    public UnityEvent<NodeGraph> GetGraph;
    private bool isReceiveGraph;
    public AddDialogueGraph graphs;

    [SerializeField, Header("��ʾ��ɫ�����ı�")] public TextMeshProUGUI Name;
    [SerializeField, Header("��ʾ�Ի����ݵ��ı�")] public TextMeshProUGUI Text;
    #region#�ɵĴ���
    //[SerializeField, Header("ͷ���ͼƬ")] public SpriteRenderer CharacterImage;

    //public TMP_Text Text;
    //[SerializeField, Header("�ı���ͼƬ")] public GameObject TextObject;




    //[SerializeField,Header("next��ť")]    public Button Button;

    //public Button OptionButton;

    //[SerializeField, Header("������Ԥ����")] public GameObject DialogPrefab;//
    //public Transform DialogGroup;
    //public GameObject NewDialog = null;
    #endregion
    private void Awake()
    {
        if (instance == null) instance = new DialogManager();
        //if (NextLine == null) NextLine = new UnityEvent<Node>();

        //GetGraph.AddListener(InitContentNode);
        foreach(NodeGraph graph in graphs.NodeGraph)//��ʼ������ͼ
        {
            Debug.Log("init_" + graph.name);
            InitContentNode(graph);
        }

    }
    void Start()
    {
        EnterChapter(FindGraph("default"));
        OnPlaying();
        #region#�ɵĴ���
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

 
        //run(CurrentNode);//�Զ���ת����ʼ�ڵ��ĵ�һ���Ի��ڵ㲢�Ҳ��ŵ�һ������                                         
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
            //����ո�������ӡ����
        }

    }
    //�����½�����
    public NodeGraph FindGraph(string GraphName)
    {
        foreach (NodeGraph graph in graphs.NodeGraph)
        {
            if (GraphName == graph.name)
            {
                return graph;
            }
        }
        Debug.Log("δ�ҵ�" + GraphName);
        return null;
    }
    public void EnterChapter(NodeGraph graph)//��ȡ��ʼ�ڵ��µĵ�һ�ζԻ�����������
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
            Debug.LogError("�Ҳ�����ʼ�ڵ�");
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

        Debug.Log("û�и���Ի��ڵ�");
        return null;
    }
    private void OnOptionSelected(Options optionNode, Button btn)
    {
        int optionIndex = btn.transform.GetSiblingIndex();
        NodePort selectedPort = optionNode.GetOutputPort($"options {optionIndex}");//��ȡ���index�Ķ˿�
        if (selectedPort.IsConnected)
        {
            CurrentNode = selectedPort.Connection.node;
            OnPlaying(); // ��������ѡ����֧
        }
        // ����ɰ�ť
        foreach (Transform child in buttonpanel.transform) Destroy(child.gameObject);
    }
    //���ŶԻ�����
    public void run(Node node)
    {
        if(node is Options option)//��ǰ�ڵ���ѡ��
        {
            Debug.Log("button");
            //���ɰ�ť��
            foreach (var choice in option.options)
            {
                GameObject button = Instantiate(optionbutton, buttonpanel.transform);
                button.GetComponentInChildren<TextMeshProUGUI>().text = choice;
                button.GetComponentInChildren<Button>().onClick.AddListener(() =>
                    OnOptionSelected(option, button.GetComponentInChildren<Button>()));
            }
            return;
        }
        if(node is Content content)//��ǰ�ڵ��ǶԻ��ڵ㣬���ص�ǰ�Ի�֮���������
        {

            //��ȡ�Ի��б�
            string line = content.Dialogues[index];
            string[] parts = line.Split(':');
            //string[] parts = line.Split(new[] {';'},2);
            //if (parts.Length < 2)
            //{
            //    Debug.LogError($"��Ϊð������ķָ����:{line}");
            //}
            Name.text = parts[0];
            Text.text = parts[1];
            Printer.instance.StartPrintText(Text.text,Text);
            
        }

    }
    public void UpdateNode(Node node)//ֻ���ýڵ��ǶԻ��ڵ㲢��û�н�����ʱ�����½ڵ�
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
    #region#�ɵķָ�Ͷ�ȡ�ı�
    public void ReadText(Node node)   //�зָ��ȡ�ı�
    {
        if(node is Content ContentNode)//�ж��Ƿ�Ϊ�Ի��ڵ�
        {
           // DialogRow = ContentNode.Dialog.text.Split('\n');
        }

    }
    public void UpdateText(string name,string text)//���ı���ֵ��UI�ؼ�
    {
        //Name.text = name;
        //Text.text = text;
    }
    public void ShowDialogs()
    {
        //���ͱ�ʶ����id����ɫ���ƣ��ı�����
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
