using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTANetworkAPI;
using MySql.Data.MySqlClient;

namespace GVMP
{
    class SettingsApp : GVMP.Module.Module<SettingsApp>
    {
        List<Wallpaper> wallpapers = new List<Wallpaper>()
        {
            new Wallpaper(1, "Park", "https://i.imgur.com/Kw2VuCY.jpg"),
            new Wallpaper(2, "LCN", "https://cdn.discordapp.com/attachments/791769614152892436/927170144847953930/LCN.png"),
            new Wallpaper(3, "LOST", "https://i.imgur.com/JbY482X.png"),
            new Wallpaper(4, "LSPD", "https://i.imgur.com/TgQwuzE.png"),
            new Wallpaper(5, "Marabunta", "https://i.imgur.com/belPu9t.png"),
            new Wallpaper(6, "Midnight", "https://i.imgur.com/JVV9wMS.png"),
            new Wallpaper(7, "Pier", "https://i.imgur.com/GQQ40BV.jpg"),
            new Wallpaper(8, "Triaden", "https://cdn.discordapp.com/attachments/791769614152892436/927169563936821269/Triaden.png"),
            new Wallpaper(9, "Vagos", "https://cdn.discordapp.com/attachments/827144560562929704/925799535362601010/Screenshot_17.png"),
            new Wallpaper(10, "YakuZa", "https://i.imgur.com/5hoqvjH.png"),
            new Wallpaper(11, "Rose", "https://cdn.discordapp.com/attachments/791769614152892436/924356661253140580/7276.png"),
            new Wallpaper(12, "Sturmtruppler", "https://cdn.discordapp.com/attachments/791769614152892436/924352082817982485/hd-star-wars-wallpaper-whatspaper-7.jpg"),
            new Wallpaper(15, "FIB", "https://cdn.discordapp.com/attachments/827144560562929704/925799812056629298/Screenshot_38.png"),
            new Wallpaper(16, "Ballas", "https://cdn.discordapp.com/attachments/827144560562929704/925799567751016538/Screenshot_18.png"),
            new Wallpaper(17, "Rednecks", "https://cdn.discordapp.com/attachments/791769614152892436/927166492011819028/Rednecks.png"),
            new Wallpaper(19, "Orga", "https://cdn.discordapp.com/attachments/791769614152892436/927627751224119356/maxresdefault.png"),
            new Wallpaper(20, "AngeloCreed", "https://cdn.discordapp.com/attachments/791769614152892436/927241822483386418/unknown.png"),
            new Wallpaper(21, "Matteo1", "https://cdn.discordapp.com/attachments/997134037623771237/997138859148181574/matteo_cringe.PNG"),
            new Wallpaper(22, "Matteo2", "https://cdn.discordapp.com/attachments/950499981838667847/997139037812957265/152fbf76-d89c-4f65-8dcb-b01c14e792e3.jpg"),
            new Wallpaper(23, "~", "https://cdn.discordapp.com/attachments/915888915519520818/933114020653850624/IMB_NJePUD.gif"),
            new Wallpaper(24, "~", "https://cdn.discordapp.com/attachments/915888915519520818/933115633804148806/2019-05-Irina-Shayk-BTS-57-Edit.gif"),
            new Wallpaper(25, "~", "https://cdn.discordapp.com/attachments/915888915519520818/933115834803572766/original_30.gif"),
            new Wallpaper(26, "~", "https://cdn.discordapp.com/attachments/915888915519520818/933115932904132698/AcclaimedExcellentBufeo-size_restricted.gif"),
            new Wallpaper(27, "~", "https://cdn.discordapp.com/attachments/915888915519520818/933116496538898442/5FE4224F-5090-4E11-84F7-B0AC930D06E5.gif"),
            new Wallpaper(28, "~", "https://cdn.discordapp.com/attachments/915888915519520818/933116610456215582/diamond-watch.gif"),
            new Wallpaper(29, "~", "https://cdn.discordapp.com/attachments/910990703537041421/933390372971876422/a_d8e99b165b7f07254df42a987c2db109.gif"),
            new Wallpaper(30, "~", "https://cdn.discordapp.com/attachments/910990703537041421/933390409806266538/64d18f0e-cc08-412f-93b9-bf26865589cb.gif"),
            new Wallpaper(31, "~", "https://media.discordapp.net/attachments/818895731711803393/913816163417477140/girl38.gif")

        };


        [RemoteEvent("requestWallpaperList")]
        public void requestWallpaperList(Client c)
        {
            try
            {
                if (c == null) return;
                c.TriggerEvent("componentServerEvent", "SettingsEditWallpaperApp", "responseWallpaperList",
                    NAPI.Util.ToJson(wallpapers));
            }
            catch (Exception ex)
            {
                Logger.Print("[EXCEPTION requestWallpaperList] " + ex.Message);
                Logger.Print("[EXCEPTION requestWallpaperList] " + ex.StackTrace);
            }
        }

        public static void checkUserSettingsTable(Client c)
        {
            try
            {
                if (c == null) return;
                DbPlayer dbPlayer = c.GetPlayer();
                if (dbPlayer == null || !dbPlayer.IsValid(true) || dbPlayer.Client == null)
                    return;

                MySqlQuery mySqlQuery = new MySqlQuery("SELECT * FROM phone_settings WHERE Id = @userid LIMIT 1");
                mySqlQuery.Parameters = new List<MySqlParameter>()
                {
                    new MySqlParameter("@userid", dbPlayer.Id)
                };
                MySqlResult mySqlReaderCon = MySqlHandler.GetQuery(mySqlQuery);
                try
                {
                    MySqlDataReader reader = mySqlReaderCon.Reader;
                    if (!reader.HasRows)
                    {
                        reader.Dispose();
                        mySqlQuery.Query = "INSERT INTO phone_settings (Id) VALUES (@userid)";
                        mySqlQuery.Parameters = new List<MySqlParameter>()
                        {
                            new MySqlParameter("@userid", dbPlayer.Id)
                        };
                        MySqlHandler.ExecuteSync(mySqlQuery);
                    }
                }
                catch (Exception ex)
                {
                    Logger.Print("[EXCEPTION checkUserSettingsTable] " + ex.Message);
                    Logger.Print("[EXCEPTION checkUserSettingsTable] " + ex.StackTrace);
                }
                finally
                {
                    mySqlReaderCon.Connection.Dispose();
                }
            }
            catch (Exception ex)
            {
                Logger.Print("[EXCEPTION checkUserSettingsTable] " + ex.Message);
                Logger.Print("[EXCEPTION checkUserSettingsTable] " + ex.StackTrace);
            }
        }

        public static bool isFlugmodus(Client c)
        {
            if (c == null) return false;
            DbPlayer dbPlayer = c.GetPlayer();
            if (dbPlayer == null || !dbPlayer.IsValid(true) || dbPlayer.Client == null)
                return false;

            checkUserSettingsTable(c);
            MySqlQuery mySqlQuery = new MySqlQuery("SELECT * FROM phone_settings WHERE Id = @userid LIMIT 1");
            mySqlQuery.Parameters = new List<MySqlParameter>()
            {
                new MySqlParameter("@userid", dbPlayer.Id)
            };
            MySqlResult mySqlReaderCon = MySqlHandler.GetQuery(mySqlQuery);
            try
            {
                mySqlQuery.Query = "SELECT * FROM phone_settings WHERE Id = @userid LIMIT 1";
                mySqlQuery.Parameters = new List<MySqlParameter>()
                {
                    new MySqlParameter("@userid", dbPlayer.Id)
                };
                MySqlHandler.ExecuteSync(mySqlQuery);
                MySqlDataReader reader = mySqlReaderCon.Reader;
                try
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            return reader.GetInt32("Flugmodus") == 1;
                        }
                    }
                }
                finally
                {
                    mySqlReaderCon.Reader.Dispose();
                }
            }
            catch (Exception ex)
            {
                Logger.Print("[EXCEPTION isFlugmodus] " + ex.Message);
                Logger.Print("[EXCEPTION isFlugmodus] " + ex.StackTrace);
            }
            finally
            {
                mySqlReaderCon.Connection.Dispose();
            }
            return false;
        }

        public static bool blockCalls(Client c)
        {
            if (c == null) return false;
            DbPlayer dbPlayer = c.GetPlayer();
            if (dbPlayer == null || !dbPlayer.IsValid(true) || dbPlayer.Client == null)
                return false;

            checkUserSettingsTable(c);
            MySqlQuery mySqlQuery = new MySqlQuery("SELECT * FROM phone_settings WHERE Id = @userid LIMIT 1");
            mySqlQuery.Parameters = new List<MySqlParameter>()
            {
                new MySqlParameter("@userid", dbPlayer.Id)
            };
            MySqlResult mySqlReaderCon = MySqlHandler.GetQuery(mySqlQuery);
            try
            {
                mySqlQuery.Query = "SELECT * FROM phone_settings WHERE Id = @userid LIMIT 1";
                mySqlQuery.Parameters = new List<MySqlParameter>()
                {
                    new MySqlParameter("@userid", dbPlayer.Id)
                };
                MySqlHandler.ExecuteSync(mySqlQuery);
                MySqlDataReader reader = mySqlReaderCon.Reader;
                try
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            return reader.GetInt32("blockCalls") == 1;
                        }
                    }
                }
                finally
                {
                    mySqlReaderCon.Reader.Dispose();
                }
            }
            catch (Exception ex)
            {
                Logger.Print("[EXCEPTION blockCalls] " + ex.Message);
                Logger.Print("[EXCEPTION blockCalls] " + ex.StackTrace);
            }
            finally
            {
                mySqlReaderCon.Connection.Dispose();
            }
            return false;
        }

        [RemoteEvent("requestPhoneWallpaper")]
        public void requestPhoneWallpaper(Client c)
        {
            try
            {
                if (c == null) return;
                DbPlayer dbPlayer = c.GetPlayer();
                if (dbPlayer == null || !dbPlayer.IsValid(true) || dbPlayer.Client == null)
                    return;

                checkUserSettingsTable(c);
                MySqlQuery mySqlQuery = new MySqlQuery("SELECT * FROM phone_settings WHERE Id = @userid LIMIT 1");
                mySqlQuery.AddParameter("@userid", dbPlayer.Id);
                MySqlResult mySqlReaderCon = MySqlHandler.GetQuery(mySqlQuery);
                try
                {
                    MySqlDataReader reader = mySqlReaderCon.Reader;
                    try
                    {
                        if (!reader.HasRows)
                        {
                            mySqlQuery.Parameters.Clear();
                            mySqlQuery.Query = "INSERT INTO phone_settings (Id) VALUES (@userid)";
                            mySqlQuery.AddParameter("@userid", dbPlayer.Id);
                            MySqlHandler.ExecuteSync(mySqlQuery);
                        }
                        else
                        {
                            reader.Read();
                            Wallpaper wallpaper =
                                wallpapers.FirstOrDefault((Wallpaper wall) => wall.Id == reader.GetInt32("Wallpaper"));

                            if (wallpaper != null)
                                c.TriggerEvent("componentServerEvent", "HomeApp", "responsePhoneWallpaper",
                                    wallpaper.File);
                        }
                    }
                    finally
                    {
                        reader.Dispose();
                    }
                }
                catch (Exception ex)
                {
                    Logger.Print("[EXCEPTION requestPhoneWallpaper] " + ex.Message);
                    Logger.Print("[EXCEPTION requestPhoneWallpaper] " + ex.StackTrace);
                }
                finally
                {
                    mySqlReaderCon.Connection.Dispose();
                }
            }
            catch (Exception ex)
            {
                Logger.Print("[EXCEPTION requestPhoneWallpaper] " + ex.Message);
                Logger.Print("[EXCEPTION requestPhoneWallpaper] " + ex.StackTrace);
            }

        }

        [RemoteEvent("saveWallpaper")]
        public void saveWallpaper(Client c, int id)
        {
            if (c == null) return;
            DbPlayer dbPlayer = c.GetPlayer();
            if (dbPlayer == null || !dbPlayer.IsValid(true) || dbPlayer.Client == null)
                return;

            checkUserSettingsTable(c);
            try
            {
                MySqlQuery mySqlQuery = new MySqlQuery("UPDATE phone_settings SET Wallpaper = @val WHERE Id = @userid");
                mySqlQuery.AddParameter("@userid", dbPlayer.Id);
                mySqlQuery.AddParameter("@val", id);
                MySqlHandler.ExecuteSync(mySqlQuery);
            }
            catch (Exception ex)
            {
                Logger.Print("[EXCEPTION saveWallpaper] " + ex.Message);
                Logger.Print("[EXCEPTION saveWallpaper] " + ex.StackTrace);
            }
        }

        [RemoteEvent("requestPhoneSettings")]
        public void requestPhoneSettings(Client c)
        {
            if (c == null) return;
            DbPlayer dbPlayer = c.GetPlayer();
            if (dbPlayer == null || !dbPlayer.IsValid(true) || dbPlayer.Client == null)
                return;

            MySqlQuery mySqlQuery = new MySqlQuery("SELECT * FROM phone_settings WHERE Id = @userid LIMIT 1");
            mySqlQuery.AddParameter("@userid", dbPlayer.Id);
            MySqlResult mySqlReaderCon = MySqlHandler.GetQuery(mySqlQuery);
            try
            {
                MySqlDataReader reader = mySqlReaderCon.Reader;
                try
                {
                    if (!reader.HasRows)
                    {
                        mySqlQuery.Query = "INSERT INTO phone_settings (Id) VALUES (@userid)";
                        mySqlQuery.Parameters = new List<MySqlParameter>()
                        {
                            new MySqlParameter("@userid", dbPlayer.Id)
                        };
                        MySqlHandler.ExecuteSync(mySqlQuery);
                    }
                    else
                    {
                        reader.Read();
                        string boolstring = "true";
                        if (reader.GetInt32("Flugmodus") == 0)
                            boolstring = "false";

                        string boolstring2 = "true";
                        if (reader.GetInt32("blockCalls") == 0)
                            boolstring2 = "false";


                        c.TriggerEvent("componentServerEvent", "SettingsApp", "responsePhoneSettings", boolstring, boolstring2, boolstring2);
                    }
                }
                finally
                {
                    reader.Dispose();
                }
            }
            catch (Exception ex)
            {
                Logger.Print("[EXCEPTION requestPhoneSettings] " + ex.Message);
                Logger.Print("[EXCEPTION requestPhoneSettings] " + ex.StackTrace);
            }
            finally
            {
                mySqlReaderCon.Connection.Dispose();
            }
        }

        [RemoteEvent("savePhoneSettings")]
        public void savePhoneSettings(Client c, bool flugmodusStatus, bool lautlosStatus, bool anrufAblehnen)
        {
            if (c == null) return;
            DbPlayer dbPlayer = c.GetPlayer();
            if (dbPlayer == null || !dbPlayer.IsValid(true) || dbPlayer.Client == null)
                return;

            checkUserSettingsTable(c);
            try
            {
                if (Convert.ToInt32(flugmodusStatus) == 1)
                {
                    dbPlayer.SetSharedData("FUNK_CHANNEL", 0);
                    dbPlayer.SetSharedData("FUNK_TALKING", false);
                    //Logger.Print("Funk Reset");
                }
                MySqlQuery mySqlQuery = new MySqlQuery("UPDATE phone_settings SET Flugmodus = @val, blockCalls = @val2 WHERE Id = @userid");
                mySqlQuery.Parameters = new List<MySqlParameter>()
                {
                    new MySqlParameter("@userid", dbPlayer.Id),
                    new MySqlParameter("@val", Convert.ToInt32(flugmodusStatus)),
                    new MySqlParameter("@val2", Convert.ToInt32(anrufAblehnen))
                };
                MySqlHandler.ExecuteSync(mySqlQuery);
            }
            catch (Exception ex)
            {
                Logger.Print("[EXCEPTION savePhoneSettings] " + ex.Message);
                Logger.Print("[EXCEPTION savePhoneSettings] " + ex.StackTrace);
            }
        }
    }
}
