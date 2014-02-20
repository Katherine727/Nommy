using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MenuScript : MonoBehaviour
{
    public Texture2D klawisz;
    public GUIStyle tt;

    public Texture2D Cursor_Normal;
    public Texture2D Cursor_Hover;
    private int cursorWidth = 32;
    private int cursorHeight = 32;
    string hover = string.Empty;

    public bool OwnCursor=false;

    // private GUILayer layer;
    private GUIElement layer;
    public class ElementGUI
    {
        string name;
        Rect position;
        // Texture2D buttonTex;

        bool czytextura = false;
        string SceneName;
        GUIStyle styl;

        delegate void Akcja(string _nazwaSc);
        
        Akcja click;


        #region Właściwości
        public Rect Position
        {
            get
            {
                return this.position;
            }
        }

        public string Name
        {
            get
            {
                return this.name;
            }
        }

        public bool CzyTextura
        {
            get
            {
                return this.czytextura;
            }
        }

        public GUIStyle Styl
        {
            get { return this.styl; }
            set { styl = value; }
        }


        #endregion

        public ElementGUI()
        {
            SceneName = "";
            click = new Akcja(Otworz_Scene);
            name = "Button";
            position = new Rect(0, 0, 0, 0);
            styl = new GUIStyle();
        }

        public ElementGUI(string _name, Rect _pos)
            : this()
        {
            name = _name;
            position = new Rect(_pos.xMin * Screen.width, _pos.yMin * Screen.height, _pos.width * Screen.width, _pos.height * Screen.height);
        }

        public ElementGUI(string _Name, Rect _Pos, Texture2D _buttontex)
            : this(_Name, _Pos)
        {
            czytextura = true;
            this.styl.normal.background = _buttontex;


        }

        public ElementGUI(string _name, Rect _pos, Texture2D _btntex, string _scenename)
            : this(_name, _pos, _btntex)
        {
            this.SceneName = _scenename;

        }

        public void Click()
        {
            click(SceneName);
        }

        private void Otworz_Scene(string nazwa)
        {
            if (nazwa == string.Empty)
            {
                Application.Quit();
                return;
            }

            Application.LoadLevel(nazwa);
        }


    }


    public Texture MenuBackground;
    public List<ElementGUI> Buttons;





    void Start()
    {
        //brak kursora
        Screen.showCursor = !OwnCursor;
        layer = Camera.main.GetComponent<GUIElement>();
    }


    // funkcja UNITY do GUI
    void OnGUI()
    {
        //stan gui
        // http://docs.unity3d.com/Documentation/ScriptReference/GUILayoutUtility.GetLastRect.html
        //https://github.com/petereichinger/Unity-Helpers

        Buttons = new List<ElementGUI>();
        Buttons.Add(new ElementGUI("Graj", new Rect(.25f, .25f, .5f, .1f)));
        Buttons.Add(new ElementGUI("Opcje", new Rect(.25f, .40f, .5f, .1f)));
        Buttons.Add(new ElementGUI("", new Rect(.25f, .70f, .5f, .1f), klawisz));



        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), MenuBackground);
        hover = GUI.tooltip;

        DrawButtons();

        if (OwnCursor)
            Myszka();

    }


    private void DrawButtons()
    {
        foreach (ElementGUI bt in Buttons)
        {
            if (bt.CzyTextura)
            {

                //  if(GUI.Button(bt.Position,bt.Name,bt.Styl))
                if (GUI.Button(bt.Position, new GUIContent(bt.Name, "Hov"), bt.Styl))
                {
                    bt.Click();

                }

            }
            //bez grafiki
            else
            {

                if (GUI.Button(bt.Position, new GUIContent(bt.Name, "Hov")))
                {

                    bt.Click();

                }
            }

        }

    }

    private void Myszka()
    {
        Texture2D kursor = Cursor_Normal;
        //if(akcja==1)
        //{

        //    kursor = Cursor_Hover;

        //}

        GUI.DrawTexture(new Rect(Input.mousePosition.x, Screen.height - Input.mousePosition.y, cursorWidth, cursorHeight), kursor);
    }


}

