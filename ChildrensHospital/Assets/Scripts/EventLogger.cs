using UnityEngine;
using System.Collections;
using System;
using System.IO;

public class EventLogger  {

    private FileStream logFile;
    private StreamWriter logger;

    private static EventLogger instance = new EventLogger();
    public static EventLogger Instance
    {
        get
        {
            return instance;
        }
    }

    private string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\ReachVolumeGame";


    private EventLogger()
    {

        if (!System.IO.Directory.Exists(path))
        {
            System.IO.Directory.CreateDirectory(path);
        }
        logFile = new FileStream(path + "\\ReachVolume.log", FileMode.Append);
        logger = new StreamWriter(logFile);

        LogData("Game Started");
    }

    public void LogData(string logString)
    {
        logger.WriteLine("{0:s}, " + logString, DateTime.Now);
        logger.Flush();
    }
}
