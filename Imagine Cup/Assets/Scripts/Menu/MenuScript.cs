using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MenuScript : MonoBehaviour
{
    public Texture2D klawisz;
    public GUIStyle tt;

    public class ElementGUI
    {
        string name;
        Rect position;
        Texture2D buttonTex;
        bool czytextura = false;

       
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

        public Texture2D ButtonTexture
        {
            get
            {
                return this.buttonTex;
            }
        }
        #endregion

        public ElementGUI()
        {
            name = "Button";
            position = new Rect(0, 0, 0, 0);
        }

        public ElementGUI(string _name, Rect _pos)
        {
            name = _name;
            position = new Rect(_pos.xMin * Screen.width, _pos.yMin * Screen.height, _pos.width * Screen.width, _pos.height * Screen.height);
        }

        public ElementGUI(string _Name,Rect _Pos,Texture2D _buttontex)
            :this(_Name,_Pos)
        {
            czytextura = true;
            this.buttonTex = _buttontex;
           
        }

    }


    public Texture MenuBackground;
    public List<ElementGUI> Buttons;



    // funkcja UNITY do GUI
    void OnGUI()
    {
        Buttons = new List<ElementGUI>();
        Buttons.Add(new ElementGUI("Graj", new Rect(.25f, .25f, .5f, .1f)));
        Buttons.Add(new ElementGUI("Opcje", new Rect(.25f, .40f, .5f, .1f)));
        Buttons.Add(new ElementGUI("", new Rect(.25f, .70f, .5f, .1f),klawisz));


        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), MenuBackground);

        DrawButtons();
    }


    private void DrawButtons()
    {
        foreach (ElementGUI bt in Buttons)
        {
            if(bt.CzyTextura)
            {
               GUIStyle tmp=new GUIStyle();
               tmp.normal.background=bt.ButtonTexture;

                if(GUI.Button(bt.Position,bt.Name,tmp))
                    {
                        //funkcja
                    }            
               
            }
           //bez grafiki
            else
            {
                if (GUI.Button(bt.Position, bt.Name))
                {
                    //funkcja
                }
            }
           
        }

    }


}

