using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace FaceHorn.UI {
    [AddComponentMenu("FaceHorn/UI/Dropdown", 3)]
    [RequireComponent(typeof(RectTransform))]
    public class Dropdown : Selectable {
        
        [SerializeField]
        private Dropdown.OptionDataList m_Options = new Dropdown.OptionDataList();
        [Space]
        [SerializeField]
        private Dropdown.DropdownEvent m_OnValueChanged = new Dropdown.DropdownEvent();
        [SerializeField]
        private RectTransform m_Template;
        [SerializeField]
        private Text m_CaptionText;
        [SerializeField]
        private Image m_CaptionImage;
        [Space]
        [SerializeField]
        private Text m_ItemText;
        [SerializeField]
        private Image m_ItemImage;
        [Space]
        [SerializeField] private float m_MaxDropdownSize = 100;
        [SerializeField] private float m_SizeOfEachItem = 20;
        [SerializeField] private float m_SpaceInbetweenItems = 0;
        [SerializeField] private float m_SpaceBetweenVerticalEdgesToItems = 5;
        [SerializeField]
        private int m_Value;
        private GameObject m_Dropdown;
        private GameObject m_Blocker;
        public bool m_DropdownShown = false;
        private int m_CurrentPlayerNum = 0;
        //private TweenRunner<FloatTween> m_AlphaTweenRunner;

            
        public float maxDropdownSize {
            get {
                return m_MaxDropdownSize;
            }
            set {
                m_MaxDropdownSize = value;
            }
        }

        public float sizeOfEachItem {
            get {
                return m_SizeOfEachItem;
            }
            set {
                m_SizeOfEachItem = value;
            }
        }

        public float spaceInbetweenItems {
            get {
                return m_SpaceInbetweenItems;
            }
            set {
                m_SpaceInbetweenItems = value;
            }
        }

        public RectTransform template {
            get {
                return this.m_Template;
            }
            set {
                this.m_Template = value;
                this.RefreshShownValue();
            }
        }

        public Text captionText {
            get {
                return this.m_CaptionText;
            }
            set {
                this.m_CaptionText = value;
                this.RefreshShownValue();
            }
        }

        public Image captionImage {
            get {
                return this.m_CaptionImage;
            }
            set {
                this.m_CaptionImage = value;
                this.RefreshShownValue();
            }
        }

        public Text itemText {
            get {
                return this.m_ItemText;
            }
            set {
                this.m_ItemText = value;
                this.RefreshShownValue();
            }
        }

        public Image itemImage {
            get {
                return this.m_ItemImage;
            }
            set {
                this.m_ItemImage = value;
                this.RefreshShownValue();
            }
        }

        public List<Dropdown.OptionData> options {
            get {
                return this.m_Options.options;
            }
            set {
                this.m_Options.options = value;
                this.RefreshShownValue();
            }
        }

        public Dropdown.DropdownEvent onValueChanged {
            get {
                return this.m_OnValueChanged;
            }
            set {
                this.m_OnValueChanged = value;
            }
        }

        public int value {
            get {
                return this.m_Value;
            }
            set {
                this.m_Value = Mathf.Clamp(value, 0, this.options.Count - 1);
                this.RefreshShownValue();

                this.m_OnValueChanged.Invoke(this.m_Value);
            }
        }

#if UNITY_EDITOR
        protected override void OnValidate() {
            base.OnValidate();
            RefreshShownValue();
        }
#endif


        public void Show(int playerNum) {
            
            if (m_DropdownShown) {
                return;
            }

            if (m_ItemText.transform.parent.GetComponent<DropdownItem>() == null) {
                m_ItemText.transform.parent.gameObject.AddComponent(typeof(DropdownItem));
            }

            m_Dropdown =  GameObject.Instantiate(template,gameObject.transform).gameObject;
            m_Dropdown.name = "Dropdown List:";
            float height = (m_Options.options.Count * m_SizeOfEachItem) + ((m_Options.options.Count - 1) * m_SpaceInbetweenItems) + (m_SpaceBetweenVerticalEdgesToItems * 2);
            RectTransform dropDownTransform = m_Dropdown.GetComponent<RectTransform>();

            if (height > m_MaxDropdownSize) {
                dropDownTransform.sizeDelta = new Vector2(dropDownTransform.sizeDelta.x, maxDropdownSize);
            }
            else {
                dropDownTransform.sizeDelta = new Vector2(dropDownTransform.sizeDelta.x, height);
            }

            m_Dropdown.transform.GetChild(0).GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(m_Dropdown.transform.GetChild(0).GetChild(0).GetComponent<RectTransform>().sizeDelta.x, height);
            m_Dropdown.SetActive(true);

            

            GameObject dropDownItem = m_Dropdown.transform.GetChild(0).GetChild(0).GetChild(0).gameObject;


            List<Selectable> items = new List<Selectable>();


            for (int i = 1; i <= m_Options.options.Count; i++) {
                RectTransform a = Instantiate(dropDownItem, dropDownItem.transform.parent).GetComponent<RectTransform>();
                a.name = "Item: " + i;

                a.anchorMin = Vector2.zero;
                a.anchorMax = new Vector2(1, 0);

                a.sizeDelta = new Vector2(a.sizeDelta.x, m_SizeOfEachItem);

                float newHeight = -(((m_SizeOfEachItem / 2) + (m_SizeOfEachItem * (i - 1))) + (m_SpaceInbetweenItems * (i-1) + m_SpaceBetweenVerticalEdgesToItems));
                
                a.localPosition = new Vector3(a.localPosition.x, newHeight, a.localPosition.z);

                items.Add(a.GetComponent<Selectable>());

                a.GetComponentInChildren<Text>().text = m_Options.options[i - 1].text;
                a.GetComponentInChildren<Image>().sprite = m_Options.options[i - 1].image;

                if (i - 1 == m_Value) {
                    a.GetChild(1).gameObject.SetActive(true);
                }
                else {
                    a.GetChild(1).gameObject.SetActive(false);
                }

                a.GetComponent<DropdownItem>().text = a.GetComponentInChildren<Text>();
                a.GetComponent<DropdownItem>().value = i - 1;
                a.GetComponent<DropdownItem>().dropdown = this;
                a.GetComponent<Button>().onClick.AddListener(new UnityAction(a.GetComponent<DropdownItem>().SendInfo));


                a.gameObject.SetActive(true);

                items[i - 1].navigationMode = Navigation.Mode.Explicit;

                if (i != 1) {
                    items[i - 2].SetNavigationExplicitSelectable(Navigation.Direction.SelectOnDown, items[i - 1]);

                    if (i <= m_Options.options.Count) {
                        items[i - 1].SetNavigationExplicitSelectable(Navigation.Direction.SelectOnUp, items[i - 2]);
                    }
                }

                if (i == 1) {
                    if (playerNum == 1) {
                        a.GetComponent<Selectable>().P1Select();
                    }
                    else if (playerNum == 2) {
                        a.GetComponent<Selectable>().P2Select();
                    }
                }
                


            }

            

            dropDownItem.SetActive(false);
            Debug.Log(playerNum);
            m_CurrentPlayerNum = playerNum;
            m_DropdownShown = true;
        }

        public void Hide() {

            if (m_CurrentPlayerNum == 1) {
                P1Select();
            }
            else {
                P2Select();
            }
            m_CurrentPlayerNum = 0;
            Destroy(m_Dropdown);
            m_DropdownShown = false;

        }



        public void RefreshShownValue() {
            m_CaptionText.text = m_Options.options[m_Value].text;
        }















        [Serializable]
        public class DropdownEvent : UnityEvent<int> {
        }


        protected internal class DropdownItem : MonoBehaviour {

            [SerializeField] private Text m_Text;
            [SerializeField] private int m_Value;
            [SerializeField] private Dropdown m_Dropdown;

            public Text text {
                get {
                    return m_Text;
                }
                set {
                    m_Text = value;
                }
            }
            
            public int value {
                get {
                    return m_Value;
                }
                set {
                    m_Value = value;
                }
            }

            public Dropdown dropdown{
                get {
                    return m_Dropdown;
                }
                set {
                    m_Dropdown = value;
                }
            }

            public void SendInfo() {

                if (m_Dropdown.value != m_Value) {
                    m_Dropdown.onValueChanged.Invoke(0);
                }
                m_Dropdown.value = m_Value;
                m_Dropdown.captionText.text = m_Text.text;
                dropdown.Hide();
            }


        }

        

        [Serializable]
        public class OptionData
        {
            [SerializeField]
            private string m_Text;
            [SerializeField]
            private Sprite m_Image;

            public OptionData()
            {
            }

            public OptionData(string text)
            {
                this.text = text;
            }

            public OptionData(Sprite image)
            {
                this.image = image;
            }

            public OptionData(string text, Sprite image)
            {
                this.text = text;
                this.image = image;
            }

            public string text
            {
                get
                {
                    return this.m_Text;
                }
                set
                {
                    this.m_Text = value;
                }
            }

            public Sprite image
            {
                get
                {
                    return this.m_Image;
                }
                set
                {
                    this.m_Image = value;
                }
            }
        }
        

        [Serializable]
        public class OptionDataList
        {
            [SerializeField]
            private List<Dropdown.OptionData> m_Options;

            public OptionDataList()
            {
                this.options = new List<Dropdown.OptionData>();
            }

            public List<Dropdown.OptionData> options
            {
                get
                {
                    return this.m_Options;
                }
                set
                {
                    this.m_Options = value;
                }
            }
        }

        

    }
}
