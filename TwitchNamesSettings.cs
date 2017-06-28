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
            username = "";
            updateRate = -1;
            streets = new ArrayList();
            commercial = new ArrayList();
            industrial = new ArrayList();
            residential = new ArrayList();
            office = new ArrayList();

            string file = configFilePath + configFileName;

            if (!Directory.Exists(configFilePath))
            {
                Directory.CreateDirectory(configFilePath);
            }

            if (!FileUtils.Exists(file))
            {
                FileStream fs = File.Create(file);
                fs.Close();
                LoadDefaults();
                SaveConfig();
            }
        }

        public static void LoadConfig()
        {
            string file = configFilePath + configFileName;
            string fileText = System.IO.File.ReadAllText(file);

            string[] lines = fileText.Split('\n');

            foreach (string line in lines)
            {
                string[] tokens = line.Split(':');
                switch (tokens[0])
                {
                    case "user":
                        username = tokens[1].Trim();
                        break;
                    case "updateRate":
                        updateRate = int.Parse(tokens[1].Trim());
                        break;
                    case "roadAdds":
                        streets = GetArrayListFromString(tokens[1]);
                        break;
                    case "industrialAdds":
                        industrial = GetArrayListFromString(tokens[1]);
                        break;
                    case "commercialAdds":
                        commercial = GetArrayListFromString(tokens[1]);
                        break;
                    case "residentialAdds":
                        residential = GetArrayListFromString(tokens[1]);
                        break;
                    case "officeAdds":
                        office = GetArrayListFromString(tokens[1]);
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

        public static ArrayList GetArrayListFromString(string str)
        {
            ArrayList list = new ArrayList();
            string[] values = str.Split(',');
            for (int i = 0; i < values.Length; i++)
            {
                values[i] = values[i].Trim();
                list.Add(values[i]);
            }
            return list;
        }


        private static void LoadDefaults()
        {
            username = "gronkh";
            updateRate = 50000;
            streets.Add("Strasse");
            streets.Add("Weg");
            streets.Add("Allee");
            streets.Add("Gasse");
            commercial.Add("Laden");
            commercial.Add("Supermarkt");
            commercial.Add("Drogerie");
            commercial.Add("Apotheke");
            commercial.Add("Mode");
            commercial.Add("Elektroladen");        
            industrial.Add("Fabrik");
            industrial.Add("Ferigung");
            industrial.Add("Logistik");
            industrial.Add("Industriepark");
            residential.Add("Haus");
            residential.Add("Villa");
            residential.Add("Anwesen");
            residential.Add("Hütte");
            office.Add("Büro");
            office.Add("Kanzlei");
            office.Add("Versicherung");
            office.Add("AG");
            office.Add("GmbH");
            office.Add("Streamingdienste");
        }

    }
}
