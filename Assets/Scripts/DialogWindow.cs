using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

namespace UserAuthentication
{
    public class DialogWindow : MonoBehaviour
    {
        [SerializeField]
        TextMeshProUGUI mTitle, mMessage;
        [SerializeField]
        UnityEngine.UI.Button mDissmessButton;
        [SerializeField]
        GameObject mDialogHolder;


        public static DialogWindow Instance { get; private set; }
        private void Awake()
        {
            // If there is an instance, and it's not me, delete myself.

            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }
            mDialogHolder.SetActive(false);

        }

        public void EnableDialog(string title, string message, string dismessButtonText)
        {
            mTitle.text = title;
            mMessage.text = message;
            UnityEngine.UI.Text txt = mDissmessButton.transform.GetChild(0).GetComponent<UnityEngine.UI.Text>();
            if (txt)
                txt.text = dismessButtonText;
            mDialogHolder.SetActive(true);
        }

    }
}