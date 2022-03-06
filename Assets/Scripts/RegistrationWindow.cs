using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using TMPro;

public class RegistrationWindow : MonoBehaviour
{
    [SerializeField]
    TMP_InputField mUserName, mPassword, mPasswordConfirm,mEmail,mAge,mFullName;
    [SerializeField]
    GameObject mUserNameHint, mPasswordHint, mPasswordConfirmHint, mEmailHint, mAgeHint, mFullNameHint;
    [SerializeField]
    Button mRegisterButton;
    [SerializeField]
    GameObject mLoadingIndicator;

    [SerializeField]
    UnityEvent OnRegistrationAttempt, OnRegistrationFaile, OnRegistrationSuccess;


    public bool registerResult = true;

    // Start is called before the first frame update
    void Start()
    {
        mRegisterButton.onClick.AddListener(OnRegistrationClick);
        mLoadingIndicator.SetActive(false);

        OnRegistrationAttempt.AddListener(OnRegistrationAttemptAux);
        OnRegistrationFaile.AddListener(OnRegistrationFaileAux);
        OnRegistrationSuccess.AddListener(OnRegistrationSuccessAux);

        // disable hints
        mUserNameHint.SetActive(false);
        mPasswordHint.SetActive(false);
        mPasswordConfirmHint.SetActive(false); 
        mEmailHint.SetActive(false); 
        mAgeHint.SetActive(false); 
        mFullNameHint.SetActive(false);
    }

    private void OnDestroy()
    {
        mRegisterButton.onClick.RemoveListener(OnRegistrationClick);
        this.StopAllCoroutines();
    }

    void OnRegistrationClick()
    {
        Debug.Log("[Register |\nUserName : " + mUserName.text +
            ", Password : " + mPassword.text +
            ", PasswordConfirm : " + mPasswordConfirm.text +
            ", Email : " + mEmail.text +
            ", Age : " + mAge.text +
            ", FullName : " + mFullName.text
            + " ]");
        if (OnRegistrationAttempt != null)
            OnRegistrationAttempt.Invoke();

        if (!ValidInputs())
        {
            OnRegistrationFaileAux();
            return;
        }
        StartCoroutine(AsyncToRegistr(mUserName.text,
            mPassword.text,
            mEmail.text,
            mAge.text,
            mFullName.text,
            OnRegistrationSuccess,
            OnRegistrationFaile));
    }


    bool ValidInputs()
    {

        bool returnVal = true, temp;
        // validate the inputs
        mUserNameHint.SetActive(temp = !Validate(mUserName));
        returnVal = returnVal && !temp;
        Debug.Log("mUserName :" + returnVal);
        mUserName.text = mUserName.placeholder.gameObject.activeInHierarchy ?
            "" : mUserName.text;

        mPasswordHint.SetActive(temp = !Validate(mPassword));
        returnVal = returnVal && !temp;
        Debug.Log("mPassword :" + returnVal);
        mPassword.text = mPassword.placeholder.gameObject.activeInHierarchy ?
            "" : mPassword.text;

        mPasswordConfirmHint.SetActive(temp = (!Validate(mPasswordConfirm) && mPasswordConfirm.text == mPassword.text));
        returnVal = returnVal && !temp;
        Debug.Log("mPasswordConfirm :" + returnVal);
        mPassword.text = mPassword.placeholder.gameObject.activeInHierarchy ?
            "" : mPassword.text;

        mEmailHint.SetActive(temp = !Validate(mEmail));
        returnVal = returnVal && !temp;
        Debug.Log("mEmail :" + returnVal);
        mEmail.text = mEmail.placeholder.gameObject.activeInHierarchy ?
            "" : mEmail.text;

        mAgeHint.SetActive(temp = !Validate(mAge));
        returnVal = returnVal && !temp;
        Debug.Log("mAge :" + returnVal);
        mAge.text = mAge.placeholder.gameObject.activeInHierarchy ?
            "" : mAge.text;

        mFullNameHint.SetActive(temp = !Validate(mFullName));
        returnVal = returnVal && !temp;
        Debug.Log("mFullName :" + returnVal);
        mFullName.text = mFullName.placeholder.gameObject.activeInHierarchy ?
            "" : mFullName.text;

        return returnVal;

    }
    void OnRegistrationAttemptAux()
    {
        mLoadingIndicator.SetActive(true);
        mUserName.interactable = mPassword.interactable =
            mPasswordConfirm.interactable = mEmail.interactable =
            mAge.interactable = mFullName.interactable = false;
    }
    void OnRegistrationFaileAux()
    {
        Debug.LogError("Failed to register!");
        mLoadingIndicator.SetActive(false);
        mUserName.interactable = mPassword.interactable =
            mPasswordConfirm.interactable = mEmail.interactable =
            mAge.interactable = mFullName.interactable = true;
    }
    void OnRegistrationSuccessAux()
    {
        Debug.LogWarning("Successfuly registered!");

        mLoadingIndicator.SetActive(false);
        mUserName.interactable = mPassword.interactable =
            mPasswordConfirm.interactable = mEmail.interactable =
            mAge.interactable = mFullName.interactable = true;

    }
    private IEnumerator AsyncToRegistr(string userName,
        string password,
        string email,
        string age,
        string fullName,
        UnityEvent onSucess,
        UnityEvent onFaile)
    {
        //print("Login process starder" + Time.time);

        yield return new WaitForSeconds(2);

        bool result = Registr(userName, password, email, age, fullName);
        if (result && onSucess != null)
            onSucess.Invoke();
        else if (!result && onFaile != null)
            onFaile.Invoke();

        //print("Login process finished" + Time.time);
    }

    private bool Registr(string userName,
        string password,
        string email,
        string age,
        string fullName)
    {
        return registerResult;
    }


    public bool Validate(TMP_InputField i)
    {
        //Debug.Log("validating " + i.name + " ,text = " + i.text);
        if (string.IsNullOrEmpty(i.text))
            return false;
        Regex r;

        switch (i.contentType)
        {
            case TMP_InputField.ContentType.Standard:
                return true;

            case TMP_InputField.ContentType.Autocorrected:
                return true;

            case TMP_InputField.ContentType.IntegerNumber:
                int mInt;
                if (int.TryParse(i.text, out mInt))
                    return true;
                return false;
            case TMP_InputField.ContentType.DecimalNumber:
                r = new Regex(@"^[0-9]*(?:\.[0-9]*)?$");
                if (r.IsMatch(i.text))
                    return true;
                return false;

            case TMP_InputField.ContentType.Alphanumeric:
                r = new Regex("^[a-zA-Z0-9]*$");
                if (r.IsMatch(i.text))
                    return true;
                return false;
            case TMP_InputField.ContentType.Name:
                return true;

            case TMP_InputField.ContentType.EmailAddress:
                r = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                return r.IsMatch(i.text);

            case TMP_InputField.ContentType.Password:

                return i.text.Length > 6;
            case TMP_InputField.ContentType.Pin:
                return true;
            case TMP_InputField.ContentType.Custom:
                return true;
            default:
                return false;
        }

    }


}
