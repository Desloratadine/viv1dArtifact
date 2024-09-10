using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.Events;

public class nextscene : MonoBehaviour
{

    public string sceneToLoad; // 要加载的场景名称
    [SerializeField, Header("树？")]    public bool istree = false;
    [SerializeField, Header("树的文字")] public TextMeshProUGUI text;
    [SerializeField, Header("特殊事件")] public Button specialEvent;
    [Space]
    public GameObject canvas;

    public bool treeevent;
    public Button button;
    public bool special = false;

    public GameObject bee;
    public GameObject[] fruit;

    private void Start()
    {

        button.onClick.AddListener(onclick_trees);


    }
    private void Update()
    {
        iftree();
        if(special)
        {
            specialEvent.gameObject.SetActive(true);
            button.gameObject.SetActive(false);
        }
    }

    public void onclick_trees()
    {
        text.text = gameObject.GetComponent<TreesEvent>().eventtext;
        if(gameObject.GetComponent<TreesEvent>().eventtext == gameObject.GetComponent<TreesEvent>().trees_event[15])
        {
            special = true;
        }
        else if (gameObject.GetComponent<TreesEvent>().eventtext == gameObject.GetComponent<TreesEvent>().trees_event[14]|| 
            gameObject.GetComponent<TreesEvent>().eventtext == gameObject.GetComponent<TreesEvent>().trees_event[11])
        {
            Instantiate(bee, gameObject.transform);
        }
        if (gameObject.GetComponent<TreesEvent>().eventtext == gameObject.GetComponent<TreesEvent>().trees_event[8])
        {
            Instantiate(fruit[0], gameObject.transform);
        }
        if (gameObject.GetComponent<TreesEvent>().eventtext == gameObject.GetComponent<TreesEvent>().trees_event[9])
        {
            Instantiate(fruit[1], gameObject.transform);
        }
        if (gameObject.GetComponent<TreesEvent>().eventtext == gameObject.GetComponent<TreesEvent>().trees_event[10])
        {
            Instantiate(fruit[2], gameObject.transform);
        }
    }

    //场景切换专用
    public void onclick()
    {
        SceneManager.LoadScene(sceneToLoad);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (istree)
            {
           treeevent = true;
            }
           else
           canvas.SetActive(true);
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (istree)
        {
            treeevent=false;
        }
        canvas.SetActive(false);
        }

    }
    
    //树打开ui
    private void iftree()
    {
        if(treeevent)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                 canvas.SetActive(true);
            }

        }

    }
}
