using System;
using System.Collections.Generic;
using System.Net;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using ColossalFramework.IO;
using System.Collections;


namespace TwitchIntegrator
{
    public class TwitchNames
    {
        public List<string> nameArray;
        private static TwitchNames tn;
        private static bool initialized = false;
        private static int updateCycles;
        public Dictionary<int, string> roadNames, citizenNames, commercialNames, residentialNames, officeNames, industrialNames;
        private System.Random rnd;

        /*public static string[] industrial = { "Fabrik", "Fertigung", "Manufaktur", "Werk", "Industriepark" };
        public static string[] commercial = { "Laden", "Kauf", "Büdchen", "Zeile", "Supermarkt", "Markt", "Apotheke" };
        public static string[] residential = { "Haus", "Anwesen", "Schloss", "Palast", "Hütte", "Apartement" };
        public static string[] office = { "Consulting", "GmbH", "AG", "KG", "Kanzlei", "Notariat", "Streamingdienste", "ThinkTank", "Marketing", "Bank", "Versicherung" };
        public static string[] streets = { "Strasse", "Allee", "Weg", "Gasse" };*/

        public static void Initialize()
        {
            tn = new TwitchNames();
            initialized = true;
            updateCycles = 0;
            tn.UpdateNames();
        }

        public static void Update()
        {
            if (initialized && TwitchNamesSettings.updateRate != -1)
            {
                updateCycles++;

                if (updateCycles == TwitchNamesSettings.updateRate)
                {
                    tn.UpdateNames();
                    updateCycles = 0;
                }

            }
        }


        public static string GetRoadName(int id)
        {
            string name;
            int no;

            if (tn.roadNames.ContainsKey(id))
            {
                return tn.roadNames[id];
            } else
            {
                name = tn.GetRandomName();
                if (TwitchNamesSettings.streets.Count > 0) {
                    no = id % TwitchNamesSettings.streets.Count;
                    name += " " + TwitchNamesSettings.streets[no].ToString();
                }
                tn.roadNames.Add(id, name);
                return name;
            }
        }

        public static string GetCommercialName(int id)
        {
            string name;
            int no;

            if (tn.commercialNames.ContainsKey(id))
            {
                return tn.commercialNames[id];
            }
            else
            {

                name = tn.GetRandomName();
                if (TwitchNamesSettings.commercial.Count > 0)
                {
                    no = id % TwitchNamesSettings.commercial.Count;
                    name += "s " + TwitchNamesSettings.commercial[no].ToString();
                }
                tn.commercialNames.Add(id, name);
                return name;
            }
        }

        public static string GetIndustrialName(int id)
        {
            string name;
            int no;

            if (tn.industrialNames.ContainsKey(id))
            {
                return tn.industrialNames[id];
            }
            else
            {

                name = tn.GetRandomName();
                if (TwitchNamesSettings.industrial.Count > 0)
                {
                    no = id % TwitchNamesSettings.industrial.Count;
                    name += "s " + TwitchNamesSettings.industrial[no].ToString();
                }
                tn.industrialNames.Add(id, name);
                return name;
            }
        }

        public static string GetResidentialName(int id)
        {
            string name;
            int no;

            if (tn.residentialNames.ContainsKey(id))
            {
                return tn.residentialNames[id];
            }
            else
            {

                name = tn.GetRandomName();
                if (TwitchNamesSettings.residential.Count > 0)
                {
                    no = id % TwitchNamesSettings.residential.Count;
                    name += "s " + TwitchNamesSettings.residential[no].ToString();
                }
                tn.residentialNames.Add(id, name);
                return name;
            }
        }

        public static string GetOfficeName(int id)
        {
            string name;
            int no;

            if (tn.officeNames.ContainsKey(id))
            {
                return tn.officeNames[id];
            }
            else
            {

                name = tn.GetRandomName();
                if (TwitchNamesSettings.office.Count > 0)
                {
                    no = id % TwitchNamesSettings.office.Count;
                    name += "s " + TwitchNamesSettings.office[no].ToString();
                }
                tn.officeNames.Add(id, name);
                return name;
            }
        }

        public static string GetName(int id)
        {
            return tn.GetNameOfId(id);
        }


        public TwitchNames()
        {
            TwitchNamesSettings.init();
            TwitchNamesSettings.LoadConfig();

            this.nameArray = new List<string>();
            this.roadNames = new Dictionary<int, string>();
            this.commercialNames = new Dictionary<int, string>();
            this.industrialNames = new Dictionary<int, string>();
            this.residentialNames = new Dictionary<int, string>();
            this.officeNames = new Dictionary<int, string>();
            this.rnd = new System.Random();
        }


        public string GetRandomName()
        {
            if(nameArray.Count == 0)
            {
                return "NoViewer";
            }
            int id = rnd.Next(0, nameArray.Count);
            return nameArray[id];
        }

        public string GetNameOfId(int id)
        {

            if (id > nameArray.Count)
            {
                id = id % nameArray.Count;
            }
            return nameArray[id];
        }

        public void UpdateNames()
        {
            string name;
            string stream = this.GET();
            int index = stream.IndexOf("viewers");
            Match match = Regex.Match(stream.Substring(index), "\"([^\"]*)\"[,|\n\\]]");
            while (match.Success)
            {
                name = match.Groups[1].ToString();
                if (!this.nameArray.Contains(name))
                {
                    this.nameArray.Add(name);
                }
                match = match.NextMatch();
            }
            
        }


        private string GET()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://tmi.twitch.tv/group/user/" + TwitchNamesSettings.username + "/chatters");
            try
            {
                WebResponse response = request.GetResponse();
                using (Stream responseStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                    return reader.ReadToEnd();
                }
            }
            catch (WebException ex)
            {
                WebResponse errorResponse = ex.Response;
                using (Stream responseStream = errorResponse.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, Encoding.GetEncoding("utf-8"));
                    String errorText = reader.ReadToEnd();
                }
                throw;
            }
        }

    }
}
