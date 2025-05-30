using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
/// <summary>
/// ����noteԤ������ ����� �ƶ� �ж� ����
/// �ж�Ϊperfect��ʱ��㲥֪ͨ����ű�
/// </summary>
public class NotesMove : MonoBehaviour
{
    private KeyCode arrow;
    [SerializeField] private bool Miss = true;
    public Transform start;
    public Transform end;
    public float Speed = 5f;     //�ٶ��еȼ�����
    public bool OnTop = false;
    void Start()
    {
        getArrow();
        start = transform.parent.GetChild(0);
        end = transform.parent.GetChild(1);
        transform.position = start.position;
        StartCoroutine(check());
    }


    void Update()
    {
        if (transform.position.x - end.position.x <= 2.5f) 
        { 
            OnTop = true;
        }   
        transform.position = Vector2.MoveTowards(transform.position, end.position,Speed*Time.deltaTime);

        //�ж���Χ1f��
        if(Mathf.Abs(transform.position.x-end.position.x)<=1f )  Miss = false;
        else Miss = true;

        if(transform.position.x - end.position.x<=0.01f)//�뿪�ж�����ʱ���ٸ�����
        {
            //Debug.Log("note to end");
            GameObject.Find("miss").transform.position = transform.position;
            GameObject.Find("miss").GetComponent<Animator>().SetTrigger("play");
            Destroy(gameObject);
            
        }
    }
    private void getArrow()
    {
        if (GetComponent<SpriteRenderer>().sprite.name == "read_ele_0") arrow = KeyCode.LeftArrow;
        else if (GetComponent<SpriteRenderer>().sprite.name == "read_ele_1") arrow = KeyCode.RightArrow;
        else if (GetComponent<SpriteRenderer>().sprite.name == "read_ele_2") arrow = KeyCode.Space;
        else if (GetComponent<SpriteRenderer>().sprite.name == "read_ele_3") arrow = KeyCode.UpArrow;
        else arrow = KeyCode.DownArrow;
    }
    //�ж�
    private IEnumerator check()
    {

        while (true)
        {

            if(OnTop)
            {            
                //Debug.Log("check on");
            if (Input.GetKeyDown(arrow) && !Miss)
            {
                //Debug.Log("perfect");

                    GameObject.Find("meow").transform.position = transform.position;
                    GameObject.Find("meow").GetComponent<Animator>().SetTrigger("play");
                    GameObject.Find("meow").GetComponent<AudioSource>().Play();
                    //�㲥�ۼ�perfect����������
                    RoundSummarize.instance.PerfectHit.Invoke();
                Destroy(gameObject);

            }
            else if(Input.GetKeyDown(arrow) && Miss)
            {
                //Debug.Log("miss");
                    GameObject.Find("miss").transform.position = transform.position;
                    GameObject.Find("miss").GetComponent<Animator>().SetTrigger("play");
                    Destroy(gameObject);
                }

            }
        yield return null;
        }

    }
}
