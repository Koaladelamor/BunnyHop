using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatDisplayMovement : MonoBehaviour
{

    [SerializeField] private float beatSpeed;
    [SerializeField] private Vector3 beatVelocity;
    //[SerializeField] private bool onBeat;
    //private Conductor _conductor;
    private RectTransform _destroyBeatTransform;

    private void Awake()
    {
        //_conductor = GameObject.FindGameObjectWithTag("Conductor").GetComponent<Conductor>();
        _destroyBeatTransform = GameObject.FindGameObjectWithTag("DestroyBeat").GetComponent<RectTransform>();
    }
    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        this.transform.localPosition += beatVelocity * Time.deltaTime * beatSpeed;
        if (this.transform.position.x > _destroyBeatTransform.position.x) {
            Destroy(this.gameObject);
            //Debug.Log("RIP Beat");
        }
    }

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "DestroyBeat")
        {
            Destroy(this.gameObject);
        }
        else if (collision.name == "Hit") 
        {
            //onBeat = true;
            _conductor.SetBeatOnHit(true);
        }
    }*/

    /*private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "Hit")
        {
            //onBeat = false;
            _conductor.SetBeatOnHit(false);
        }
    }*/

    public void SetBeatSpeed(float speed) { beatSpeed = speed; }

    public void SetBeatVelocity(Vector3 velocity) { beatVelocity = velocity; }

}
