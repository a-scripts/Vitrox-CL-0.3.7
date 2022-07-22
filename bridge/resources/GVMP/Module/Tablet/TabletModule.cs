using GTANetworkAPI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GVMP
{
    class TabletModule : GVMP.Module.Module<TabletModule>
    {
        public static List<Ticket> Tickets = new List<Ticket>();
        // public static List<VehicleData> vehicleList = new List<VehicleData>();
        public static List<AcceptedTicket> AcceptedTickets = new List<AcceptedTicket>();

        [RemoteEvent("closeIpad")]
        public void closeIpad(Client c)
        {
            try
            {
                if (c == null) return;
                DbPlayer dbPlayer = c.GetPlayer();
                if (dbPlayer == null || !dbPlayer.IsValid(true) || dbPlayer.Client == null)
                    return;

                if (dbPlayer.HasData("PLAYER_ADUTY") && ((bool)dbPlayer.GetData("PLAYER_ADUTY")) == true)
                {
                    c.TriggerEvent("closeIpad");
                }
            }
            catch (Exception ex)
            {
                Logger.Print("[EXCEPTION closeIpad] " + ex.Message);
                Logger.Print("[EXCEPTION closeIpad] " + ex.StackTrace);
            }
        }

        [RemoteEvent("IpadCheck")]
        public void IpadCheck(Client c)
        {
            try
            {
                if (c == null) return;
                DbPlayer dbPlayer = c.GetPlayer();
                if (dbPlayer == null || !dbPlayer.IsValid(true) || dbPlayer.Client == null)
                    return;

                if (dbPlayer.HasData("PLAYER_ADUTY") && ((bool)dbPlayer.GetData("PLAYER_ADUTY")) == true)
                {
                    c.TriggerEvent("openIpad");
                    c.TriggerEvent("componentServerEvent", "IpadDesktopApp", "responseIpadApps",
                        "[{\"id\":1,\"appName\":\"SupportOverviewApp\", \"name\":\"Support\", \"icon\":\"204316.svg\"}, {\"id\":1,\"appName\":\"SupportVehicleApp\", \"name\":\"Fahrzeugsupport\", \"icon\":\"234788.svg\"}]" /*, {\"id\":2,\"appName\":\"SupportVehicleApp\",\"name\":\"Fahrzeugsupport\",\"icon\":\"234788.svg\"}]"*/);
                }
            }
            catch (Exception ex)
            {
                Logger.Print("[EXCEPTION IpadCheck] " + ex.Message);
                Logger.Print("[EXCEPTION IpadCheck] " + ex.StackTrace);
            }
        }

        [RemoteEvent("requestOpenSupportTickets")]
        public void requestOpenSupportTickets(Client c)
        {
            try
            {
                if (c == null) return;
                DbPlayer dbPlayer = c.GetPlayer();
                if (dbPlayer == null || !dbPlayer.IsValid(true) || dbPlayer.Client == null)
                    return;

                if (!dbPlayer.HasData("PLAYER_ADUTY") || !((bool)dbPlayer.GetData("PLAYER_ADUTY"))) return;
                c.TriggerEvent("componentServerEvent", "SupportOpenTickets", "responseOpenTicketList",
                    NAPI.Util.ToJson(Tickets));
            }
            catch (Exception ex)
            {
                Logger.Print("[EXCEPTION requestOpenSupportTickets] " + ex.Message);
                Logger.Print("[EXCEPTION requestOpenSupportTickets] " + ex.StackTrace);
            }
        }


        [RemoteEvent("requestSupportVehicleList")]
        public void requestSupportVehicleList(Client p, string owner)
        {

            DbPlayer dbPlayer = p.GetPlayer();

            int primary = 0;
            bool primary2 = int.TryParse(owner, out primary);
            if (primary == null) return;
            if (primary == 1) return;
            if (!primary2)
            {
                dbPlayer.SendNotification("Gebe bitte eine gültige Spieler ID an! /id IngameName", 5000, "red", "FEHLER!");
                return;
            }

            try
            {

                if (owner == null) return;

                List<VehicleData> list = new List<VehicleData>();
                foreach (GarageVehicle garageVehicles in MySqlManager.GetAllVehicles())
                {
                    if (garageVehicles == null) return;
                    if (primary == garageVehicles.OwnerID)
                    {


                        VehicleData item = new VehicleData
                        {
                            Id = garageVehicles.Id,
                            InGarage = garageVehicles.Parked,
                            Garage = garageVehicles.Garage,
                            Vehiclehash = garageVehicles.Name
                        };
                        list.Add(item);

                        {

                            p.TriggerEvent("componentServerEvent", new object[3] {
                "SupportVehicleList",
                "responseVehicleList",
                NAPI.Util.ToJson(list)
            });
                        }
                        //return;
                    }

                }
            }
            catch (Exception ex)
            {
                DbPlayer dbPlayer3 = p.GetPlayer();
                dbPlayer3.SendNotification("Gebe bitte eine gültigen ID an!", 3000, "red", "FEHLER!");
                Logger.Print("[EXCEPTION TabletFahrzeug] " + ex.Message);

                Logger.Print("[EXCEPTION TabletFahrzeug] " + ex.StackTrace);
            }
        }



        [RemoteEvent("acceptOpenSupportTicket")]
        public void acceptOpenSupportTicket(Client c, string t)
        {
            try
            {
                if (c == null) return;
                DbPlayer dbPlayer = c.GetPlayer();
                if (dbPlayer == null || !dbPlayer.IsValid(true) || dbPlayer.Client == null) return;
                if (!dbPlayer.HasData("PLAYER_ADUTY") || !((bool)dbPlayer.GetData("PLAYER_ADUTY"))) return;

                DbPlayer dbPlayer2 = PlayerHandler.GetPlayer(t);
                if (dbPlayer2 == null || !dbPlayer2.IsValid(true))
                {
                    dbPlayer.SendNotification("Da der Spieler offline ist, wurde das Ticket geschlossen.", 3000, "yellow", "SUPPORT");
                    Tickets.RemoveAll((ticket) => ticket.Creator == t);
                }

                Client client = dbPlayer2.Client;

                dbPlayer2.SendNotification(
                    "Dein Ticket wird nun von " + dbPlayer.Adminrank.Name + " " + dbPlayer.Name + " bearbeitet!", 3000,
                    "yellow", "SUPPORT");

                Ticket ticket = Tickets.FirstOrDefault((Ticket ticket2) => ticket2.Creator == t);
                if (ticket == null) return;

                var aticket = new AcceptedTicket
                {
                    Id = ticket.Id,
                    Creator = ticket.Creator,
                    Text = ticket.Text,
                    Admin = dbPlayer.Name,
                    Created = ticket.Created
                };
                dbPlayer.SendNotification("Ticket erfolgreich angenommen.", 3000, "yellow", "SUPPORT");

                if (!Tickets.Contains(ticket)) return;

                Tickets.Remove(ticket);
                AcceptedTickets.Add(aticket);

                WebhookSender.SendMessage("Admin akzeptiert Ticket",
                    "Der Spieler " + dbPlayer.Name + " hat das Ticket von " + ticket.Creator + " mit Grund " +
                    ticket.Text + " angenommen.", Webhooks.supportlogs, "Support");
            }
            catch (Exception ex)
            {
                Logger.Print("[EXCEPTION requestOpenSupportTickets] " + ex.Message);
                Logger.Print("[EXCEPTION requestOpenSupportTickets] " + ex.StackTrace);
            }
        }

        [RemoteEvent("requestAcceptedTickets")]
        public void requestAcceptedTickets(Client c)
        {
            try
            {
                if (c == null) return;
                DbPlayer dbPlayer = c.GetPlayer();
                if (dbPlayer == null || !dbPlayer.IsValid(true) || dbPlayer.Client == null)
                    return;

                if (!dbPlayer.HasData("PLAYER_ADUTY") || !(bool)dbPlayer.GetData("PLAYER_ADUTY"))
                    return;

                dbPlayer.TriggerEvent("componentServerEvent", "SupportAcceptedTickets", "responseAcceptedTicketList", NAPI.Util.ToJson(AcceptedTickets));

            }
            catch (Exception ex)
            {
                Logger.Print("[EXCEPTION requestAcceptedTickets] " + ex.Message);
                Logger.Print("[EXCEPTION requestAcceptedTickets] " + ex.StackTrace);
            }
        }

        [RemoteEvent("zuportTpToThePlayer")]
        public void zuportTpToThePlayer(Client c, string t)
        {
            try
            {
                if (c == null) return;
                DbPlayer dbPlayer = c.GetPlayer();
                if (dbPlayer == null || !dbPlayer.IsValid(true) || dbPlayer.Client == null)
                    return;
                Adminrank adminranks = dbPlayer.Adminrank;
                if (adminranks.Permission < 92)
                {
                    return;
                };
                DbPlayer dbPlayer2 = PlayerHandler.GetPlayer(t);
                if (dbPlayer2 == null || !dbPlayer2.IsValid(true))
                    return;

                Client client = dbPlayer2.Client;

                c.Dimension = client.Dimension;
                c.Position = client.Position;
                dbPlayer.SendNotification($"Du hast dich zu {t} teleportiert.", 3000, "red", "Support");
                dbPlayer2.SendNotification($"{dbPlayer.Adminrank.Name} {dbPlayer.Name} hat sich zu dir teleportiert.",
                    3000, "yellow", "Support");
            }
            catch (Exception ex)
            {
                Logger.Print("[EXCEPTION supportTeleportToPlayer] " + ex.Message);
                Logger.Print("[EXCEPTION supportTeleportToPlayer] " + ex.StackTrace);
            }
        }

        [RemoteEvent("zuortRavivathePleyer")]
        public void zuortRavivathePleyer(Client c, string t)
        {
            try
            {
                if (c == null) return;
                DbPlayer dbPlayer = c.GetPlayer();
                if (dbPlayer == null || !dbPlayer.IsValid(true) || dbPlayer.Client == null)
                    return;

                Adminrank adminranks = dbPlayer.Adminrank;
                if (adminranks.Permission < 92)
                {
                    return;
                };
                DbPlayer dbPlayer2 = PlayerHandler.GetPlayer(t);
                if (dbPlayer2 == null || !dbPlayer2.IsValid(true))
                    return;

                Client client = dbPlayer2.Client;

                dbPlayer2.SpawnPlayer(dbPlayer2.Client.Position);
                dbPlayer2.disableAllPlayerActions(false);
                dbPlayer2.SetAttribute("Death", 0);
                dbPlayer2.StopAnimation();
                dbPlayer2.SetInvincible(false);
                dbPlayer2.DeathData = new DeathData { IsDead = false, DeathTime = new DateTime(0) };
                dbPlayer2.StopScreenEffect("DeathFailOut");

                dbPlayer.SendNotification("Spieler " + dbPlayer2.Name + " revived!", 3000, "red", "Support");
                dbPlayer2.SendNotification(
                    "Du wurdest von " + dbPlayer.Adminrank.Name + " " + dbPlayer.Name + " revived!", 3000, "yellow",
                    "Support");
            }
            catch (Exception ex)
            {
                Logger.Print("[EXCEPTION supportRevivePlayer] " + ex.Message);
                Logger.Print("[EXCEPTION supportRevivePlayer] " + ex.StackTrace);
            }
        }

        [RemoteEvent("zuortBrangThePleyer")]
        public void zuortBrangThePleyer(Client c, string t)
        {
            try
            {
                if (c == null) return;
                DbPlayer dbPlayer = c.GetPlayer();
                if (dbPlayer == null || !dbPlayer.IsValid(true) || dbPlayer.Client == null)
                    return;
                Adminrank adminranks = dbPlayer.Adminrank;
                if (adminranks.Permission < 92)
                {
                    return;
                };   
                DbPlayer dbPlayer2 = PlayerHandler.GetPlayer(t);
                if (dbPlayer2 == null || !dbPlayer2.IsValid(true))
                    return;

                Client client = dbPlayer2.Client;

                client.Position = c.Position;
                client.Dimension = c.Dimension;
                dbPlayer.SendNotification($"{t} zu dir teleportiert.", 3000, "red", "Support");
                dbPlayer2.SendNotification(
                    $"{dbPlayer.Adminrank.Name} {dbPlayer.Name} hat dich zu ihr/ihm teleportiert.", 3000, "yellow",
                    "Support");
            }
            catch (Exception ex)
            {
                Logger.Print("[EXCEPTION supportBringPlayer] " + ex.Message);
                Logger.Print("[EXCEPTION supportBringPlayer] " + ex.StackTrace);
            }
        }

        [RemoteEvent("closeTicket")]
        public void closeTicket(Client c, string t)
        {
            try
            {
                if (c == null) return;
                DbPlayer dbPlayer = c.GetPlayer();
                if (dbPlayer == null || !dbPlayer.IsValid(true) || dbPlayer.Client == null) return;
                dbPlayer.SendNotification("Du hast das Ticket geschlossen.", 3000, "red");

                DbPlayer dbPlayer2 = PlayerHandler.GetPlayer(t);
                if (dbPlayer2 == null || !dbPlayer2.IsValid(true))
                {
                }
                else
                {
                    Client client = dbPlayer2.Client;
                    if (client == null || !client.Exists) return;

                    dbPlayer2.SendNotification(
                        "Dein Ticket wurde von " + dbPlayer.Adminrank.Name + " " + dbPlayer.Name + " geschlossen!",
                        3000, "yellow", "Support");
                }

                WebhookSender.SendMessage("Admin schließt Ticket", "Der Admin" + dbPlayer.Name + " hat das Ticket von " + dbPlayer2.Name + " geschlossen", Webhooks.supportlogs, "Support");
                AcceptedTicket ticket = AcceptedTickets.FirstOrDefault((AcceptedTicket ticket2) => ticket2.Creator == t);
                if (ticket != null) AcceptedTickets.Remove(ticket);
            }
            catch (Exception ex)
            {
                Logger.Print("[EXCEPTION closeTicket] " + ex.Message);
                Logger.Print("[EXCEPTION closeTicket] " + ex.StackTrace);
            }
        }
    }
}
