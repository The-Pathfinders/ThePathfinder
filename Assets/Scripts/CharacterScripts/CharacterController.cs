using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterController : MonoBehaviour
{
    public float fallMultiplier = 2.5f;

    public float lowJumpMultiplier = 2f;

    public float jumpVelocity;

    public float MovementSpeed = 5;

    Rigidbody2D rb;

    public bool isGrounded;

    private bool canJump;

    public Transform groundCheck;

    public float groundCheckRadius;

    public LayerMask whatIsGround;

    private Inventory inventory;

    [SerializeField] private UI_Inventory uiInventory;

    private bool isColliding = false;
    
    private ItemWorld itemWorld = null;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        inventory = new Inventory();

        uiInventory.SetInventory(inventory);

        ItemWorld.SpawnItemWorld(new Vector3(3, -2), new Item { itemType = Item.ItemType.Sphere, amount = 1 });
        ItemWorld.SpawnItemWorld(new Vector3(-3, -2), new Item { itemType = Item.ItemType.Square, amount = 1 });
        ItemWorld.SpawnItemWorld(new Vector3(1, -2), new Item { itemType = Item.ItemType.Rectangle, amount = 1 });    
    }
    
    private void OnTriggerEnter2D(Collider2D collider)
    {
        itemWorld = collider.GetComponent<ItemWorld>();

        isColliding = true;
    }

    private void OnTriggerExit2D(Collider2D collider){
        isColliding = false;
    }
    
    void Update()
    {
        var Movement = Input.GetAxisRaw("Horizontal");

        transform.position += new Vector3(Movement, 0, 0) * Time.deltaTime * MovementSpeed;

        CheckSurroundings();

        CanJump();

        if(Input.GetButton("Jump") && canJump)
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.up * jumpVelocity;
        }

        if(rb.velocity.y < 0 && canJump)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        } else if(rb.velocity.y > 0 && !Input.GetButton("Jump") && !canJump)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }

        if(isColliding) getItem();
    }

    void CheckSurroundings()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
    }

    private void CanJump()
    {
        if(isGrounded && rb.velocity.y <= 0)
        {
            canJump = true;
        }
        else
        {
            canJump = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }

    private void getItem(){
        if(itemWorld != null)
        {
            inventory.AddItem(itemWorld.GetItem());

            itemWorld.DestroySelf();

            isColliding = false;
        }
    }

    public void SavePlayer()
    {
        Saves.SavePlayer(this, inventory);
    }

    public void LoadPlayer()
    {
        PlayerData data = Saves.LoadPlayer();

        Vector3 position;

        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];

        transform.position = position;

        inventory.itemList = data.itemList;
    }
}
