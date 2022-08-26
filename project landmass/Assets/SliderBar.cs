using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderBar : MonoBehaviour
{
    public float SlideValue;
    
    // Start is called before the first frame update
    void Start()
    {
        SlideValue = transform.parent.parent.GetComponent<Creature>().thirst_state;
            
    }

    // Update is called once per frame
    void Update()
    {
        SlideValue = transform.parent.parent.GetComponent<Creature>().thirst_state;
       // transform.position = new Vector3(1 - transform.localScale.x, transform.position.y, transform.position.z);
            
        transform.localScale = new Vector3(SlideValue / 100, 1, 1);
    }
}
