using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class Hand : MonoBehaviour
{
    public int count;
    public Transform point;
    public List<GameObject> objects = new List<GameObject>();
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Stake"))
        {
            Olusturma(collision);
            Destroy(collision.gameObject);
        }
    }
    public void Olusturma(Collision collision)
    {
        count++;
        objects.Add(Instantiate(objects[0], point.position, Quaternion.identity, point.parent));
        point.position = new Vector3(point.position.x, point.position.y + 0.05f, point.position.z);
        float c = 0;
        foreach (var item in objects)
        {
           objects[0].transform.DOScale(objects[0].transform.localScale * 1.5f, 0.1f).OnComplete(() =>
            {
                objects[0].transform.DOScale(objects[0].transform.localScale / 1.5f, 0.1f);


            }).SetDelay(c * 0.1f);
            c += 0.1f;
        }
    }
}
