using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace GVMP
{
    class WebhookSender : GVMP.Module.Module<WebhookSender>
    {
        public static async void SendMessage(string title, string msg, string webhook, string type)
        {
            try
            {
                DateTime now = DateTime.Now;
                string[] strArray = new string[20];
                strArray[0] = "{\"username\":\"Vitrox\",\"avatar_url\":\"https://cdn.discordapp.com/attachments/950499981838667847/996860000888684664/Komp_1_1.gif\",\"content\":\"\",\"embeds\":[{\"author\":{\"name\":\"Vitrox Crimelife\",\"url\":\"https://discord.gg/QRHWTGjXSn\",\"icon_url\":\"https://cdn.discordapp.com/attachments/950499981838667847/996860000888684664/Komp_1_1.gif\"},\"title\":\"" + type + "\",\"thumbnail\":{\"url\":\"https://cdn.discordapp.com/attachments/950499981838667847/996860000888684664/Komp_1_1.gif\"},\"url\":\"https://discord.gg/QRHWTGjXSn\",\"description\":\"Es wurde am **";
                int num = now.Day;
                strArray[1] = num.ToString();
                strArray[2] = ".";
                num = now.Month;
                strArray[3] = num.ToString();
                strArray[4] = ".";
                num = now.Year;
                strArray[5] = num.ToString();
                strArray[6] = " | ";
                num = now.Hour;
                strArray[7] = num.ToString();
                strArray[8] = ":";
                num = now.Minute;
                strArray[9] = num.ToString();
                strArray[10] = "** ein " + type + " ausgelöst.\",\"color\":1127128,\"fields\":[{\"name\":\"";
                strArray[11] = title;
                strArray[12] = "\",\"value\":\"";
                strArray[13] = msg;
                strArray[14] = "\",\"inline\":true}],\"footer\":{\"text\":\"Vitrox Crimelife | " + type + " Bot (c) 2021\"}}]}";
                string stringPayload = string.Concat(strArray);
                StringContent httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");
                using (HttpClient httpClient = new HttpClient())
                {
                    HttpResponseMessage httpResponseMessage = await httpClient.PostAsync(webhook, (HttpContent)httpContent);
                }
                stringPayload = (string)null;
                httpContent = (StringContent)null;
            }
            catch (Exception ex)
            {
                Logger.Print("[EXCEPTION SendMessage] " + ex.Message);
                Logger.Print("[EXCEPTION SendMessage] " + ex.StackTrace);
            }
        }

        public static async void SendMessage423(string title, string msg, string webhook, string type)
        {
            try
            {
                DateTime now = DateTime.Now;
                string[] strArray = new string[20];
                strArray[0] = "{\"username\":\"NEXUS\",\"avatar_url\":\"https://cdn.discordapp.com/attachments/824761396952432640/866878215456030790/dark-logo_1.png\",\"content\":\"\",\"embeds\":[{\"author\":{\"name\":\"Nexus Crimelife\",\"url\":\"https://discord.gg/nexuscrimelife\",\"icon_url\":\"https://cdn.discordapp.com/attachments/824761396952432640/866878215456030790/dark-logo_1.png\"},\"title\":\"" + type + "\",\"thumbnail\":{\"url\":\"https://cdn.discordapp.com/attachments/824761396952432640/866878215456030790/dark-logo_1.png\"},\"url\":\"https://discord.gg/nexuscrimelife\",\"description\":\"Es wurde am **";
                int num = now.Day;
                strArray[1] = num.ToString();
                strArray[2] = ".";
                num = now.Month;
                strArray[3] = num.ToString();
                strArray[4] = ".";
                num = now.Year;
                strArray[5] = num.ToString();
                strArray[6] = " | ";
                num = now.Hour;
                strArray[7] = num.ToString();
                strArray[8] = ":";
                num = now.Minute;
                strArray[9] = num.ToString();
                strArray[10] = "** ein " + type + " ausgelöst.\",\"color\":1127128,\"fields\":[{\"name\":\"";
                strArray[11] = title;
                strArray[12] = "\",\"value\":\"";
                strArray[13] = msg;
                strArray[14] = "\",\"inline\":true}],\"footer\":{\"text\":\"Nexus Crimelife | " + type + " Bot (c) 2021\"}}]}";
                string stringPayload = string.Concat(strArray);
                StringContent httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");
                using (HttpClient httpClient = new HttpClient())
                {
                    HttpResponseMessage httpResponseMessage = await httpClient.PostAsync(webhook, (HttpContent)httpContent);
                }
                stringPayload = (string)null;
                httpContent = (StringContent)null;
            }
            catch (Exception ex)
            {
                Logger.Print("[EXCEPTION SendMessage] " + ex.Message);
                Logger.Print("[EXCEPTION SendMessage] " + ex.StackTrace);
            }
        }

        public static async void SendMessage321(string title, string msg, string webhook, string type)
        {
            try
            {
                DateTime now = DateTime.Now;
                string[] strArray = new string[20];
                strArray[0] = "{\"username\":\"NEXUS\",\"avatar_url\":\"https://cdn.discordapp.com/attachments/824761396952432640/866878215456030790/dark-logo_1.png\",\"content\":\"\",\"embeds\":[{\"author\":{\"name\":\"Nexus Crimelife\",\"url\":\"https://discord.gg/nexuscrimelife\",\"icon_url\":\"https://cdn.discordapp.com/attachments/824761396952432640/866878215456030790/dark-logo_1.png\"},\"title\":\"" + type + "\",\"thumbnail\":{\"url\":\"https://cdn.discordapp.com/attachments/824761396952432640/866878215456030790/dark-logo_1.png\"},\"url\":\"https://discord.gg/nexuscrimelife\",\"description\":\"Es wurde am **";
                int num = now.Day;
                strArray[1] = num.ToString();
                strArray[2] = ".";
                num = now.Month;
                strArray[3] = num.ToString();
                strArray[4] = ".";
                num = now.Year;
                strArray[5] = num.ToString();
                strArray[6] = " | ";
                num = now.Hour;
                strArray[7] = num.ToString();
                strArray[8] = ":";
                num = now.Minute;
                strArray[9] = num.ToString();
                strArray[10] = "** ein " + type + " ausgelöst.\",\"color\":1127128,\"fields\":[{\"name\":\"";
                strArray[11] = title;
                strArray[12] = "\",\"value\":\"";
                strArray[13] = msg;
                strArray[14] = "\",\"inline\":true}],\"footer\":{\"text\":\"Nexus Crimelife | " + type + " Bot (c) 2021\"}}]}";
                string stringPayload = string.Concat(strArray);
                StringContent httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");
                using (HttpClient httpClient = new HttpClient())
                {
                    HttpResponseMessage httpResponseMessage = await httpClient.PostAsync(webhook, (HttpContent)httpContent);
                }
                stringPayload = (string)null;
                httpContent = (StringContent)null;
            }
            catch (Exception ex)
            {
                Logger.Print("[EXCEPTION SendMessage] " + ex.Message);
                Logger.Print("[EXCEPTION SendMessage] " + ex.StackTrace);
            }
        }

        public static async void SendMessage123(string title, string msg, string webhook, string type)
        {
            try
            {
                DateTime now = DateTime.Now;
                string[] strArray = new string[20];
                strArray[0] = "{\"username\":\"NEXUS\",\"avatar_url\":\"https://cdn.discordapp.com/attachments/824761396952432640/866878215456030790/dark-logo_1.png\",\"content\":\"\",\"embeds\":[{\"author\":{\"name\":\"Nexus Crimelife\",\"url\":\"https://discord.gg/nexuscrimelife\",\"icon_url\":\"https://cdn.discordapp.com/attachments/824761396952432640/866878215456030790/dark-logo_1.png\"},\"title\":\"" + type + "\",\"thumbnail\":{\"url\":\"https://cdn.discordapp.com/attachments/824761396952432640/866878215456030790/dark-logo_1.png\"},\"url\":\"https://discord.gg/nexuscrimelife\",\"description\":\"Es wurde am **";
                int num = now.Day;
                strArray[1] = num.ToString();
                strArray[2] = ".";
                num = now.Month;
                strArray[3] = num.ToString();
                strArray[4] = ".";
                num = now.Year;
                strArray[5] = num.ToString();
                strArray[6] = " | ";
                num = now.Hour;
                strArray[7] = num.ToString();
                strArray[8] = ":";
                num = now.Minute;
                strArray[9] = num.ToString();
                strArray[10] = "** ein " + type + " ausgelöst.\",\"color\":1127128,\"fields\":[{\"name\":\"";
                strArray[11] = title;
                strArray[12] = "\",\"value\":\"";
                strArray[13] = msg;
                strArray[14] = "\",\"inline\":true}],\"footer\":{\"text\":\"Nexus Crimelife | " + type + " Bot (c) 2021\"}}]}";
                string stringPayload = string.Concat(strArray);
                StringContent httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");
                using (HttpClient httpClient = new HttpClient())
                {
                    HttpResponseMessage httpResponseMessage = await httpClient.PostAsync(webhook, (HttpContent)httpContent);
                }
                stringPayload = (string)null;
                httpContent = (StringContent)null;
            }
            catch (Exception ex)
            {
                Logger.Print("[EXCEPTION SendMessage] " + ex.Message);
                Logger.Print("[EXCEPTION SendMessage] " + ex.StackTrace);
            }
        }

        public static async void SendMessage534(string title, string msg, string webhook, string type)
        {
            try
            {
                DateTime now = DateTime.Now;
                string[] strArray = new string[20];
                strArray[0] = "{\"username\":\"NEXUS\",\"avatar_url\":\"https://cdn.discordapp.com/attachments/824761396952432640/866878215456030790/dark-logo_1.png\",\"content\":\"\",\"embeds\":[{\"author\":{\"name\":\"Nexus Crimelife\",\"url\":\"https://discord.gg/nexuscrimelife\",\"icon_url\":\"https://cdn.discordapp.com/attachments/824761396952432640/866878215456030790/dark-logo_1.png\"},\"title\":\"" + type + "\",\"thumbnail\":{\"url\":\"https://cdn.discordapp.com/attachments/824761396952432640/866878215456030790/dark-logo_1.png\"},\"url\":\"https://discord.gg/nexuscrimelife\",\"description\":\"Es wurde am **";
                int num = now.Day;
                strArray[1] = num.ToString();
                strArray[2] = ".";
                num = now.Month;
                strArray[3] = num.ToString();
                strArray[4] = ".";
                num = now.Year;
                strArray[5] = num.ToString();
                strArray[6] = " | ";
                num = now.Hour;
                strArray[7] = num.ToString();
                strArray[8] = ":";
                num = now.Minute;
                strArray[9] = num.ToString();
                strArray[10] = "** ein " + type + " ausgelöst.\",\"color\":1127128,\"fields\":[{\"name\":\"";
                strArray[11] = title;
                strArray[12] = "\",\"value\":\"";
                strArray[13] = msg;
                strArray[14] = "\",\"inline\":true}],\"footer\":{\"text\":\"Nexus Crimelife | " + type + " Bot (c) 2021\"}}]}";
                string stringPayload = string.Concat(strArray);
                StringContent httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");
                using (HttpClient httpClient = new HttpClient())
                {
                    HttpResponseMessage httpResponseMessage = await httpClient.PostAsync(webhook, (HttpContent)httpContent);
                }
                stringPayload = (string)null;
                httpContent = (StringContent)null;
            }
            catch (Exception ex)
            {
                Logger.Print("[EXCEPTION SendMessage] " + ex.Message);
                Logger.Print("[EXCEPTION SendMessage] " + ex.StackTrace);
            }
        }
    }
}
