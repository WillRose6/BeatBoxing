using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteDetector : MonoBehaviour
{
    public struct NoteSpawn
    {
        public NoteSpawn(Transform _location, NoteDetector _noteDetector)
        {
            location = _location;
            detector = _noteDetector;
        }

        public Transform location;
        public NoteDetector detector;
    }

    private RectTransform rect;
    [SerializeField]
    private int ownedPlayerID;

    public ControlScheme ownedControls;
    public int ColumnID;
    public string inputName = "";
    public bool inputAvailable;
    private List<GameObject> collisions = new List<GameObject>();
    public List<Note> inColumn = new List<Note>();
    public bool isDestroyer;

    public int OwnedPlayerID { get => ownedPlayerID; set => ownedPlayerID = value; }

    private void Start()
    {
        inputAvailable = true;
        if (!rect) {
            rect = GetComponent<RectTransform>();
        }

        Player owndedByPlayer = PlayerManager.instance.Players[ownedPlayerID];

        if (!isDestroyer)
        {
            ownedControls = owndedByPlayer.controlScheme;
        }

        if (ownedControls)
        {
            switch (ColumnID)
            {
                case 1:
                    inputName = ownedControls.FirstColumn + (ownedPlayerID + 1);
                    break;
                case 2:
                    inputName = ownedControls.SecondColumn + (ownedPlayerID + 1);
                    break;
                case 3:
                    inputName = ownedControls.ThirdColumn + (ownedPlayerID + 1);
                    break;
                case 4:
                    inputName = ownedControls.FourthColumn + (ownedPlayerID + 1);
                    break;
            }
        }
    }

    IEnumerator Pulse()
    {
        for (int i = 0; i <= 10; i++)
        {
            if (i <= 5) { gameObject.transform.localScale *= 1.1f; }
            else { gameObject.transform.localScale /= 1.1f; }
            yield return new WaitForSeconds(0.01f);
        }
        gameObject.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
    }

    private void Update()
    {
        if (inputName != "")
        {
            float axisValue = Input.GetAxis(inputName);
            ConfigureInputs();

            if (inputAvailable && axisValue > 0)
            {
                StartCoroutine("Pulse");
                inputAvailable = false;
                if (collisions.Count > 0)
                {
                    for (int i = 0; i < collisions.Count; i++)
                    {
                        if (collisions[i])
                        {
                            Note n = collisions[i].GetComponent<Note>();
                            if (n)
                            {
                                if (n)
                                {
                                    HitNote();
                                    inColumn.Remove(n);
                                    Destroy(collisions[i]);
                                }
                            }
                        }
                    }
                }
                else
                {
                    NoNotesHit();
                }
            }
        }
    }

    private void NoNotesHit()
    {
        GameManager.instance.ScoreManager.AddScoreToPlayer(ownedPlayerID, -20);
        PlayerManager.instance.Players[ownedPlayerID].GetComponent<RhythmPlayer>().Combo = 0;
    }

    private void ConfigureInputs()
    {
        if (!inputAvailable)
        {
            if (Input.GetAxis(inputName) == 0)
            {
                inputAvailable = true;
            }
        }
    }

    private void HitNote()
    {
        GameManager.instance.ScoreManager.AddScoreToPlayer(ownedPlayerID, 100);
        PlayerManager.instance.Players[ownedPlayerID].GetComponent<RhythmPlayer>().Combo++;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDestroyer)
        {
            PlayerManager.instance.Players[ownedPlayerID].GetComponent<RhythmPlayer>().Combo = 0;
            GameManager.instance.ScoreManager.AddScoreToPlayer(ownedPlayerID, -10);
            Destroy(collision.gameObject);
        }
        else if((PlayerManager.instance.Players[ownedPlayerID] as RhythmPlayer).isAI)
        {
            if (Random.Range(0, 10) < 9)
            {
                StartCoroutine("Pulse");
                HitNote();
                inColumn.Remove(collision.gameObject.GetComponent<Note>());
                Destroy(collision.gameObject);
            }
        }
        else
        {
            collisions.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        inColumn.Remove(collision.gameObject.GetComponent<Note>());
        collisions.Remove(collision.gameObject);
    }

    public void AddNoteToColumn(Note note)
    {
        inColumn.Add(note);
    }
}
