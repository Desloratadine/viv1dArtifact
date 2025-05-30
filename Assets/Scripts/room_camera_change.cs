using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 移动摄像头的坐标达到切换房间的效果
/// 碰撞箱判断在不在门前，按R互动，需要手动给目的地赋值，调整透明度让出发点淡出
/// </summary>
public class changeview : MonoBehaviour
{
    public GameObject _camera;
    public Transform[] _target;
    int _index = 0;
    Coroutine coroutine;
    private bool IsOnDoor = false;
    
   
    void Update()
    {
        if (IsOnDoor)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                if(GetComponent<AudioSource>()) GetComponent<AudioSource>().Play();
                
                _target[0].gameObject.GetComponent<SpriteRenderer>().color = new Color(_target[1].gameObject.GetComponent<SpriteRenderer>().color.r,
    _target[1].gameObject.GetComponent<SpriteRenderer>().color.g, _target[1].gameObject.GetComponent<SpriteRenderer>().color.b, 1);
                GameObject.FindGameObjectWithTag("Player").transform.position = _target[2].transform.position;
                ChangeRoom();

            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("reach door");
        if (collision.gameObject.CompareTag("Player"))
        {
            IsOnDoor = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            IsOnDoor = false;
        }
    }
    public void ChangeRoom()
    {

        
        //Debug.Log("change room");
        Vector3 target = new Vector3(_target[0].position.x, _target[0].position.y, _camera.transform.position.z);

        {
            StartCoroutine(RoomFaded());
            StartCoroutine(count(target));

        }
        //if (_index == 0) _index++;
        //else _index--;
        //_camera.transform.position = new Vector3(15,10,_camera.transform.position.y);
    }
    private IEnumerator count(Vector3 target)
    {
        //_target[0].gameObject.SetActive(true);
        //_target[1].gameObject.SetActive(false);

        float speed = .7f;
        float timer = 0f;
        while (timer <= 1f)
        {
            timer += Time.deltaTime;
            _camera.transform.position = Vector3.Lerp(_camera.transform.position, target, timer * speed);

            if (target != _camera.transform.position)
                yield return null;
            else break;
        }

        yield return new WaitForEndOfFrame();


    }
    private IEnumerator RoomFaded()
    {
        float timer = 0f;
        float WaitTime = .5f;
        float alpha = 1f;
        while (alpha!=0)
        {
            timer += Time.deltaTime;
            alpha = (WaitTime - timer) / WaitTime;

            _target[1].gameObject.GetComponent<SpriteRenderer>().color = new Color(_target[1].gameObject.GetComponent<SpriteRenderer>().color.r,
                _target[1].gameObject.GetComponent<SpriteRenderer>().color.g, _target[1].gameObject.GetComponent<SpriteRenderer>().color.b, alpha);
                yield return null;
            if (Input.GetKeyDown(KeyCode.R)) yield break;
           
        }

        yield return new WaitForEndOfFrame();


    }
}
