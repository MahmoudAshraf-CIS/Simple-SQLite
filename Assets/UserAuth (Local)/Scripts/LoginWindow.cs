using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UserAuthentication;
using TMPro;
using Scheme;

namespace UserAuthentication
{
    public class LoginWindow : MonoBehaviour
    {
        [SerializeField]
        TMP_InputField mUserName, mPassword;
        [SerializeField]
        GameObject mUserNameHint, mPasswordHint;

        [SerializeField]
        Button mLoginButton;
        [SerializeField]
        GameObject mLoadingIndicator;

        [SerializeField]
        UnityEvent OnLoginAttempt, OnLoginFaile, OnLoginSuccess;

        public enum LoadLevelOnLogin
        {
            none = -1,
            Level0 = 0,
            Level1 = 1,
            Level2 = 2,
        }

        public LoadLevelOnLogin levelToLoadOnLogin = LoadLevelOnLogin.none;

        
        // Start is called before the first frame update
        void Start()
        {
            mLoginButton.onClick.AddListener(OnLoginClick);
            mLoadingIndicator.SetActive(false);

            OnLoginAttempt.AddListener(OnLoginAttemptAux);
            OnLoginFaile.AddListener(OnLoginFaileAux);
            OnLoginSuccess.AddListener(OnLoginSuccessAux);

            // disable hints
            mUserNameHint.SetActive(false);
            mPasswordHint.SetActive(false);
        }

        private void OnDestroy()
        {
            mLoginButton.onClick.RemoveListener(OnLoginClick);
            this.StopAllCoroutines();
        }

        void OnLoginClick()
        {
            if (OnLoginAttempt != null)
                OnLoginAttempt.Invoke();

            StartCoroutine(AsyncToLogin(mUserName.text,
                mPassword.text,
                OnLoginSuccess,
                OnLoginFaile));
        }


        public void LogIn(string useraName, string password)
        {
            mUserName.text = useraName;
            mPassword.text = password;
            OnLoginClick();
        }

        void OnLoginAttemptAux()
        {
            Debug.Log("[User Name : " + mUserName.text + ", Password : " + mPassword.text + " ]");
            mLoadingIndicator.SetActive(true);
            mUserName.interactable = mPassword.interactable = false;
           
        }
        void OnLoginFaileAux()
        {
            mLoadingIndicator.SetActive(false);
            mUserName.interactable = mPassword.interactable = true;
            DialogWindow.Instance.EnableDialog("Login failed !", 
                "Failed to Login, Incorrect user name or password!", 
                "OK");
        }
        void OnLoginSuccessAux()
        {
            mLoadingIndicator.SetActive(false);
            mUserName.interactable = mPassword.interactable = true;
            DialogWindow.Instance.EnableDialog("Login Sucess !", 
                "Now you are loged in..", 
                "OK");

            if (levelToLoadOnLogin != LoadLevelOnLogin.none && 
                UnityEngine.SceneManagement.SceneManager.sceneCount >= ((int)levelToLoadOnLogin))
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(((int)levelToLoadOnLogin));
            }
        }
        private IEnumerator AsyncToLogin(
            string userName,
            string password,
            UnityEvent onSucess,
            UnityEvent onFaile)
        {

            print("Login process starder" + Time.time);

            //yield return new WaitForSeconds(2);
            var usr = UsersDB.Connect().Table<User>().Where(u => u.UserName == userName).FirstOrDefault();
            //Debug.Log(usr.ToString());
            bool result = (usr != null && usr.Password == password) ;  
                        
            if (result && onSucess != null)
            {
                Session.Instance.SetUser(usr);
                onSucess.Invoke();
            }
            else if (!result && onFaile != null)
                onFaile.Invoke();

            print("Login process finished" + Time.time + ": With result +["+result+"]");
            yield return null;
        }

        
    }
}
    