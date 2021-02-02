using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ManageLanguageMainScreen : MonoBehaviour
{
    //elements UI
    public GameObject playButton;
    public GameObject optionsButton;
    public GameObject creditsButton;
    public Text[] loginText;
    public Text hintLoginText;
    public Text[] registerText;
    public Text hintRegisterText;
    public Text limitRegisterText;

    public Text username;
    public Text name;
    public Text logoutText;

    //elementos BR
    public Sprite[] playButtonPT;
    public Sprite[] optionsButtonPT;
    public Sprite[] creditsButtonPT;


    private string successfullyRegister = "Registration successfully completed!";
    private string helloText = "Hello ";
    private string invalidUsername = "Invalid username";
    private string usernameNotRegistred = "Username not registered";

    private string defaultError = "Unable to login you at the moment. Try again later";

    private string usernameAlreadyRegistred = "Username already registered";
    private string usernameErrorConnection = "Network connection error, check your connection and try again";
    private string defaultErrorRegister = "Unable to register you at the moment. Try again later";

    private string errorPlay = "You must be logged to play.";

    public string getsuccessfullyRegister()
    {
        return successfullyRegister;
    }

    public string getHello()
    {
        return helloText;
    }

    public string getInvalidUsername()
    {
        return invalidUsername;
    }

    public string getUsernameNotRegistred()
    {
        return usernameNotRegistred;
    }

    public string getDefaultError()
    {
        return defaultError;
    }

    public string getUsernameAlreadyRegistred()
    {
        return usernameAlreadyRegistred;
    }
    public string getUsernameErrorConnection()
    {
        return usernameErrorConnection;
    }
    public string getDefaultErrorRegister()
    {
        return defaultErrorRegister;
    }

    public string getErrorPlay()
    {
        return errorPlay;
    }
    void Awake()
    {
        SpriteState sprState;
        if (Application.systemLanguage == SystemLanguage.Portuguese)
        {
            sprState.pressedSprite = playButtonPT[1];

            playButton.GetComponent<Image>().sprite = playButtonPT[0];
            playButton.GetComponent<Button>().spriteState = sprState;

            sprState.pressedSprite = optionsButtonPT[1];

            optionsButton.GetComponent<Image>().sprite = optionsButtonPT[0];
            optionsButton.GetComponent<Button>().spriteState = sprState;

            sprState.pressedSprite = creditsButtonPT[1];

            creditsButton.GetComponent<Image>().sprite = creditsButtonPT[0];
            creditsButton.GetComponent<Button>().spriteState = sprState;

            successfullyRegister = "Registro feito com sucesso!";
            helloText = "Olá ";
            invalidUsername = "Usuário inválido.";
            usernameNotRegistred = "Usuário não registrado.";
            defaultError = "Login indisponivel no momento. Tente novamente mais tarde.";
            usernameAlreadyRegistred = "Usuário já registrado.";
            usernameErrorConnection = "Erro na conexão, verifique sua conexão e tente novamente.";
            defaultErrorRegister = "Register indisponivel no momento. Tente novamente mais tarde.";
            errorPlay = "Voce deve estar logado para jogar";

            loginText[0].text = "Entrar";
            loginText[1].text = "Entrar";
            hintLoginText.text = "Digite o nome de usuário";
            hintRegisterText.text = "Escreva seu nome";
            registerText[0].text = "Registrar";
            registerText[1].text = "REGISTRAR";
            limitRegisterText.text = "(3-20 letras)";
            username.text = "Nome de usuário";
            name.text = "NOME";
            logoutText.text = "Sair";
        }
    }

}
