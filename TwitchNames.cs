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
        private string username;
        public List<string> nameArray;
        private static TwitchNames tn;
        private static bool initialized = false;
        private static int updateCycles;
        public Dictionary<int, string> roadNames, citizenNames, commercialNames, residentialNames, officeNames, industrialNames;
        private System.Random rnd;

        public static int updateRate;
        public static ArrayList industrial;
        public static ArrayList commercial;
        public static ArrayList residential;
        public static ArrayList office;
        public static ArrayList streets;

        /*public static string[] industrial = { "Fabrik", "Fertigung", "Manufaktur", "Werk", "Industriepark" };
        public static string[] commercial = { "Laden", "Kauf", "Büdchen", "Zeile", "Supermarkt", "Markt", "Apotheke" };
        public static string[] residential = { "Haus", "Anwesen", "Schloss", "Palast", "Hütte", "Apartement" };
        public static string[] office = { "Consulting", "GmbH", "AG", "KG", "Kanzlei", "Notariat", "Streamingdienste", "ThinkTank", "Marketing", "Bank", "Versicherung" };
        public static string[] streets = { "Strasse", "Allee", "Weg", "Gasse" };*/

        public static void Initialize(string user)
        {
            tn = new TwitchNames(user);
            initialized = true;
            updateCycles = 0;
            tn.UpdateNames();
        }

        public static void Update()
        {
            if (initialized && updateRate != -1)
            {
                updateCycles++;

                if (updateCycles == updateRate)
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
                if (streets.Count > 0) {
                    no = id % streets.Count;
                    name += " " + streets[no].ToString();
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
                if (commercial.Count > 0)
                {
                    no = id % commercial.Count;
                    name += "s " + commercial[no].ToString();
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
                if (industrial.Count > 0)
                {
                    no = id % industrial.Count;
                    name += "s " + industrial[no].ToString();
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
                if (residential.Count > 0)
                {
                    no = id % residential.Count;
                    name += "s " + residential[no].ToString();
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
                if (office.Count > 0)
                {
                    no = id % office.Count;
                    name += "s " + office[no].ToString();
                }
                tn.officeNames.Add(id, name);
                return name;
            }
        }

        public static string GetName(int id)
        {
            return tn.GetNameOfId(id);
        }


        public TwitchNames(string user)
        {
            this.username = user;
            this.nameArray = new List<string>();
            this.roadNames = new Dictionary<int, string>();
            this.commercialNames = new Dictionary<int, string>();
            this.industrialNames = new Dictionary<int, string>();
            this.residentialNames = new Dictionary<int, string>();
            this.officeNames = new Dictionary<int, string>();
            this.rnd = new System.Random();
            streets = new ArrayList();
            commercial = new ArrayList();
            industrial = new ArrayList();
            residential = new ArrayList();
            office = new ArrayList();

            string file =  DataLocation.modsPath + "/TwitchIntegrator/config.txt";
            System.Object obj;

            if (FileUtils.Exists(file))
            {
                string fileText = System.IO.File.ReadAllText(file);
                obj = ColossalFramework.HTTP.JSON.JsonDecode(fileText);

                if (obj != null)
                {
                    Hashtable dictionary = (Hashtable)obj;
                    this.username = dictionary["user"].ToString();
                    streets = (ArrayList)dictionary["roadAdds"];
                    industrial = (ArrayList)dictionary["industrialAdds"];
                    commercial = (ArrayList)dictionary["commercialAdds"];
                    residential = (ArrayList)dictionary["residentialAdds"];
                    office = (ArrayList)dictionary["officeAdds"];

                    if (dictionary.ContainsKey("updateRate"))
                    {
                        updateRate = int.Parse(dictionary["updateRate"].ToString());
                    } else
                    {
                        updateRate = 50000;
                    }

                }


            }
            else
            {
                throw new Exception("KiwiPloetzeTwitchNames: Can't find config file!!!!!");
            }
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
            Match match = Regex.Match(stream.Substring(index), "\"([^\"]*)\",");
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
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://tmi.twitch.tv/group/user/" + username + "/chatters");
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
