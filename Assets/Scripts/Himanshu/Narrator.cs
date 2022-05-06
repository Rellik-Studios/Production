using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using rachael;
using rachael.SaveSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Himanshu
{
        
    public class Narrator : MonoBehaviour
    {
        private Dictionary<string, AudioClip> m_audioClips;
        public NarratorDialogue m_narratorDialogue;
        
        #region Text

        
        [SerializeField] private TMP_Text m_textBox;
        
        
        [TextArea(4, 6)]
        [SerializeField] public List<string> m_hidingLines;
        
        public bool hidingLines
        {
            set => Play(m_hidingLines);
        }
        
        
        [TextArea(4, 6)]
        [SerializeField] public List<string> m_spottedLines;
        
        public bool spottedLines
        {
            set => Play(m_spottedLines);
        }
        
        [TextArea(4, 6)]
        [SerializeField] public List<string> m_madeSound;
        
        public bool madeSound
        {
            set => Play(m_madeSound);
        }
        
        [TextArea(4, 6)]
        [SerializeField] public List<string> m_corridor;
        
        public bool corridor
        {
            set => Play(m_corridor, false);
        }

        [TextArea(4, 6)]
        [SerializeField] public List<string> m_breathing;
        
        public bool breathing
        {
            set => Play(m_breathing);
        }
        [TextArea(4, 6)]
        [SerializeField] public List<string> m_idleRoom;
        
        public bool idleRoom
        {
            set => Play(m_idleRoom);
        }

        [TextArea(4, 6)]
        [SerializeField] public List<string> m_ballRoom;
        
        public bool ballRoom
        {
            set => Play(m_ballRoom);
        }
        
        [TextArea(4, 6)]
        [SerializeField] public List<string> m_cafeteria;
        
        public bool cafeteria
        {
            set => Play(m_cafeteria);
        }
        
        [TextArea(4, 6)]
        [SerializeField] public List<string> m_maze;
        
        public bool maze
        {
            set => Play(m_maze);
        }
        
        [TextArea(4, 6)]
        [SerializeField] public List<string> m_bathroom;
        
        public bool bathroom
        {
            set => Play(m_bathroom);
        }
        
        [TextArea(4, 6)]
        [SerializeField] public List<string> m_mirrorShatter;
        
        public bool mirrorShatter
        {
            set => Play(m_mirrorShatter);
        }
        
        [TextArea(4, 6)]
        [SerializeField] public List<string> m_hospital1950;
        
        public bool hospital1950
        {
            set => Play(m_hospital1950);
        }
        
        [TextArea(4, 6)]
        [SerializeField] public List<string> m_hospitalIdle;
        
        public bool hospitalIdle
        {
            set => Play(m_hospitalIdle);
        }
        
        [TextArea(4, 6)]
        [SerializeField] public List<string> m_hospital1870;
        
        public bool hospital1870
        {
            set => Play(m_hospital1870);
        }
        
        [TextArea(4, 6)]
        [SerializeField] public List<string> m_hospitalFuturistic;
        
        
        public bool hospitalFuturistic
        {
            set => Play(m_hospitalFuturistic);
        }
        
        [TextArea(4, 6)]
        [SerializeField] public List<string> m_hospitalPresent;

        
        
        public bool hospitalPresent
        {
            set => Play(m_hospitalPresent);
        }
        
        [TextArea(4, 6)]
        [SerializeField] public List<string> m_hub1;
        
        
        public bool hub1
        {
            set => Play(m_hub1);
        }
        
        [TextArea(4, 6)]
        [SerializeField] public List<string> m_hub2;
        
        
        public bool hub2
        {
            set => Play(m_hub2);
        }
        
        [TextArea(4, 6)]
        [SerializeField] public List<string> m_finish1;

        
        public bool finish1
        {
            set => Play(m_finish1);
        }
        
        #endregion

        public float m_idleTimer;
        
        private Coroutine m_waitPlay;
        private bool m_settingText = false;
        public bool settingText
        {
            get => m_settingText;
            set => m_settingText = value;
        }

        [SerializeField] private GameObject m_textBackdrop;
        private AudioSource m_audioSource;

        private void Start()
        {
            m_audioSource = GetComponent<AudioSource>();
            m_audioClips = new Dictionary<string, AudioClip>();
            var clips = Resources.LoadAll<AudioClip>("Dialogues/");
            foreach (var clip in clips)
            {
                m_audioClips.Add(clip.name.Substring(0, 15), clip);
                Debug.Log(clip.name);
            }
            var tempNarr = SaveSystem.LoadNarrator();

            m_narratorDialogue = tempNarr ?? new NarratorDialogue();
            
            m_waitPlay = StartCoroutine(eSetText("", m_textBox));
            m_idleTimer = Random.Range(90f, 120f);
            
        }


        private void Update()
        {
            if (m_idleTimer < 0f)
            {
                m_idleTimer = 4f;
                //Play(m_idleRoom);
            }
            else
            {
                m_idleTimer -= Time.deltaTime;
            }
           

            if (m_textBox.text != "" && !m_textBackdrop.activeSelf)
            {
                m_textBackdrop.SetActive(true);
            }
            
            else if (m_textBox.text == "" && m_textBackdrop.activeSelf)
            {
                m_textBackdrop.SetActive(false);
            }
        }

        //Call this function when a door dissapears.
        public void ResetTimer()
        {
            m_idleTimer = Random.Range(90f, 120f);
        }

        public void Play(string _toPlay)
        {
            StopAllCoroutines();
            m_textBox.text = "";
            // if (!m_settingText)
            {
                StartCoroutine(eSetText(_toPlay, m_textBox)); 
            }
        }
        public void Play(List<string> _toPlay, bool _isRandom = true)
        {
            if (gameManager.Instance.isTutorialRunning) return;
                
            if (_toPlay.Count > 1 && !m_settingText)
            {
                int rand = Random.Range(1, _toPlay.Count);
                StartCoroutine(eSetText(_toPlay[_isRandom ? rand : 1], m_textBox));
                _toPlay.RemoveAt(_isRandom ? rand : 1);
            }
            
            else if (!m_settingText)
            {
                StartCoroutine(eSetText(_toPlay[0], m_textBox));
            }

            else
            {
                //StopCoroutine(m_waitPlay);
                m_waitPlay = StartCoroutine(WaitAndPlay(_toPlay));
            }
            
            SaveSystem.SaveNarrator();
        }

        private IEnumerator WaitAndPlay(List<string> _toPlay)
        {
            yield return new WaitWhile(() => m_settingText);
            Play(_toPlay);
        }

        IEnumerator eSetText(string _text, TMP_Text _textBox, bool additive = false)
        {
           
            if (_text.Length > 15 && m_audioClips.TryGetValue(_text.Substring(0, 15), out AudioClip clip))
            {
                m_audioSource.PlayOneShot(clip);
            }
            m_settingText = true;
            if(!additive) _textBox.text = "";
            bool commandStart = false;
            bool conditionStart = false;
            bool conditionEnd = false;
            bool choiceStart = false;
            bool choiceEnd = false;
            string command = "";
            string condition = "";
            int pos = 0;
            List<string> choices = new List<string>();
            List<string> conditions = new List<string>();
            string currentChoice = "";
            string currentCondition = "";
            bool skip = false;
            bool unconditionalSkip = false;
            foreach (var letter in _text)
            {
                pos++;
                if (letter == '#')
                {
                    yield return new WaitForSeconds(2f);
                    yield return StartCoroutine(eSetText(_text.Substring(pos + 1), _textBox));
                    yield break;
                }

                if (letter == '%')
                {
                    skip = true;
                    continue;
                }

                if (letter == '~')
                {
                    unconditionalSkip = true;
                    continue;
                }

                //conditionStart = true;
                if (conditionStart)
                {
                    if (conditionEnd)
                    {
                        if (letter == '|')
                        {
                            conditions.Add(currentCondition);
                            currentCondition = "";
                            continue;
                        }
                        else if (letter == '$')
                        {
                            conditionEnd = false;
                            continue;
                        }
                        else
                        {
                            currentCondition += letter;
                            continue;
                        }

                        
                    }
                    else
                    {
                        if (letter == '$')
                        {
                            yield return StartCoroutine(eSetText(conditions[ConditionCheck(condition).Result], _textBox, true));
                            conditionStart = false;
                            conditionEnd = false;
                            continue;
                        }
                        else if (letter != '|')
                        {
                            condition += letter;
                            continue;
                        }
                        else if (letter == '|')
                        {
                            conditionEnd = true;
                            continue;
                        }
                    }
                }

                if (choiceStart)
                {
                    if (letter == '|')
                    {
                        choices.Add(currentChoice);
                        currentChoice = "";
                        continue;
                    }
                    else if (letter == '}')
                    {
                        choices.Add(currentChoice);
                        choiceEnd = true;
                        choiceStart = false;
                    }
                    else
                    {
                        currentChoice += letter;
                        continue;
                    }
                }

                if (choiceEnd)
                {
                    yield return StartCoroutine(eSetText("%" + choices.Random(), _textBox, true));
                    m_settingText = true;
                    choiceEnd = false;
                    continue;
                }

                if (letter == '{')
                {
                    choiceStart = true;
                    continue;
                }

                if (letter == '*')
                {
                    conditionStart = true;
                    continue;
                }
                
                if (commandStart)
                {
                    if (letter == ' ' || letter == '?' || letter == ',')
                    {
                        var t = EvaluateCommand(command);
                        if(t != "")
                            yield return StartCoroutine(eSetText(t, _textBox, true));
                        m_settingText = true;
                        //_textBox.text += EvaluateCommand(command);
                        commandStart = false;
                    }
                    else
                    {
                        command += letter;
                        continue;
                    }
                    
                }
                if (letter == '@')
                {
                    commandStart = true;
                    command = "";
                }
                else if (letter == ' ')
                {
                    _textBox.text += letter;
                    yield return new WaitForSeconds(0.05f);
                }
                else
                {
                    _textBox.text += letter;
                    yield return new WaitForSeconds(0.0167f);
                }
            }

            if(!(skip || unconditionalSkip))
                yield return new WaitForSeconds(3f);
            
            if(!skip)
                m_textBox.SetText("");
            
            m_settingText = false;
        }

        private string EvaluateCommand(string _command)
        {
            switch (_command)
            {
                case "disableDoor":
                {
                    foreach (var door in FindObjectsOfType<Door>())
                    {
                        door.m_locked = true;
                    }

                    return "";
                }
                case "enableDoor":
                {
                    foreach (var door in FindObjectsOfType<Door>())
                    {
                        door.m_locked = false;
                    }
                    return "";
                }
                case "userName":
                    return "%" + NarratorScript.UserName;
                case "timeCategory":
                    return "%" + NarratorScript.timeCategory;
                case "day":
                    return "%" + NarratorScript.WeekDay;
                case "time":
                    return "%" + NarratorScript.Time;
                case "activity":
                    return "%" + NarratorScript.activity;
                default:
                    return "Not Set Yet";
            }
            return "";
        }

        private async Task<int>  ConditionCheck(string _condition)
        {
            
            switch (_condition)
            {
                case "tutorialSkipped ":
                {
                    if (Tutorial.m_tutorialSkipped)
                    {
                        return 0;
                    }
                    else
                    {
                        return 1;
                    }
                    break;
                }
                
                default: return 0;
            }

            return 0;
        }
        

    }
}