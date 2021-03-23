using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(LineRenderer))]

public class laser : MonoBehaviour
{
    RaycastHit hit;
    float range = 10.0f;
    LineRenderer line;
     
     void Start()
    {
        line = GetComponent<LineRenderer>();
        line.positionCount = 2;
        line.startWidth = 0.1f;
        line.endWidth = 0.25f;
        intro.enabled = true;
    }

    void FixedUpdate()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Input.GetMouseButton(0))
        {
            if (Physics.Raycast(ray, out hit, range))
            {

                line.enabled = true;
                Vector3 lol = new Vector3((float)(transform.position.x + 0.5), (float)(transform.position.y + 0.9) , transform.position.z);
                line.SetPosition(0, lol);
                line.SetPosition(1, hit.point);
                if (hit.collider.gameObject.CompareTag("Enemy") || hit.collider.gameObject.CompareTag("Projectile"))
                {
                    Destroy(hit.collider.gameObject);
                }
            } else 
            { 
                line.enabled = false;
            }
        } else
        {
            line.enabled = false;
        }

        if (upgrade.enabled && (Time.time >= timeWhenDisappear))
        {
            upgrade.enabled = false;
        }
        if (death.enabled && (Time.time >= timeWhenDisappear))
        {
            death.enabled = false;
        }
        if (win.enabled && (Time.time >= timeWhenDisappear))
        {
            win.enabled = false;
        }
        if (Input.GetKey(KeyCode.F))
        {
            intro.enabled = false;
        }

    }

    public Camera lol;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.CompareTag("Enemy") || collision.collider.gameObject.CompareTag("Projectile"))
        {
            Debug.Log("collision");
            Destroy(this.gameObject);
            EnableText2();
            Instantiate<Camera>(lol);
        }

        if (collision.collider.gameObject.CompareTag("range"))
        {
            range *= 2;
            EnableText1();
        }

        if (collision.collider.gameObject.CompareTag("win"))
        {
            EnableText3();
            Destroy(this.gameObject);
            Instantiate<Camera>(lol);
        }
    }

    public Text upgrade;  //Add reference to UI Text here via the inspector
    public Text death;
    public Text win;
    public Text intro;
    private float timeToAppear = 2f;
    private float timeWhenDisappear;

    //Call to enable the text, which also sets the timer
    public void EnableText1()
    {
        upgrade.enabled = true;
        
        timeWhenDisappear = Time.time + timeToAppear;
    }

    public void EnableText2()
    {
        death.enabled = true;
        upgrade.enabled = false;
        intro.enabled = false;
        timeWhenDisappear = Time.time + timeToAppear;
    }

    public void EnableText3()
    {
        win.enabled = true;
        timeWhenDisappear = Time.time + timeToAppear;
    }

    //We check every frame if the timer has expired and the text should disappear
    
}