﻿using Crimelife;
using GTANetworkAPI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GVMP

{
    class FarmingModule : GVMP.Module.Module<FarmingModule>
    {
        public static List<FarmingModel> farming = new List<FarmingModel>();
        public static Dictionary<string, int> farmingPrices = new Dictionary<string, int>();

        protected override bool OnLoad()
        {
            var random = new Random();
            /*Farming.Add(new FarmingModel
            {
                Id = Farming.Count,
                Item_1 = "Hanfsamen",
                Item_2 = "Weed",
                Sammler = new Vector3(),
                PickupCount = 5,
                Verarbeiter = new Vector3(),
                RemoveCount = 40,
                AddCount = 5
            });*/
            farming.Add(new FarmingModel
            {
                Id = 1,
                Item_1 = "Trauben",
                Item_2 = "Traubensaft",
                Sammler = new Vector3(-1663.28, 2308.55, 56.19),
                PickupCount = 3,
                Verarbeiter = new Vector3(-1122.05, 2681.06, 18.6),
                RemoveCount = 6,
                AddCount = 2,
                ColShapeRange = 30
            });
            farming.Add(new FarmingModel
            {
                Id = 2,
                Item_1 = "Eisen",
                Item_2 = "Waffenteile",
                Sammler = new Vector3(2952.127, 2788.6, 40.38878),
                PickupCount = 3,
                Verarbeiter = new Vector3(1551.969, 2189.698, 77.74232),
                RemoveCount = 20,
                AddCount = 1,
                ColShapeRange = 30
            });
            farming.Add(new FarmingModel
            {
                Id = 3,
                Item_1 = "Aramidfasern",
                Item_2 = "Kevlar",
                Sammler = new Vector3(139.16, 1014.99, 225.88),
                PickupCount = 10,
                Verarbeiter = new Vector3(714, -965, 30),
                RemoveCount = 50,
                AddCount = 5,
                ColShapeRange = 30
            });

            foreach (FarmingModel farmingModel in farming)
            {
                ColShape c = NAPI.ColShape.CreateCylinderColShape(farmingModel.Sammler, farmingModel.ColShapeRange, 2.4f, 0);
                c.SetData("FUNCTION_MODEL", new FunctionModel("triggerFarming"));
                c.SetData("MESSAGE", new Message("Benutze E um zu farmen.", "FARMING", "green", 3000));

                ColShape ca = NAPI.ColShape.CreateCylinderColShape(farmingModel.Verarbeiter, 30f, 2.4f, 0);
                ca.SetData("FUNCTION_MODEL", new FunctionModel("triggerFarming"));
                ca.SetData("MESSAGE", new Message("Benutze E um zu verarbeiten.", "FARMING", "green", 3000));

                /*NAPI.Marker.CreateMarker(1, t.Sammler, new Vector3(), new Vector3(), t.ColShapeRange, new Color(255, 140, 0), false, 0);
                NAPI.Marker.CreateMarker(1, t.Verarbeiter, new Vector3(), new Vector3(), 3.7f, new Color(255, 140, 0), false, 0);*/
                NAPI.Blip.CreateBlip(501, farmingModel.Sammler, 1f, 0, farmingModel.Item_1 + "sammler", 255, 0, true, 0, 0);
                NAPI.Blip.CreateBlip(792, farmingModel.Verarbeiter, 1f, 0, farmingModel.Item_1 + "verarbeiter", 255, 0, true, 0, 0);
            }

            ColShape cb = NAPI.ColShape.CreateCylinderColShape(new Vector3(1465.892, 6547.677, 13.05419), 1.4f, 1.4f, 0);
            cb.SetData("FUNCTION_MODEL", new FunctionModel("showDrugMenu"));
            cb.SetData("MESSAGE", new Message("Benutze E um Waren zu verkaufen.", "DEALER", "green", 3000));

            NAPI.Marker.CreateMarker(1, new Vector3(1465.892, 6547.677, 13.05419), new Vector3(), new Vector3(), 1.0f, new Color(0, 154, 51), false, 0);
            NAPI.Blip.CreateBlip(280, new Vector3(1465.892, 6547.677, 13.05419), 1f, 1, "Dealer", 255, 0, true, 0, 0);
            NAPI.Blip.CreateBlip(617, new Vector3(-626.77, -235.5, 38.06), 1f, 74, "Juwelier", 255, 0, true, 0, 0);
            NAPI.Blip.CreateBlip(66, new Vector3(390.17, -356.3, 48.02), 1f, 74, "Geschenk", 255, 0, true, 0, 0);

           


            //farmingPrices.Add("Traubensaft", (int)random.Next(6057, 10349));
            farmingPrices.Add("Traubensaft", (int)random.Next(9057, 12349));
            //farmingPrices.Add("Weed", (int)random.Next(5473, 9486));
            farmingPrices.Add("Gold", (int)random.Next(14500, 33000));
            farmingPrices.Add("Juwel", (int)random.Next(16520, 39400));

            return true;
        }

        [RemoteEvent("triggerFarming")]
        public static void triggerFarming(Client c)
        {
            try
            {
                if (c == null) return;
                DbPlayer dbPlayer = c.GetPlayer();
                if (dbPlayer == null || !dbPlayer.IsValid(true) || dbPlayer.Client == null) return;

                dbPlayer.IsFarming = !dbPlayer.IsFarming;
                dbPlayer.RefreshData(dbPlayer);

                dbPlayer.disableAllPlayerActions(dbPlayer.IsFarming);

                if (dbPlayer.IsFarming)
                {
                    NAPI.Player.StopPlayerAnimation(c);
                    NAPI.Player.PlayPlayerAnimation(c, 33, "anim@mp_snowball", "pickup_snowball");
                }
                else
                {
                    NAPI.Player.StopPlayerAnimation(c);
                }
            }
            catch (Exception ex)
            {
                Logger.Print("[EXCEPTION EndGangwar] " + ex.Message);
                Logger.Print("[EXCEPTION EndGangwar] " + ex.StackTrace);
            }
        }

        public override void OnFiveSecUpdate()
        {
            try
            {
                foreach (FarmingModel farmingModel in farming)
                {
                    foreach (DbPlayer dbPlayer in PlayerHandler.GetPlayers())
                    {
                        Client client = dbPlayer.Client;
                        if (client == null) return;
                        if (client.Position.DistanceTo(farmingModel.Sammler) <= farmingModel.ColShapeRange)
                        {
                            if (dbPlayer.IsFarming)
                            {
                                dbPlayer.UpdateInventoryItems(farmingModel.Item_1, farmingModel.PickupCount, false);
                                dbPlayer.SendNotification(
                                    $"Du hast {farmingModel.PickupCount}x {farmingModel.Item_1} gesammelt.", 3000,
                                    "green", "FARMING");
                            }
                        }
                        else if (client.Position.DistanceTo(farmingModel.Verarbeiter) <= 30.0f)
                        {
                            if (dbPlayer.IsFarming)
                            {
                                if (dbPlayer.GetItemAmount(farmingModel.Item_1) >= farmingModel.RemoveCount)
                                {
                                    dbPlayer.UpdateInventoryItems(farmingModel.Item_1, farmingModel.RemoveCount, true);
                                    dbPlayer.UpdateInventoryItems(farmingModel.Item_2, farmingModel.AddCount, false);
                                    dbPlayer.SendNotification(
                                        $"Du hast {farmingModel.RemoveCount}x {farmingModel.Item_1} zu {farmingModel.AddCount}x {farmingModel.Item_2} verarbeitet.",
                                        3000, "green", "FARMING");
                                }
                                else
                                {
                                    dbPlayer.SendNotification("Du besitzt nicht genug " + farmingModel.Item_1 + "!",
                                        3000, "red");
                                    triggerFarming(client);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Print("[EXCEPTION OnTenSecUpdate] " + ex.Message);
                Logger.Print("[EXCEPTION OnTenSecUpdate] " + ex.StackTrace);
            }
        }

        [RemoteEvent("showDrugMenu")]
        public void showDrugMenu(Client c)
        {
            try
            {
                if (c == null) return;
                DbPlayer dbPlayer = c.GetPlayer();
                if (dbPlayer == null || !dbPlayer.IsValid(true) || dbPlayer.Client == null) return;

                List<NativeItem> nativeItems = new List<NativeItem>();

                foreach (FarmingModel farmingModel in farming)
                {
                    if (farmingPrices.ContainsKey(farmingModel.Item_2))
                        nativeItems.Add(new NativeItem(
                            "1x " + farmingModel.Item_2 + " - " + farmingPrices[farmingModel.Item_2].ToDots() + "$",
                            farmingModel.Item_2));
                }

                nativeItems.Add(new NativeItem("10x Meth - 14.000$", "Meth"));

                NativeMenu nativeMenu = new NativeMenu("Dealer", "", nativeItems);
                dbPlayer.ShowNativeMenu(nativeMenu);
            }
            catch (Exception ex)
            {
                Logger.Print("[EXCEPTION showDrugMenu] " + ex.Message);
                Logger.Print("[EXCEPTION showDrugMenu] " + ex.StackTrace);
            }
        }

        [RemoteEvent("nM-Dealer")]
        public void DrogenDealer(Client c, string value)
        {
            try
            {
                DbPlayer dbPlayer = c.GetPlayer();
                if (dbPlayer == null || !dbPlayer.IsValid(true) || dbPlayer.Client == null) return;

                if (value == "Meth")
                {
                    if (dbPlayer.GetItemAmount("Meth") >= 10)
                    {
                        dbPlayer.UpdateInventoryItems("Meth", 10, true);
                        dbPlayer.addMoney(14000 * 10);
                        dbPlayer.SendNotification("Du hast 10x Meth verkauft.", 3000, "green", "DEALER");
                    }
                    else
                    {
                        dbPlayer.SendNotification("Du besitzt nicht genug Meth!", 3000, "red");
                    }
                }
                if (value == "Traubensaft")
                {
                    if (dbPlayer.GetItemAmount("Traubensaft") >= 1)
                    {
                        dbPlayer.UpdateInventoryItems("Traubensaft", 1, true);
                        dbPlayer.addMoney(farmingPrices["Traubensaft"]);
                        dbPlayer.SendNotification("Du hast 1x Traubensaft verkauft.", 3000, "green", "DEALER");
                    }
                    else
                    {
                        dbPlayer.SendNotification("Du besitzt nicht genug Traubensäfte!", 3000, "red");
                    }
                }
                if (value == "Juwel")
                {
                    if (dbPlayer.GetItemAmount("Juwel") >= 1)
                    {
                        dbPlayer.UpdateInventoryItems("Juwel", 1, true);
                        dbPlayer.addMoney(farmingPrices["Juwel"]);
                        dbPlayer.SendNotification("Du hast 1x Juwel verkauft.", 3000, "green", "DEALER");
                    }
                    else
                    {
                        dbPlayer.SendNotification("Du besitzt nicht genug Juwel!", 3000, "red");
                    }
                }
                if (value == "Gold")
                {
                    if (dbPlayer.GetItemAmount("Gold") >= 1)
                    {
                        dbPlayer.UpdateInventoryItems("Gold", 1, true);
                        dbPlayer.addMoney(farmingPrices["Gold"]);
                        dbPlayer.SendNotification("Du hast 1x Gold verkauft.", 3000, "green", "DEALER");
                    }
                    else
                    {
                        dbPlayer.SendNotification("Du besitzt nicht genug Gold!", 3000, "red");
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Print("[EXCEPTION Dealer] " + ex.Message);
                Logger.Print("[EXCEPTION Dealer] " + ex.StackTrace);
            }
        }
    }
}
