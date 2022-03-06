using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UserAuthentication;
public class LoginWindow : MonoBehaviour
{
    [SerializeField]
    InputField mUserName, mPassword;
    [SerializeField]
    Toggle mRememberMe;
    [SerializeField]
    Button mLoginButton;
    [SerializeField]
    GameObject mLoadingIndicator;

    [SerializeField]
    UnityEvent OnLoginAttempt,OnLoginFaile,OnLoginSuccess;


    public bool loginResult = true;

    // Start is called before the first frame update
    void Start()
    {
        mLoginButton.onClick.AddListener(OnLoginClick);
        mLoadingIndicator.SetActive(false);

        OnLoginAttempt.AddListener(OnLoginAttemptAux);
        OnLoginFaile.AddListener(OnLoginFaileAux);
        OnLoginSuccess.AddListener(OnLoginSuccessAux);
    }

    private void OnDestroy()
    {
        mLoginButton.onClick.RemoveListener(OnLoginClick);
        this.StopAllCoroutines();
    }

    void OnLoginClick()
    {
        Debug.Log("[User Name : " + mUserName.text + ", Password : " + mPassword.text + " ]");
        mLoadingIndicator.SetActive(true);
        mUserName.interactable = mPassword.interactable = false;
        if (OnLoginAttempt != null)
            OnLoginAttempt.Invoke();


        StartCoroutine(AsyncToLogin(mUserName.text, 
            mPassword.text,
            OnLoginSuccess,
            OnLoginFaile ));        
    }

    void OnLoginAttemptAux()
    {

    }
    void OnLoginFaileAux()
    {
        mLoadingIndicator.SetActive(false);
        mUserName.interactable = mPassword.interactable = true;
    }
    void OnLoginSuccessAux()
    {

    }
    private IEnumerator AsyncToLogin(
        string userName,
        string password, 
        UnityEvent onSucess, 
        UnityEvent onFaile)
    {
         
        print("Login process starder" + Time.time);

        yield return new WaitForSeconds(2);

        bool result = Login(userName, password);
        if (result && onSucess != null)
            onSucess.Invoke();
        else if (!result && onFaile != null)
            onFaile.Invoke();

        print("Login process finished" + Time.time);
    }

    private bool Login(string userName, string password)
    {
        var usr = UsersDB.Connect().Table<User>().Where(u => u.UserName == userName).FirstOrDefault();
        //Debug.Log(usr.ToString());
        if (usr == null)
            return false;
        return usr.Password == password;
    }

}
