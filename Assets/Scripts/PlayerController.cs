using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speedX = 5f;

    [SerializeField] private Transform playerModelTransform;

    private float _horizontal = 0f;

    private Rigidbody2D _rb;
    
    private bool _isGround;
    
    private bool _isJump;
    
    private bool _isFacingRight = true;

    private bool _isFinished = false;

    private bool _nearLevelArm = false;

    private const float SpeedXMultiplier = 50f;

    private const string GroundTag = "Ground";
    private const string FinishTag = "Finish";
    private const string LevelArmTag = "LevelArm";

    private int _speedAnimID;

    private Animator _animator;

    private FinishScript _finishObj;

    private LevelArm _levelArm;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponentInChildren<Animator>();
        AssignAnimationsIDs();
        
        // bad code
        _finishObj = GameObject.FindGameObjectWithTag(FinishTag).GetComponent<FinishScript>();
        _levelArm = FindObjectOfType<LevelArm>();
    }

    private void Update()
    {
        _horizontal = Input.GetAxis("Horizontal");
        
        if (Input.GetKeyDown(KeyCode.W) && _isGround) _isJump = true;
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (_isFinished) _finishObj.FinishLevel();
            if (_nearLevelArm) _levelArm.ActivateLevelArm();
        }
        
        _animator.SetFloat(_speedAnimID, Mathf.Abs(_horizontal));
    }

    private void FixedUpdate()
    {
        _rb.velocity = new Vector2(_horizontal * speedX * SpeedXMultiplier * Time.fixedDeltaTime, _rb.velocity.y);

        if (_isJump)
        {
            _rb.AddForce(new Vector2(0f, 500f));
            _isGround = false;
            _isJump = false;
        }

        if (_horizontal > 0 && !_isFacingRight) Flip();
        if (_horizontal < 0 && _isFacingRight) Flip();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag(GroundTag)) _isGround = true;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag(FinishTag)) _isFinished = true;
        if (col.gameObject.CompareTag(LevelArmTag)) _nearLevelArm = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(FinishTag)) _isFinished = false;
        if (other.gameObject.CompareTag(LevelArmTag)) _nearLevelArm = false;
    }

    private void Flip()
    {
        _isFacingRight = !_isFacingRight;
        var playerScaleCurrent = playerModelTransform.localScale;
        playerScaleCurrent.x *= -1;
        playerModelTransform.localScale = playerScaleCurrent;
    }

    private void AssignAnimationsIDs()
    {
        _speedAnimID = Animator.StringToHash("speedX");
    }
}
