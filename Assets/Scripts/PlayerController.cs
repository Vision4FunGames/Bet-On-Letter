using UnityEngine;
using Lean.Touch;
using UnityEngine.EventSystems;
using TMPro;
using System.Collections;
using System.Collections.Generic;
//using Sirenix.OdinInspector;
using Lean.Common;
using PathCreation;
using DG.Tweening;
//using Cinemachine;


public class PlayerController : MonoBehaviour
{
    GameManager gameManager;
    CameraController cameraController;
    public CameraTarget cameraTarget;
    TextMeshProUGUI tmpInfo;
    //Color colorInfoDefault = new Color(0.125f,1f,0f,1f);

    [SerializeField]
    float forwardSpeed = 3;
    public bool uiPresent;
    bool levelStarted;
    public bool failed;
    public bool levelFinished;
    Animator animator;

    public SkinnedMeshRenderer meshRenderer;
    CharacterMovement characterMovement;



    PathCreator pathCreator;
    float distanceTravelled;
    EndOfPathInstruction endOfPathInstruction = EndOfPathInstruction.Loop;

    bool moneyToSort, isEnterObstacle;
    LevelFinishHolder levelFinishHolder;//level finish variables

    //[SerializeField] CinemachineVirtualCamera vcam;
    [SerializeField] TextMeshProUGUI tmpro;
    void Awake()
    {
        cameraTarget = FindObjectOfType<CameraTarget>();

        //uiPresent = GameObject.FindGameObjectWithTag("UICamera") != null;
        characterMovement = GetComponent<CharacterMovement>();
        animator = GetComponent<Animator>();


    }


    private void StartLevel()
    {


        levelStarted = true;
        //if (uiPresent) ShowInstruction("BUILD UP!");
        //StartCoroutine(EnableFeedbacks());


    }
    private void Start()
    {
        gameManager = GameManager.GetInstance();
        gameManager.playerController = this;

        if (uiPresent)
        {
            tmpInfo = gameManager.tmpInfo;
        }
        //gameManager.vcam = vcam;

    }
    private void OnEnable()
    {
        LeanTouch.OnFingerUp += HandleFingerUp;
        LeanTouch.OnFingerDown += HandleFingerDown;
        LeanTouch.OnFingerUpdate += HandleFingerUpdate;

    }

    private void OnDisable()
    {
        LeanTouch.OnFingerUp -= HandleFingerUp;
        LeanTouch.OnFingerDown -= HandleFingerDown;
        LeanTouch.OnFingerUpdate -= HandleFingerUpdate;
    }
    public void UpdateMonetText(int totalMoney)
    {
        tmpro.text = "$" + totalMoney;

    }
    private void HandleFingerDown(LeanFinger finger)
    {
        if (!levelStarted && !failed && !levelFinished && EventSystem.current.currentSelectedGameObject == null)
        {
            StartLevel();
            gameManager.StartLevel();
        }
        print("Start Game");
    }

    private void HandleFingerUp(LeanFinger finger)
    {

    }

    private void HandleFingerUpdate(LeanFinger finger)
    {

    }

    private void Update()
    {
        if (!levelStarted) return;

        if (pathCreator != null)
        {


            distanceTravelled += 10 * Time.deltaTime;
            transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
            //transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);

        }
        else
        {
            if (GameManager.GetInstance().gameSequence == GAMESEQUENCE.GO)
            {
                if (!isEnterObstacle)
                    animator.SetInteger("Status", 1);
                transform.position += transform.forward * forwardSpeed * Time.deltaTime;
                //characterMovement.enabled = true;

            }
            else if (!gameManager.levelFinished)
            {
                animator.SetInteger("Status", 0);

            }
        }
    }





    public void EndLevel(bool finished)
    {
        if (levelFinished) return;
        levelFinished = true;
        GameManager.GetInstance().EndLevel(finished);
    }


    #region  Collisions
    private void OnTriggerEnter(Collider other)
    {
        print("other ");

    }
    #endregion

    #region Feedback System

    /*
    IEnumerator EnableFeedbacks()
    {
        yield return new WaitForSeconds(.2f);
    }

    public void ShowInstruction(string instruction, Color? color = null)
    {
        if (!uiPresent) return;
        if (tmpInfo == null) tmpInfo = gameManager.tmpInfo;
        CancelInvoke("DisableInstruction");
        if (tmpInfo == null) return;
        //tmpInfo.color = color ?? colorInfoDefault;
        tmpInfo.text = instruction;
        tmpInfo.gameObject.SetActive(true); 
        Invoke("DisableInstruction",1.2f);
    }

    private void DisableInstruction()
    {
        tmpInfo.gameObject.SetActive(false);
        //tmpInfo.color = colorInfoDefault;
    }
    */
    #endregion
}