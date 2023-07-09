using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using Amazon.Polly;
using Amazon.Polly.Model;
using Amazon.Runtime;

public class TextToSpeech : MonoBehaviour
{
    [SerializeField] ItchCommentFetcher itchCommentFetcher;
    
    [SerializeField] AudioSource audioSource;
    string textToSpeak = "Hi, my name is Mark and I am making a video game about magnets.";

    string configPath;
    string audioOutputPath;

    void Awake()
    {
        string rootPath = System.IO.Path.GetFullPath(System.IO.Path.Combine(Application.dataPath, ".."));
        configPath = $"{rootPath}/config.json";
        audioOutputPath = $"{Application.persistentDataPath}/ttsaudio.mp3";
    }

    void OnEnable()
    {
        itchCommentFetcher.OnNewComment += HandleNewComment;
    }

    void OnDisable()
    {
        itchCommentFetcher.OnNewComment -= HandleNewComment;
    }

    private void HandleNewComment(ItchCommentFetcher.Comment comment)
    {
        textToSpeak = comment.content;
    }

    async Task Start()
    {
        if (!File.Exists(configPath))
        {
            Debug.LogError("Config file not found");
        }

        string json = File.ReadAllText(configPath);
        ConfigData config = JsonUtility.FromJson<ConfigData>(json);
        string awsAccessKey = config.awsAccessKey;
        string awsSecretKey = config.awsSecretKey;

        BasicAWSCredentials credentials = new BasicAWSCredentials(awsAccessKey, awsSecretKey);
        AmazonPollyClient client = new AmazonPollyClient(credentials, Amazon.RegionEndpoint.USWest2);

        SynthesizeSpeechRequest sreq = new SynthesizeSpeechRequest
        {
            Text = textToSpeak,
            Engine = Engine.Neural,
            VoiceId = VoiceId.Arthur,
            OutputFormat = OutputFormat.Mp3
        };

        SynthesizeSpeechResponse sres = null;

        sres = await client.SynthesizeSpeechAsync(sreq);

        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(audioOutputPath, AudioType.MPEG))
        {
            var op = www.SendWebRequest();
            while (!op.isDone) { await Task.Yield(); }

            AudioClip clip = DownloadHandlerAudioClip.GetContent(www);

            audioSource.clip = clip;
            audioSource.Play();
        }
    }

    private void WriteIntoFile(Stream stream)
    {
        using (FileStream fileStream = new FileStream(audioOutputPath, FileMode.Create))
        {
            byte[] buffer = new byte[8 * 1024];
            int bytesRead;

            while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
            {
                fileStream.Write(buffer, 0, bytesRead);
            }
        }
    }
}

[System.Serializable]
public class ConfigData
{
    public string awsAccessKey;
    public string awsSecretKey;
}
