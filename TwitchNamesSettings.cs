using ColossalFramework.IO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace TwitchIntegrator
{
    class TwitchNamesSettings
    {
        public static string username;
        public static int updateRate;
        public static ArrayList industrial;
        public static ArrayList commercial;
        public static ArrayList residential;
        public static ArrayList office;
        public static ArrayList streets;

        private static string configFilePath = DataLocation.modsPath + "/TwitchIntegrator/";
        private static string configFileName = "TwitchIntegrator.conf";

        public static void init()
        {
            username = "gronkh";
            updateRate = 50000;
            streets = new ArrayList();
            streets.Add("Strasse");
            commercial = new ArrayList();
            commercial.Add("Laden");
            industrial = new ArrayList();
            industrial.Add("Fabrik");
            residential = new ArrayList();
            residential.Add("Haus");
            office = new ArrayList();
            office.Add("Büro");

            string file = configFilePath + configFileName;

            if (!FileUtils.Exists(file))
            {
                if (!Directory.Exists(configFilePath))
                {
                    Directory.CreateDirectory(configFilePath);
                }
                File.Create(file);
                SaveConfig();
            }
        }

        public static void LoadConfig()
        {
            string[] values;
            string file = configFilePath + configFileName;
            string fileText = System.IO.File.ReadAllText(file);

            string[] lines = fileText.Split('\n');

            foreach (string line in lines)
            {
                string[] tokens = line.Split(':');
                switch (tokens[0])
                {
                    case "user":
                        username = tokens[1];
                        break;
                    case "updateRate":
                        updateRate = int.Parse(tokens[1]);
                        break;
                    case "roadAdds":
                        values = tokens[1].Split(',');
                        streets = new ArrayList(values);
                        break;
                    case "industrialAdds":
                        values = tokens[1].Split(',');
                        industrial = new ArrayList(values);
                        break;
                    case "commercialAdds":
                        values = tokens[1].Split(',');
                        commercial = new ArrayList(values);
                        break;
                    case "residentialAdds":
                        values = tokens[1].Split(',');
                        residential = new ArrayList(values);
                        break;
                    case "officeAdds":
                        values = tokens[1].Split(',');
                        office = new ArrayList(values);
                        break;
                }
            }
        }

        public static void SaveConfig()
        {
            string file = configFilePath+configFileName;
            System.IO.File.WriteAllText(file, SettingsToText());
        }

        public static string SettingsToText()
        {
            string text = "";
            text += "user: " +username+"\n";
            text += "updateRate: " + updateRate + "\n";
            text += "roadAdds: " + GetStringRepresentation(streets) + "\n";
            text += "industrialAdds: " + GetStringRepresentation(industrial) + "\n";
            text += "commercialAdds: " + GetStringRepresentation(commercial) + "\n";
            text += "residentialAdds: "+GetStringRepresentation(residential)+"\n";
            text += "officeAdds: "+GetStringRepresentation(office)+"\n";

            return text;
        }

        public static string GetStringRepresentation(ArrayList list)
        {
            string text = "";
            for (int i = 0; i < list.Count; i++)
            {
                if (i != 0) text += ",";
                text += list[i];
            }

            return text;
        }

    }
}
