using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkManager : MonoBehaviour
{
    public static NetworkManager Instance { get; private set; }
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    [Serializable]
    public class UserData
    {
        public string username;
        public string email;
        public string pass;
        public string birthDate;
        public string gender;
        public string country;
        public string deviceModel;
        public string operatingSystem;
        public string platform;
        public string systemLanguage;
    }

    [Serializable]
    public class LoginData
    {
        public string username;
        public string pass;
        public string deviceModel;
        public string operatingSystem;
        public string platform;
        public string systemLanguage;
    }

    [Serializable]
    public class Response
    {
        public bool done;
        public string message;
        public int userId;
    }

    [Serializable]
    public class SessionData
    {
        public int id_usuario;
        public string startTime;
        public string endTime;
    }

    // ✅ Registro con todos los campos
    public void CreateUserExtended(string userName, string email, string pass, string birthDate, string gender, string country, string deviceModel, string operatingSystem, string platform, string systemLanguage, Action<Response> callback)
    {
        UserData data = new UserData
        {
            username = userName,
            email = email,
            pass = pass,
            birthDate = birthDate,
            gender = gender,
            country = country,
            deviceModel = deviceModel,
            operatingSystem = operatingSystem,
            platform = platform,
            systemLanguage = systemLanguage
        };

        string json = JsonUtility.ToJson(data);
        StartCoroutine(SendRequest("https://www.cryptohorizongame.org/createUser", json, callback));
    }

    // ✅ Login extendido con métricas invisibles
    public void LoginUserExtended(string username, string pass, string deviceModel, string operatingSystem, string platform, string systemLanguage, Action<Response> callback)
    {
        LoginData data = new LoginData
        {
            username = username,
            pass = pass,
            deviceModel = deviceModel,
            operatingSystem = operatingSystem,
            platform = platform,
            systemLanguage = systemLanguage
        };

        string json = JsonUtility.ToJson(data);
        StartCoroutine(SendRequest("https://www.cryptohorizongame.org/loginUser", json, callback));
    }

    // ✅ Guardar sesión del jugador
    public void SaveSession(int userId, string startTime, string endTime, Action<Response> callback)
    {
        if (userId == 0 || string.IsNullOrWhiteSpace(startTime) || string.IsNullOrWhiteSpace(endTime))
        {
            Debug.LogWarning("⚠️ Datos incompletos para guardar sesión.");
            callback?.Invoke(new Response { done = false, message = "Datos incompletos", userId = 0 });
            return;
        }

        string formattedStartTime = DateTime.Parse(startTime).ToString("yyyy-MM-dd HH:mm:ss");
        string formattedEndTime = DateTime.Parse(endTime).ToString("yyyy-MM-dd HH:mm:ss");

        SessionData session = new SessionData
        {
            id_usuario = userId,
            startTime = formattedStartTime,
            endTime = formattedEndTime
        };

        string json = JsonUtility.ToJson(session);
        StartCoroutine(SendRequest("https://www.cryptohorizongame.org/saveSession", json, callback));
    }

    // ✅ Petición HTTP POST genérica
    private IEnumerator SendRequest(string url, string json, Action<Response> callback)
    {
        using UnityWebRequest request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("❌ Error en la petición: " + request.error);
            yield break;
        }

        string responseText = request.downloadHandler.text;
        Debug.Log("📨 Respuesta del servidor: " + responseText);

        if (string.IsNullOrEmpty(responseText) || !responseText.Trim().StartsWith("{"))
        {
            Debug.LogError("⚠️ La respuesta no es JSON válido");
            yield break;
        }

        Response parsed = JsonUtility.FromJson<Response>(responseText);
        callback?.Invoke(parsed);
    }
}
