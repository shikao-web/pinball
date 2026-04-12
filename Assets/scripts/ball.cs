using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ball : MonoBehaviour
{
    public GameManager gameManager;
    public int heart = 10;
    private int addForceCount = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.position.y < -7)
        {
            heart -= 1;
            if (heart == 0)
            {
                Destroy(this.gameObject);
                gameManager.GameOver();
            }
            else
            {
                transform.position = new Vector3(-2, 0, 0.3f);
                transform.rotation = Quaternion.Euler(0, 0, 0); 
                Rigidbody rb = this.GetComponent<Rigidbody>();
                rb.velocity = Vector3.zero;
                addForceCount = 0;
            }
            
        } 
    }
    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.name == "pop-left" && Input.GetKey(KeyCode.LeftArrow))
        {
            if (addForceCount == 0)
            {
                Rigidbody rb = this.GetComponent<Rigidbody>();
                Vector3 force = new Vector3(0.056f, 1.78f, 0f);
                rb.AddForce(force, ForceMode.Impulse);
            }
            addForceCount += 1;
            if (addForceCount >= 3)
            {
                addForceCount = 0;
            }
        }
        if (collision.gameObject.name == "pop-right" && Input.GetKey(KeyCode.RightArrow))
        {
            if (addForceCount == 0)
            {
                Rigidbody rb = this.GetComponent<Rigidbody>();
                Vector3 force = new Vector3(-0.056f, 1.78f, 0f);
                rb.AddForce(force, ForceMode.Impulse);
            }
            addForceCount += 1;
            if (addForceCount >= 3)
            {
                addForceCount = 0;
            }
        }
    }
}
