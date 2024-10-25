using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerInput : MonoBehaviour
{
    public Rigidbody2D rb;
    public bool InputTypeIsTouch = true;
    private Vector2 ScreenDimensions;

    private void Awake()
    {
        Time.timeScale = 1f;
        rb = GetComponent<Rigidbody2D>();
        ScreenDimensions = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
    }


    void Update()
    {
        /*
        if (InputTypeIsTouch)
        {
            PlayerTouchInput();
        }
        else
        {
        }
        */

        /*
        Move();
            PlayerAccInput();
            WallTeleport();
        GoingUp();
        CameraFollowPlayer();
        */
    }

    #region WallTeleport

    private void WallTeleport()
    {
        if (Mathf.Abs(transform.position.x) > Camera.main.transform.position.x + ScreenDimensions.x)
        {
            transform.position = new Vector3(Camera.main.transform.position.x - (ScreenDimensions.x * Mathf.Sign(transform.position.x)) + 0.2f * Mathf.Sign(transform.position.x), transform.position.y, transform.position.z);
        }
    }
    #endregion

    public float Score;
    public Text scoreText;

    #region GoingUp

    public float UpSpeed;
    public float bubbleBonus;
    public void GoingUp()
    {
        UpSpeed = 1f + transform.position.y * 0.2f;
        Score = UpSpeed + bubbleBonus;
        scoreText.text = "Score: " + (int)Score;
        rb.velocity = new Vector2(rb.velocity.x, UpSpeed + bubbleBonus);
    }
    #endregion

    #region Movement
    private float DirectX;                                                      // direction of the player on x axes

    //move right and left function
    private void Move()
    {
        //get input
        //DirectX = Input.GetAxis("Horizontal");
        //move
        //rb.velocity = new Vector2(DirectX * MoveSpeed, rb.velocity.y);


        //movement with tilting the phone
        DirectX = Input.acceleration.x * MoveSpeed * Time.deltaTime;
        transform.Translate(DirectX, 0f, 0f);
    }

    //public Vector2 inputPosition;
    public void PlayerTouchInput()
    {
        //move Player with finger
        if (Input.touchCount > 0 || Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPosition = (Camera.main.ScreenToWorldPoint(touch.position));
            //inputPosition = touchPosition;

            if (touchPosition.x != transform.position.x)
            {
                //transform.position = Vector2.MoveTowards(transform.position, new Vector2(touchPosition.x, transform.position.y), MoveSpeed * Time.deltaTime);
                //transform.position = Vector2.MoveTowards(transform.position, inputPosition, MoveSpeed * Time.deltaTime);
                MoveTowardsInput(new Vector2(touchPosition.x, transform.position.y));
            }
        }
    }

    //private float DirectX;
    public void PlayerAccInput()
    {
        //movement with tilting the phone
        float DirectX = Input.acceleration.x * Time.deltaTime;

        //transform.Translate(DirectX, 0f, 0f);
        MoveTowardsInput(transform.position + new Vector3(DirectX, 0f, 0f));
    }

    public float MoveSpeed;
    public void MoveTowardsInput(Vector2 Direction)
    {
        //transform.Translate(DirectX, 0f, 0f);
        //transform.position = Vector2.MoveTowards(transform.position, transform.position+ new Vector3(DirectX,0,0) , MoveSpeed * Time.deltaTime);

        transform.position = Vector2.MoveTowards(transform.position, Direction, MoveSpeed * Time.deltaTime);
        //rb.velocity = new Vector2(Direction.x * MoveSpeed, rb.velocity.y);
    }

    #endregion

    #region CameraFollowPlayer

    public float distance;
    public void CameraFollowPlayer()
    {
        if (transform.position.y - Camera.main.transform.position.y > -distance)
            Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, transform.position.y + distance, Camera.main.transform.position.z);
    }

    #endregion

    #region Rotation



    #endregion 

    #region Collisions
    float savedMoveSpeed;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bubble"))
        {
            bubbleBonus += 0.02f;
            Destroy(collision.gameObject);
        }
        else if (collision.CompareTag("Trap"))
        {
            Time.timeScale = 0f;
        }
        else if (collision.CompareTag("Cloud"))
        {
            Debug.Log("Cloud enter");
            savedMoveSpeed = MoveSpeed;
            MoveSpeed = MoveSpeed * 0.6f;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Cloud"))
        {
            Debug.Log("Cloud exit");
            MoveSpeed = savedMoveSpeed;

        }
    }

    #endregion

    //public GameObject replayButton;
    public void Replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
