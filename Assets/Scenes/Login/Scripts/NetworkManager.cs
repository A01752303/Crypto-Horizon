using System;
using System.Collections;
using System.Collections.Generic;
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

    [Serializable]
    public class usercompletedLevel
    {
        public int id_usuario;
        public int level_id;
        public int aciertos;
        public float tiempo_finalizacion;
    }

    [Serializable]
    public class progress
    {
        public int level_id;
        public float time;
    }

    [Serializable]
    public class ProgressResponse : Response
    {
        public List<progress> progress;
    }

    [Serializable]
    public class LeaderboardResponse : Response
    {
        public List<LeaderboardEntry> leaderboard;
    }

    [Serializable]
    public class LeaderboardEntry
    {
        public int Ranking;
        public string Username;
        public int TotalScore;
        public int TotalTime;
    }

    // ✅ Guardar progreso del jugador
    public void saveLevelCompleted(int id_usuario, int level_id, int aciertos, float tiempo_finalizacion, Action<Response> callback)
    {
        if (id_usuario == 0 || level_id == 0)
        {
            Debug.LogWarning("⚠️ Datos incompletos para guardar el nivel.");
            callback?.Invoke(new Response { done = false, message = "Datos incompletos", userId = 0 });
            return;
        }

        usercompletedLevel data = new usercompletedLevel
        {
            id_usuario = id_usuario,
            level_id = level_id,
            aciertos = aciertos,
            tiempo_finalizacion  = tiempo_finalizacion
        };

        string json = JsonUtility.ToJson(data);
        StartCoroutine(SendRequest("https://www.cryptohorizongame.org/saveLevelCompleted", json, callback));
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

    public void LoadUserProgress(int userId, Action<ProgressResponse> callback)
    {
        if (userId == 0)
        {
            Debug.LogWarning("⚠️ ID de usuario no válido.");
            callback?.Invoke(new ProgressResponse { done = false, message = "ID de usuario no válido", userId = 0 });
            return;
        }

        WWWForm form = new WWWForm();
        form.AddField("id_usuario", userId);

        StartCoroutine(SendProgressRequest("https://www.cryptohorizongame.org/getUserProgress", form, callback));
    }

    public void GetLeaderboard(Action<LeaderboardResponse> callback)
    {
        StartCoroutine(SendLeaderboardRequest("https://www.cryptohorizongame.org/generateLeaderboard", "", callback));
    }

    private IEnumerator SendLeaderboardRequest(string url, string json, Action<LeaderboardResponse> callback)
    {
        using UnityWebRequest request = new UnityWebRequest(url, "GET");
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

        LeaderboardResponse parsed = JsonUtility.FromJson<LeaderboardResponse>(responseText);
        callback?.Invoke(parsed);
    }

    private IEnumerator SendProgressRequest(string url, WWWForm form, Action<ProgressResponse> callback)
    {
        using (UnityWebRequest www = UnityWebRequest.Post(url, form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error fetching user progress: " + www.error);
                callback?.Invoke(new ProgressResponse { done = false, message = www.error });
            }
            else
            {
                string responseText = www.downloadHandler.text;
                ProgressResponse response = JsonUtility.FromJson<ProgressResponse>(responseText);
                callback?.Invoke(response);
            }
        }
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

    public void ReloadPage()
    {
        // Enviar un mensaje al navegador
        Application.ExternalEval("recargarGame();");
    }
}
