using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using System.Collections;

public class ConsultarDatos: MonoBehaviour
{
    public TMP_InputField nombreUsuarioInput;
    public TextMeshProUGUI resultadoText;

    public void Consultar()
    {
        string nombreUsuario = nombreUsuarioInput.text;
        StartCoroutine(ConsultarDatosDesdeServidor(nombreUsuario));
    }

    IEnumerator ConsultarDatosDesdeServidor(string nombreUsuario)
    {
        string url = "http://localhost/Semana12ejercicio2/consultardatos_juego3.php";

        WWWForm form = new WWWForm();
        form.AddField("nombre_usuario", nombreUsuario);

        UnityWebRequest www = UnityWebRequest.Post(url, form);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log("Error al consultar los datos: " + www.error);
        }
        else
        {
            resultadoText.text = www.downloadHandler.text;
        }
    }
}
