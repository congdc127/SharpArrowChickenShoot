using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;
using JetSystems;

[RequireComponent(typeof(SplineFollower))]
//[RequireComponent(typeof(PlayerAnimator))]
public class PlayerController : MonoBehaviour
{
    public enum PlayerState { Idle, Moving, GoingForward, Dead }
    private PlayerState playerState;

    public delegate void OnFightStarted();
    public static OnFightStarted onFightStarted;

    [Header(" Components ")]
    private SplineFollower splineFollower;
    //private PlayerAnimator playerAnimator;

    [Header(" Control ")]
    [SerializeField] private float maxXOffset;
    [SerializeField] private float moveSpeedCoef;
    Vector3 slidePressedPos;
    Vector3 slideReleasedPos;
    float pressedXOffset;

    private void Awake()
    {
        splineFollower = GetComponent<SplineFollower>();
        splineFollower.follow = false;

        //playerAnimator = GetComponent<PlayerAnimator>();

        UIManager.onGameSet += StartMoving;
        LevelManager.onLevelInstantiated += ConfigureSplineFollower;
    }

    private void OnDestroy()
    {
        UIManager.onGameSet -= StartMoving;
        LevelManager.onLevelInstantiated -= ConfigureSplineFollower;

    }

    private void ConfigureSplineFollower()
    {
        splineFollower.spline = FindObjectOfType<SplineComputer>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            StartMoving();

        if (UIManager.IsGame())
            UIManager.updateProgressBarDelegate?.Invoke((float)splineFollower.result.percent);

        ManagePlayerState();
    }

    private void ManagePlayerState()
    {
        switch (playerState)
        {
            case PlayerState.Idle:
                break;

            case PlayerState.Moving:
                ManageSlideControl();
                break;

            case PlayerState.GoingForward:
                GoForward();
                break;

            case PlayerState.Dead:
                break;

            default:
                break;
        }
    }

    private void StartMoving()
    {
        playerState = PlayerState.Moving;
        splineFollower.follow = true;

        //playerAnimator.Run();
    }

    public void StartGoingForward()
    {
        playerState = PlayerState.GoingForward;

        splineFollower.motion.offset = new Vector2(0, splineFollower.motion.offset.y);

        LeanTween.delayedCall(Time.deltaTime * 2, () => splineFollower.follow = false);

        //UIManager.setFightDelegate?.Invoke();

        //playerAnimator.Idle();

        //onFightStarted?.Invoke();
    }

    private void ManageSlideControl()
    {
        // On click down, save the 2 positions
        if (Input.GetMouseButtonDown(0))
        {
            slidePressedPos = GetCorrectedMousePosition();
            slideReleasedPos = GetCorrectedMousePosition();

            pressedXOffset = splineFollower.motion.offset.x;
        }
        else if (Input.GetMouseButton(0))
        {
            slideReleasedPos = GetCorrectedMousePosition();

            float moveMagnitude = slideReleasedPos.x - slidePressedPos.x;

            //Vector3 pos = transform.position;
            Vector2 offset = splineFollower.motion.offset;

            float targetPos = pressedXOffset + (moveMagnitude * moveSpeedCoef / (float)Screen.width);
            offset.x = Mathf.Lerp(offset.x, targetPos, 0.2f);
            offset.x = Mathf.Clamp(offset.x, -maxXOffset, maxXOffset);
            splineFollower.motion.offset = offset;

        }
    }

    private Vector3 GetCorrectedMousePosition()
    {
        Vector3 actualMousePos = new Vector3(Input.mousePosition.x - Screen.width / 2,
            Input.mousePosition.y - Screen.height / 2,
            Input.mousePosition.z);
        return actualMousePos;
    }

    private void GoForward()
    {
        transform.position += transform.forward * Time.deltaTime * splineFollower.followSpeed * 2.5f;
    }

    private void AttackEnemy()
    {
        //playerAnimator.Attack();
        //FindObjectOfType<FinalBoss>().TakeDamage(20);
    }

    public bool IsMoving()
    {
        return playerState == PlayerState.Moving;
    }

    public PlayerState GetPlayerState()
    {
        return playerState;
    }

    public void Die()
    {
        //playerAnimator.Die();
        splineFollower.follow = false;

        UIManager.setGameoverDelegate?.Invoke();
    }

    public void SetGameover()
    {
        Die();
    }

    public void SetLevelComplete()
    {
        splineFollower.follow = false;
        playerState = PlayerState.Idle;
        UIManager.setLevelCompleteDelegate?.Invoke();
    }

    public void EndOfSplineReachedCallback()
    {
        StartGoingForward();
    }

    public bool IsPoppingBalloons()
    {
        return playerState == PlayerState.GoingForward;
    }
}
