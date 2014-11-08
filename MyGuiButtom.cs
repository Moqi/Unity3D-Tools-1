using System;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(BoxCollider), typeof(SpriteRenderer))]
public class MyGuiButtom : MonoBehaviour
{
    public event TelaEventos.EventoVasio Clicked;

    public bool Disabled;

    public enum ButtomType
    {
        TrocaSprites,
        MudaCores
    }
    public ButtomType TipoBotão;

    public AudioClip SomClick;
    public Sprite Pressed;

    public Color32 CorPressionado = Cor.cinza;

    private SpriteRenderer _spriteRenderer;
    private Sprite _defaultSprite;
    private AudioSource _somClickSource;

    public GameObject[] EnableObjects;
    public GameObject[] DisableObjects;
    public ObjetosParaMensagem[] SendMessages;

    [Serializable]
    public class ObjetosParaMensagem
    {
        public GameObject Target;
        public string Message;
    }

    private void Start()
    {
        Clicked += OnClick;

        _spriteRenderer = GetComponent<SpriteRenderer>();
        _defaultSprite = _spriteRenderer.sprite;

        if (SomClick == null || !Application.isPlaying)
            return;

        _somClickSource = gameObject.AddComponent<AudioSource>();
        _somClickSource.playOnAwake = false;
        _somClickSource.clip = SomClick;
    }

    internal virtual void OnClick()
    {
       
    }

    private void TouchOn()
    {
        switch (TipoBotão)
        {
            case ButtomType.TrocaSprites:
                if (Pressed == null)
                    return;
                _spriteRenderer.sprite = Pressed;
                break;
        }

    }

    private void TouchOff()
    {
        switch (TipoBotão)
        {
            case ButtomType.TrocaSprites:
                if (_defaultSprite != null)
                    _spriteRenderer.sprite = _defaultSprite;
                break;
        }

        if (_somClickSource != null)
            _somClickSource.Play();


        Clicked();

        foreach (var o in EnableObjects)
            o.SetActive(true);

        foreach (var o in DisableObjects)
            o.SetActive(false);

        foreach (var o in SendMessages)
        {
            if (o.Target != null && !string.IsNullOrEmpty(o.Message))
                o.Target.SendMessage(o.Message);
        }
    }

    private void TouchCancel()
    {
        if (_defaultSprite != null)
            _spriteRenderer.sprite = _defaultSprite;
    }

    private void TouchMove()
    {

    }

    private void TouchHold()
    {

    }

    private enum AcaoCor
    {
        clarear, escurecer
    }

    private AcaoCor acao;

    void Update()
    {
        Action spriteClarear   = () => _spriteRenderer.color = Color.Lerp(_spriteRenderer.color, Cor.clear, 0.3f);
        Action spriteEscurecer = () => _spriteRenderer.color = Color.Lerp(_spriteRenderer.color, CorPressionado, 0.3f);


        switch (acao)
        {
            case AcaoCor.clarear:
                spriteClarear();
                break;
            case AcaoCor.escurecer:
                spriteEscurecer();
                break;
        }

        if(Disabled)
        {
            acao = AcaoCor.escurecer;
            return;
        }

        if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                var pos = Input.mousePosition;

                var raioMouse = FindObjectOfType<Camera>().ScreenPointToRay(pos);

                RaycastHit colisao;

                // Se nao colidiu com nada
                if (!Physics.Raycast(raioMouse, out colisao))
                    return;

                // Se o objeto não for exatamente o que eu quero
                if (colisao.transform.gameObject.GetHashCode() != gameObject.GetHashCode())
                    return;

                TouchOff();
                //TouchOn();
                acao = AcaoCor.escurecer;
            }
            else if (Input.GetButtonUp("Fire1"))
            {
                //TouchOff();
                acao = AcaoCor.clarear;
            }
        }
        else
        {
            if (Input.touchCount <= 0)
                return;
            var toque = Input.touches.FirstOrDefault();

            var raioToque = Camera.current.ScreenPointToRay(toque.position);

            RaycastHit colisao;

            // Se nao colidiu com nada
            if (!Physics.Raycast(raioToque, out colisao))
                return;

            // Se o objeto não for exatamente o que eu quero
            if (colisao.transform.gameObject.GetHashCode() != gameObject.GetHashCode())
            {
                spriteClarear();
                return;
            }

            if (TipoBotão == ButtomType.MudaCores)
                acao = AcaoCor.escurecer;

            switch (toque.phase)
            {
                    // Inicio do toque
                case TouchPhase.Began:
                    TouchOn();
                    break;

                    // Movendo
                case TouchPhase.Moved:
                    TouchMove();
                    break;

                    // Segurando na tela
                case TouchPhase.Stationary:
                    TouchHold();
                    break;

                    // Parou de tocar
                case TouchPhase.Ended:
                    acao = AcaoCor.clarear;
                    TouchOff();
                    break;

                    // Cancelou a movimentação do toque
                case TouchPhase.Canceled:
                    TouchCancel();
                    break;
            }
        }
    }

    public void CorrigirBoxCollider()
    {
        var spriteAtual = GetComponent<SpriteRenderer>().sprite;

        var tamanhoSprite = new Vector2(spriteAtual.rect.width, spriteAtual.rect.height);

        GetComponent<BoxCollider>().size = new Vector2(
            x: tamanhoSprite.x / 100,
            y: tamanhoSprite.y / 100
            );
    }

}