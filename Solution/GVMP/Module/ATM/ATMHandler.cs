using GTANetworkAPI;
using System;
using System.Collections.Generic;
using GVMP;
using GVMP;

namespace GVMP

{ 
    public class ATM : GVMP.Module.Module<ATM>
    {
        public static List<ATMModel> Atm_ = new List<ATMModel>();

        
        protected override bool OnLoad()
        {

            Atm_.Add(new ATMModel
            {
                Position = new Vector3(160.94, -3241.95, 5.98),
                robbed = false                
            });

            Atm_.Add(new ATMModel
            {
                Position = new Vector3(-42.1, -1757.87, 29.49),
                robbed = false
            });

            Atm_.Add(new ATMModel
            {
                Position = new Vector3(-43.17, -1759.73, 29.49),
                robbed = false
            });

            Atm_.Add(new ATMModel
            {
                Position = new Vector3(50.74, -1044.96, 29.58),
                robbed = false
            });

            Atm_.Add(new ATMModel
            {
                Position = new Vector3(130.34, -1054.86, 29.19),
                robbed = false
            });


            Atm_.Add(new ATMModel
            {
                Position = new Vector3(127.71, -1054.37, 29.19),
                robbed = false
            });


            Atm_.Add(new ATMModel
            {
                Position = new Vector3(115.71, -1049.54, 29.2),
                robbed = false
            });


            Atm_.Add(new ATMModel
            {
                Position = new Vector3(300.13, -906.51, 29.29),
                robbed = false
            });

            Atm_.Add(new ATMModel
            {
                Position = new Vector3(298.11, -905.79, 29.29),
                robbed = false
            });



            Atm_.ForEach(x => {

                 ColShape val1 = NAPI.ColShape.CreateCylinderColShape(x.Position, 1.4f, 2.4f, 0);
                 val1.SetData("FUNCTION_MODEL", new FunctionModel("robATM"));
                 val1.SetData("MESSAGE", new Message("Drücke E um den Mülltonnen durchsuchen ", "", "red", 3000));
                 NAPI.Marker.CreateMarker(29, x.Position, new Vector3(), new Vector3(), 1.0f, new Color(160, 32, 240), false, 0);

             });
            return true;
        }
    

        [RemoteEvent("robATM")]
        public static void robATM(Client client)
        {
            try
            {
                if (client == null) return;
                DbPlayer dbPlayer = client.GetPlayer();

                if (dbPlayer == null || !dbPlayer.IsValid(true) || dbPlayer.Client == null)
                    return;
               

                Atm_.ForEach(x =>
                {
                    if (x.Position.DistanceTo(client.Position) <= 1.5f)                   
                    {
                        if (!dbPlayer.IsFarming)
                        {
                            if (!x.robbed)
                            {
                                dbPlayer.disableAllPlayerActions(true);
                                dbPlayer.AllActionsDisabled = true;
                                dbPlayer.SendProgressbar(15000);
                                dbPlayer.IsFarming = true;
                                dbPlayer.RefreshData(dbPlayer);
                                dbPlayer.PlayAnimation(33, "mini@repair", "fixing_a_player", 8f);
                                dbPlayer.SendNotification("Mülltonnen durchsuchen !", 2000, "grey");
                                x.robbed = true;

                                

                                NAPI.Task.Run(delegate
                                {
                                    dbPlayer.TriggerEvent("client:respawning");
                                    dbPlayer.StopProgressbar();
                                    dbPlayer.addMoney(5000);
                                    dbPlayer.IsFarming = false;
                                    dbPlayer.RefreshData(dbPlayer);
                                    dbPlayer.disableAllPlayerActions(false);
                                    dbPlayer.StopAnimation();                                 
                                    dbPlayer.SendNotification("Du hast diesen Mülltonnen durchsuchen !!", 2000, "green");

                                }, 15000);

                            }
                            else
                            {
                                dbPlayer.SendNotification("Dieser Mülltonnen wurde bereits durchsuchen.", 3000, "red", "ATM");
                                return;
                            }
                        }
                    }                
                });
            }
            catch (Exception ex)
            {
                Logger.Print("[EXCEPTION robATM] " + ex);
            }
        }       
    }
}
