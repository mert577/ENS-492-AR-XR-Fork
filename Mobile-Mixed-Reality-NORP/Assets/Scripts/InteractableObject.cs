using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public InteractableType interactableType;

    /*
    public GameObject hocaPrefab;
    public AudioClip hocaClip;
    public bool isSpawn;
    */
}

public enum InteractableType
{
    SpawnErkayHoca,
    SpawnKursatHoca,
    SabanciIntroButton,
    MainMenuButton,
    ErkayHocaDialog1,
    ErkayHocaDialog2,
    ErkayHocaDialog3,
    KursatHocaDialog1,
    KursatHocaDialog2,
    KursatHocaDialog3,
    GotoLocationScene,
}
