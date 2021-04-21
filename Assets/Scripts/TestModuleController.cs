using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class TestModuleController : MonoBehaviour
{

    [SerializeField]
    Button SendResultButton;

    [SerializeField]
    Button ExitButton;

    const string module_name = "TestModule";

    void Start()
    {
        Debug.Log("In Start");

        SendResultButton.onClick.AddListener(SendResult);
        ExitButton.onClick.AddListener(OnExitClick);
    }

    void Update()
    {
        
    }

    void OnExitClick() {
        Debug.Log("Quitting");
        Application.Quit();
    }

    void SendResult()
    {
        Dictionary<string, string> arguments = GetNamedArgumentsFromConsole();

        Dictionary<string, string> results = CalculateResults(arguments);

        SendResultsWithNamedPipes(results);
    }

    public Dictionary<string, string> CalculateResults(Dictionary<string, string> arguments)
    {
        Dictionary<string, string> res = new Dictionary<string, string>();
        string first_str;
        arguments.TryGetValue("first", out first_str);

        string second_str;
        arguments.TryGetValue("second", out second_str);

        int first;
        int second;
        int.TryParse(first_str, out first);

        int.TryParse(second_str, out second);
        res.Add("mult", (first * second).ToString());
        res.Add("div", ((double)((double)first / (double)second)).ToString());
        return res;
    }

    public void SendResultsWithNamedPipes(Dictionary<string, string> results)
    {
        using (NamedPipeClientStream pipeClient =
            new NamedPipeClientStream(".", "AlphaS", PipeDirection.Out))
        {
            pipeClient.Connect();

            Debug.Log("Client connected.");
            try
            {
                using (StreamWriter sw = new StreamWriter(pipeClient))
                {
                    sw.WriteLine(module_name);
                    sw.AutoFlush = true;
                    foreach (KeyValuePair<string, string> pair in results)
                    {
                        Debug.Log("Sending line to pipe : " + pair.Key + "=" + pair.Value);
                        sw.WriteLine(pair.Key + "=" + pair.Value);
                    }
                    sw.WriteLine("End");
                    pipeClient.WaitForPipeDrain();
                    pipeClient.Close();
                    Debug.Log("Done writeing results.");

                }
            }
            catch (IOException e)
            {
                Debug.Log(e);
            }
        }
    }

    public Dictionary<string, string> GetNamedArgumentsFromConsole()
    {
        string[] args = Environment.GetCommandLineArgs();

        var arguments = new Dictionary<string, string>();

        foreach (string argument in args)
        {
            Debug.Log("argument: " + argument);
            string[] splitted = argument.Split('=');
            
            if (splitted.Length == 2)
            {
                arguments[splitted[0]] = splitted[1];
                Debug.Log("Adding new argument: " + splitted[0] + " : " + splitted[1]);
            }

        }

        return arguments;
    }




}
