using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using PS3Lib;
using System.Threading;
using DiscordRPC;
using System.Text.RegularExpressions;
using System.Net.Http;
using System.IO;

namespace PS3RPC
{
    public partial class Main : Form
    {

        private PS3API PS3 = new PS3API();
        private DiscordRpcClient rpcClient;
        private List<string[]> dbData = new List<string[]>();

        public Main()
        {

            checkDB();
            InitializeComponent();
            parseDb();
            rpcClient = new DiscordRpcClient("561898274072690698");
            rpcClient.Initialize();
            rpcClient.SetPresence(new RichPresence() { Details = "Disconnected" });
            connectionLabel.ForeColor = Color.Red;

        }

        public void checkDB()
        {

            if (!File.Exists(Path.GetFullPath("db"))) downloadDB();

        }

        public void downloadDB()
        {

            Console.WriteLine("Downloading DB");

            using (var client = new HttpClient())
            {

                HttpResponseMessage response = client.PostAsync("https://psndl.net/download-db", null).Result;

                if (!response.IsSuccessStatusCode)
                {

                    MessageBox.Show("Failed to download db, games may not show up correctly", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;

                }

                string content = response.Content.ReadAsStringAsync().Result;
                string[] lines = content.Split(new string[] { "\r\n", "\n\r", "\n", "\r" }, StringSplitOptions.None);

                using (StreamWriter file = new StreamWriter(Path.GetFullPath("db")))
                {

                    for (int i = 1; i < lines.Length; i++)
                    {

                        string[] rowData = lines[i].Split(';');
                        if (rowData.Length > 2)
                        {

                            file.WriteLine("{0};{1}", rowData[0], rowData[1]);

                        }

                    }

                }

            }

        }

        private void parseDb()
        {

            using (StreamReader file = new StreamReader(Path.GetFullPath("db")))
            {

                while (!file.EndOfStream)
                {

                    string[] rowData = file.ReadLine().Split(';');
                    dbData.Add(rowData);

                }

            }

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

            PS3.ChangeAPI(SelectAPI.ControlConsole);

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

            PS3.ChangeAPI(SelectAPI.TargetManager);

        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (button1.Text == "Disconnect")
            {

                PS3.DisconnectTarget();
                connectionLabel.Text = "Disconnected";
                connectionLabel.ForeColor = Color.Red;
                button1.Text = "Connect";
                rpcClient.SetPresence(new RichPresence() { Details = "Disconnected" });

            }
            else
            {

                if (PS3.ConnectTarget())
                {

                    connectionLabel.Text = "Connected";
                    connectionLabel.ForeColor = Color.Green;
                    button1.Text = "Disconnect";

                    new Thread(() => checkForData(PS3, rpcClient, dbData)).Start();

                }
                else
                {

                    MessageBox.Show("Failed to connect", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }

            }

        }

        public static void checkForData(PS3API PS3, DiscordRpcClient rpcClient, List<string[]> dbData)
        {

            rpcClient.SetPresence(new RichPresence()
            {
                
                Details = "Connected"

            });

            

            CCAPI ccapi = PS3.CCAPI;
            TMAPI tmapi = PS3.TMAPI;
            Regex gameRegex = new Regex(@"\/(.{4}[0-9]{5})\/");
            string lastAttachedGame = null;
            Int32 startTime;
            uint currentProcess;

            string getGame()
            {

                ccapi.GetProcessName(currentProcess, out string name);

                if (name.Contains("dev_bdvd")) return "Unknown Disc Game";

                Match match = gameRegex.Match(name);

                if (!match.Success)
                {

                    return null;

                }

                return match.Groups[1].Value;

            }

            string parseTitleID(string titleId)
            {

                if (titleId == null) return "Nothing";

                for (int i = 0; i < dbData.Count; i++)
                {

                    if (dbData[i][0].Equals(titleId)) return dbData[i][1];

                }

                return titleId;

            }

            while (true)
            {

                if (PS3.GetCurrentAPI() == SelectAPI.ControlConsole)
                {

                    if (ccapi.GetConnectionStatus() == 0) break;

                    ccapi.AttachProcess();
                    currentProcess = ccapi.GetAttachedProcess();

                    if (lastAttachedGame != getGame())
                    {

                        startTime = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
                        lastAttachedGame = getGame();
                        Console.WriteLine("Now playing {0}", getGame());

                        rpcClient.SetPresence(new RichPresence()
                        {

                            Details = parseTitleID(lastAttachedGame),
                            Timestamps = new Timestamps(Timestamps.FromUnixMilliseconds(Convert.ToUInt32(startTime)))

                        });

                    }

                }
                else break;

                Thread.Sleep(30000);

            }

        }

        private void connectionLabel_Click(object sender, EventArgs e)
        {

        }
    }

}
