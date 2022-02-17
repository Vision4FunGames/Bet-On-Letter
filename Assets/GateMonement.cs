using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GateMonement : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        GateMovementFunc();
    }
    public void GateMovementFunc()
    {
        transform.DOLocalMoveX(0, 2f).OnComplete(() => transform.DOLocalMoveX(-2, 2f).OnComplete(() => GateMovementFunc() ));
    }
    // Update is called once per frame
    void Update()
    {

    }
}
