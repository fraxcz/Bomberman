using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;
using UnityEngine.U2D.Animation;

public class PlayerScript : MonoBehaviour
{
    Vector2 dir;
    private Animator Animator;
    public int PlayerID;
    [SerializeField] GameObject Bomb;
    [SerializeField] TMP_Text NameHolder;
    [SerializeField] SpriteLibrary spriteLibrary;
    private SpriteRenderer spriteRenderer;
    [SerializeField] SpriteLibraryAsset[] spriteLibraryAsset;
    Rigidbody2D RB;
    internal PlayerInput _playerInput;
    internal int CountBombPlaced;
    int MaxBombPlaced;
    internal Tilemap _destructible;
    bool SpaceHold;
    SpriteRenderer SpriteRender;


    void Start()
    {
        _playerInput = GetComponent<PlayerInput>();
        RB = GetComponent<Rigidbody2D>();
        _destructible = GetComponent<Tilemap>();
        Animator = GetComponent<Animator>();
        SpriteRender = GetComponent<SpriteRenderer>();
        spriteLibrary = GetComponent<SpriteLibrary>();
        _playerInput.SwitchCurrentActionMap("Player" + PlayerID);
        NameHolder.text = "Player " + PlayerID;
        MaxBombPlaced = 3;
        CountBombPlaced = 0;
        SpaceHold = false;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        dir = _playerInput.actions["Move"].ReadValue<Vector2>();
        RB.velocity = new Vector2(dir.x * 5, dir.y * 5);

        if (_playerInput.actions["Bomb"].ReadValue<float>() == 1f && CountBombPlaced < MaxBombPlaced && !SpaceHold)
        {
            DropBomb(RB.position.x, RB.position.y);
            SpaceHold = true;
        }
        else if(_playerInput.actions["Bomb"].ReadValue<float>() == 0f && SpaceHold)
        {
            SpaceHold = false;
        }
        AnimationUpdate();
    }

    void DropBomb(float x, float y)
    {
        Vector3 pos = new Vector3(Mathf.Floor(x) + 0.5f, Mathf.Floor(y) + 0.5f, (int)Layers.Bomb);
        if(GameManager.DeployBomb(Bomb, pos, this)) CountBombPlaced++;
    }
    public void BombExploded()
    {
        if(CountBombPlaced > 0)
        {
            CountBombPlaced--;
        }
    }

    void AnimationUpdate()
    {
        if (dir.x > 0f)
        {
            SpriteRender.flipX = false;
            Animator.SetBool("IsRunning", true);
        }
        else if (dir.x < 0f)
        {
            SpriteRender.flipX = true;
            Animator.SetBool("IsRunning", true);
        }

        else if (dir.y > 0f || dir.y < 0f) Animator.SetBool("IsRunning", true);
        else Animator.SetBool("IsRunning", false);
    }
    public void Die()
    {
        Destroy(gameObject);
    }


}
