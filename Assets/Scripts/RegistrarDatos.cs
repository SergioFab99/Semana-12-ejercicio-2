using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using System.Collections;

public class RegistrarDatos : MonoBehaviour
{
    public TMP_InputField nombreUsuarioInput;
    public TMP_Dropdown nivelInput;
    public TextMeshProUGUI puntajeText;

    public void JugarNivel()
    {
        string nombreUsuario = nombreUsuarioInput.text;
        int nivel = nivelInput.value + 1;
        int puntaje = Random.Range(1, 21); 

        puntajeText.text = "Puntaje: " + puntaje.ToString();

        if (!string.IsNullOrEmpty(nombreUsuario))
        {
            StartCoroutine(EnviarDatos(nombreUsuario, nivel, puntaje));
        }
        else
        {
            Debug.Log("Debe ingresar un nombre de usuario.");
        }
    }

    IEnumerator EnviarDatos(string nombreUsuario, int nivel, int puntaje)
    {
        string url = "http://localhost/Semana12ejercicio2/insertardatos_juego3.php";

        WWWForm form = new WWWForm();
        form.AddField("nombre_usuario", nombreUsuario);
        form.AddField("nivel", "Nivel " + nivel);
        form.AddField("puntaje", puntaje);

        UnityWebRequest www = UnityWebRequest.Post(url, form);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log("Error al registrar los datos: " + www.error);
        }
        else
        {
            Debug.Log("Datos registrados correctamente");
        }
    }
}
