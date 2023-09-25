using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace UserAuthentication
{
    public class InputFieldTabing : MonoBehaviour
    {
        public TMP_InputField[] fields;

        public int selectedIndex;

        private void Start()
        {
            selectedIndex = 0;
            fields[selectedIndex].Select();
        }

        public void SomeFieldIsSelected(TMP_InputField selectedField)
        {
            for (int i = 0; i < fields.Length; i++)
            {
                if (selectedField == fields[i])
                {
                    selectedIndex = i;
                    return;
                }
            }
        }

        // Update is called once per frame
        void Update()
        {

            if ((Input.GetKey(KeyCode.LeftShift) && Input.GetKeyUp(KeyCode.Tab))
                ||
                (Input.GetKey(KeyCode.RightShift) && Input.GetKeyUp(KeyCode.Tab)))
            {
                selectedIndex--;
                if (selectedIndex < 0)
                    selectedIndex = fields.Length - 1;
                fields[selectedIndex].Select();

            }
            else if (Input.GetKeyUp(KeyCode.Tab))
            {
                selectedIndex++;
                if (selectedIndex >= fields.Length)
                    selectedIndex = 0;
                fields[selectedIndex].Select();

            }
        }





    }
}