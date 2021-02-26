using UnityEngine;

[RequireComponent(typeof(Canvas))]
public abstract class PanelController : MonoBehaviour
{
    [SerializeField]
    protected Canvas mainCanvas;
    public bool IsMainCanvasVisible => mainCanvas.enabled;

    protected virtual void Start()
    {
        if (mainCanvas == null)
            mainCanvas = GetComponent<Canvas>();
    }

    //GESTIONE GRAFICA
    //MOSTRA IL PANEL
    public virtual void ShowPanel() => mainCanvas.enabled = true;
    //NASCONDE IL PANEL
    public virtual void HidePanel() => mainCanvas.enabled = false;

    //GESTIONE MOVIMENTO
    //MOVIMENTO DEI TAB
    public abstract PanelControllerReturn OnTabMovement(float movement);
    //MOVIMENTO ALL'INTERNO DEL PANEL
    public abstract PanelControllerReturn OnPanelMovement(Vector2 movement);
    //GESTIONE DELLE AZIONI
    //SE L'UTENTE HA PREMUTO IL TASTO AZIONE
    public abstract PanelControllerReturn OnActionPressed();
    //SE L'UTENTE HA PREMUTO IL TASTO INDIETRO
    //RITORNA UN BOOL, PER INDICARE SE QUALCOSA è STATO FATTO
    public abstract PanelControllerReturn OnBackPressed();
}