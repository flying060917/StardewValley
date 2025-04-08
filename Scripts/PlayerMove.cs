using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed=3f;
    private Animator animator;
    private Vector2 direction;
    // Start is called before the first frame update
    void Start()
    {
        animator=GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(direction.magnitude>0.01f)
        {
            animator.SetBool("isWalking",true);
            animator.SetFloat("horizontal",direction.x);
            animator.SetFloat("vertical",direction.y); 
        }
        else
        {
            animator.SetBool("isWalking",false);
        }
        if(ToolbarUI.instance.GetSelectSlotUI()!=null&&
        ToolbarUI.instance.GetSelectSlotUI().GetData().item.type==ItemType.Hoe&&Input.GetKeyDown(KeyCode.Space))
        {
            PlantManager.instance.HoeGround(this.transform.position);
            animator.SetTrigger("hoe");
        }
    }
    private void FixedUpdate()
    {
        float h=Input.GetAxisRaw("Horizontal");
        float v=Input.GetAxisRaw("Vertical");
        direction=new Vector2(h,v).normalized;
        this.transform.Translate(direction*speed*Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag=="Pickable")
        {
            InventoryManager.instance.AddItemToBackpack(other.GetComponent<Pickable>().type);
            Destroy(other.gameObject);
        }
    }
    public void ThrowItem(GameObject prefab,int count)
    {
        for(int i=0;i<count;i++)
        {
            GameObject item=Instantiate(prefab);
            Vector2 randomDir=Random.insideUnitCircle.normalized*1.3f;
            item.transform.position=this.transform.position+new Vector3(randomDir.x,randomDir.y,0);
        }
    }
}
