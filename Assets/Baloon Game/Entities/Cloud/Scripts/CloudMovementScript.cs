using UnityEngine;

public class CloudMovementScript : MonoBehaviour
{
    public int random;
    public float speed = 1f;
    private Vector2 ScreenDimensions;
    public float maxDistanceFromScrren = 5f;
    void Start()
    {
        ScreenDimensions = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

        random = Random.Range(0, 2);
        if (random == 0)
        {
            random = -1;
            flip();
        }
        transform.localScale *= Random.Range(2f,5f);
        transform.position += new Vector3( Random.Range(0f, maxDistanceFromScrren) * -random,0f,0f); 
    }

    private void Update()
    {
        if (Mathf.Abs(transform.position.x) < 10f)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x + random, transform.position.y), speed * Time.deltaTime);
            Debug.Log("Moving cloud");
        }
        else
        {
            Destroy(this.gameObject);
        }

    }

    void flip()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;
    }
}
