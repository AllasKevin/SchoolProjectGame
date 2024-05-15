using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class BuildBolt : MonoBehaviour
{
    [SerializeField] private GameObject build;
    private PlayerController player;
    private float direction;
    private Rigidbody2D rb;
    public string test;
    // Start is called before the first frame update

    void Awake()
    {
        player = GameObject.FindObjectOfType<PlayerController>();
    }
    void Start()
    {
        
        if (player.turnedRight)
        {
            UnityEngine.Debug.Log("direction  :  right");
            direction = 1;
        }
        else
        {
            UnityEngine.Debug.Log("direction  :  left");
            direction = -1;
        }
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(10*direction, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!(collision.gameObject.tag == "Enemy") && !(collision.gameObject.tag == "Collectable"))
        {
            UnityEngine.Debug.Log(collision.tag);
            UnityEngine.Debug.Log(this.gameObject.transform.position);
            Instantiate(build, this.gameObject.transform);
            UnityEngine.Debug.Log("trigger went off");
            Destroy(this);
            
        }
    }
}
