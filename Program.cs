using System;
using System.IO;
using System.Text;

namespace SupplyChainSimulation
{
    class Program
    {
        static void Main(string[] args)
        {
            int tests = 1;
            double[] monies = new double[tests];

            double profit = 220;
            StreamWriter sw = new StreamWriter("data.txt");
            

            for (int k = 0; k <tests; k++){
                monies[k] = 6976013;
            }
            for (int k = 0; k <tests; k++){
                monies[k] -= k*5*50000;
                Location cal = new Location("cal", 38.5, 405, 600, 689, 120);
                Location sor = new Location("sor", 27.5,124,400,620,120);
                Location tyr = new Location("tyr", 18,223,400,316,120);
                Location ent = new Location("ent", 17,400,400,646,120);
                double factoryEndDate = 0;
                
                double cResupplyDate = double.MaxValue;
                double sResupplyDate = double.MaxValue;
                double tResupplyDate = double.MaxValue;
                double eResupplyDate = double.MaxValue;
                double lostSales = 0;
                for (double i = 946; i < 1430; i+=0.2){

                    if ((i >= 946 + 90) && (i < 946 + 90.05)){
                        Console.WriteLine("increasing demand for sample " + k);
                        cal.leadTime = cal.orderSize/(175+k*5);
                        sor.leadTime = sor.orderSize/(175+k*5);
                        tyr.leadTime = tyr.orderSize/(175+k*5);
                        ent.leadTime = ent.orderSize/(175+k*5);
                        monies[k] -= 55*50000;
                        Console.WriteLine(cal.leadTime + " " + sor.leadTime + " " + tyr.leadTime + " " + ent.leadTime + " ");
                    }

                    // Console.WriteLine("DAY " + i);
                    sor.demand = (i-730)*0.1767+3.9743;

                    //start the factory
                    if (i >= factoryEndDate){
                        if (cal.inv<=cal.restockPoint && cResupplyDate == double.MaxValue){
                            factoryEndDate = i + cal.leadTime;
                            cResupplyDate = i + cal.leadTime + 7;
                            // Console.WriteLine("cal supply date " + cResupplyDate);
                        } else if (sor.inv<=sor.restockPoint && sResupplyDate == double.MaxValue){
                            factoryEndDate = i + sor.leadTime;
                            sResupplyDate = i + sor.leadTime + 7;
                            // Console.WriteLine("sor supply date " + sResupplyDate);
                        } else if (tyr.inv<=tyr.restockPoint && tResupplyDate == double.MaxValue){
                            factoryEndDate = i + tyr.leadTime;
                            tResupplyDate = i + tyr.leadTime + 7;
                            // Console.WriteLine("tyr supply date " + tResupplyDate);
                        } else if (ent.inv<=ent.restockPoint && eResupplyDate == double.MaxValue){
                            factoryEndDate = i + ent.leadTime;
                            eResupplyDate = i + ent.leadTime + 7;
                            // Console.WriteLine("ent supply date " + eResupplyDate);
                        }
                        // Console.WriteLine("factory end date " + factory1EndDate);
                    }

                    if (i >= cResupplyDate){
                        // Console.WriteLine("resupplying cal");
                        cal.inv+=cal.orderSize;
                        cResupplyDate = double.MaxValue;
                    }

                    if (i >= eResupplyDate){
                        // Console.WriteLine("resupplying ent");
                        ent.inv+=ent.orderSize;
                        eResupplyDate = double.MaxValue;
                    }

                    if (i >= tResupplyDate){
                        // Console.WriteLine("resupplying tyr");
                        tyr.inv+=tyr.orderSize;
                        tResupplyDate = double.MaxValue;
                    }

                    if (i >= sResupplyDate){
                        // Console.WriteLine("resupplying sor");
                        sor.inv+=sor.orderSize;
                        sResupplyDate = double.MaxValue;
                    }
                    
                    if (cal.inv >= cal.demand){
                        monies[k] += cal.demand*profit;
                        cal.inv-=cal.demand;
                    } else {
                        // if (cal.inv > 0){
                        //     monies[k] += cal.inv*profit;
                        //     lostSales+=(cal.demand - cal.inv);
                        //     cal.inv = 0;
                        // }
                        
                    }

                    if (sor.inv >= sor.demand){
                        monies[k] += sor.demand*profit;
                        sor.inv-=sor.demand;
                    } else {
                        // monies[k] += sor.inv*profit;
                        
                        // lostSales+=(sor.demand - sor.inv);
                        // sor.inv = 0;
                    }

                    if (tyr.inv >= tyr.demand){
                        monies[k] += tyr.demand*profit;
                        tyr.inv-=tyr.demand;
                    } else {
                        // monies[k] += tyr.inv*profit;
                        
                        // lostSales+=(tyr.demand - tyr.inv);
                        // tyr.inv = 0;
                    }

                    if (ent.inv >= ent.demand){
                        monies[k] += ent.demand*profit;
                        ent.inv-=ent.demand;
                    } else {
                        // monies[k] += ent.inv*profit;
                        
                        // lostSales+=(ent.demand - ent.inv);
                        // ent.inv = 0;
                    }

                    // totalInv = cal.inv+sor.inv+tyr.inv+ent.inv;
                    // //decrease the demand
                    // if (totalInv > cal.demand){
                    //     totalInv-= cal.demand;
                    //     money+=cal.demand*profit;
                    //     if (cal.inv > cal.demand){
                    //         cal.inv -= cal.demand;
                    //     } else {
                    //         cal.inv = 0;
                    //         cal.demand -= cal.inv;
                    //         if (sor.inv > cal.demand){
                    //             sor.inv -= cal.demand;
                    //         } else {
                    //             sor.inv = 0;
                    //             sor.demand -= cal.inv;
                    //             if (tyr.inv > cal.demand){
                    //                 tyr.inv -= cal.demand;
                    //             } else {
                    //                 tyr.inv = 0;
                    //                 tyr.demand -= cal.inv;
                    //                 if (ent.inv > cal.demand){
                    //                     ent.inv -= cal.demand;
                    //                 } else {
                    //                     ent.inv = 0;
                    //                     ent.demand -= cal.inv;
                    //                 }
                    //             }
                    //         }
                    //     }
                    // } else {
                    //     lostSales+=cal.demand;
                    //     Console.WriteLine("LOST SALES :" + lostSales);
                    // }

                    // if (totalInv > sor.demand){
                    //     totalInv-= sor.demand;
                    //     money+=cal.demand*profit;
                    //     if (sor.inv > sor.demand){
                    //         sor.inv -= sor.demand;
                    //     } else {
                    //         sor.inv = 0;
                    //         sor.demand -= sor.inv;
                    //         if (cal.inv > sor.demand){
                    //             cal.inv -= sor.demand;
                    //         } else {
                    //             cal.inv = 0;
                    //             cal.demand -= sor.inv;
                    //             if (tyr.inv > sor.demand ){
                    //                 tyr.inv -= sor.demand;
                    //             } else {
                    //                 tyr.inv = 0;
                    //                 tyr.demand -= sor.inv;
                    //                 if (ent.inv > sor.demand ){
                    //                     ent.inv -= sor.demand;
                    //                 } else {
                    //                     ent.inv = 0;
                    //                     ent.demand -= sor.inv;
                    //                 }
                    //             }
                    //         }
                    //     }
                    // } else {
                    //     lostSales+=sor.demand;
                    //     Console.WriteLine("LOST SALES :" + lostSales);
                    // }

                    // if (totalInv > tyr.demand){
                    //     totalInv-= tyr.demand;
                    //     money+=cal.demand*profit;
                    //     if (tyr.inv > tyr.demand){
                    //         tyr.inv -= tyr.demand;
                    //     } else {
                    //         tyr.inv = 0;
                    //         tyr.demand -= tyr.inv;
                    //         if (cal.inv > tyr.demand){
                    //             cal.inv -= tyr.demand;
                    //         } else {
                    //             cal.inv = 0;
                    //             cal.demand -= tyr.inv;
                    //             if (sor.inv > tyr.demand){
                    //                 sor.inv -= tyr.demand;
                    //             } else {
                    //                 sor.inv = 0;
                    //                 sor.demand -= tyr.inv;
                    //                 if (ent.inv > tyr.demand){
                    //                     ent.inv -= tyr.demand;
                    //                 } else {
                    //                     ent.inv = 0;
                    //                     ent.demand -= tyr.inv;
                    //                 }
                    //             }
                    //         }
                    //     }
                    // } else {
                    //     lostSales+=tyr.demand;
                    //     Console.WriteLine("LOST SALES :" + lostSales);
                    // }

                    // if (totalInv > ent.demand){
                    //     totalInv-= ent.demand;
                    //     money+=cal.demand*profit;
                    //     if (ent.inv > ent.demand){
                    //         ent.inv -= ent.demand;
                    //     } else {
                    //         ent.inv = 0;
                    //         ent.demand -= ent.inv;
                    //         if (sor.inv > ent.demand){
                    //             sor.inv -= ent.demand;
                    //         } else {
                    //             sor.inv = 0;
                    //             sor.demand -= ent.inv;
                    //             if (tyr.inv > ent.demand){
                    //                 tyr.inv -= ent.demand;
                    //             } else {
                    //                 tyr.inv = 0;
                    //                 tyr.demand -= ent.inv;
                    //                 if (cal.inv > ent.demand){
                    //                     cal.inv -= ent.demand;
                    //                 } else {
                    //                     cal.inv = 0;
                    //                     cal.demand -= ent.inv;
                    //                 }
                    //             }
                    //         }
                    //     }
                    // } else {
                    //     lostSales+=ent.demand;
                    //     Console.WriteLine("LOST SALES :" + lostSales);
                    // }
                    
                    // Console.WriteLine("Cal Inv: " + cal.inv);
                    // Console.WriteLine("Sor Inv: " + sor.inv);
                    // Console.WriteLine("Tyr Inv: " + tyr.inv);
                    // Console.WriteLine("Ent Inv: " + ent.inv);
                    // Console.WriteLine();
                    

                    
                    //sw.WriteLine((int)(cal.inv) + "\t" + (int)(sor.inv) + "\t" + (int)(tyr.inv) + "\t" + (int)(ent.inv) + "\t" + (int)(lostSales) + "\t" + (int)(monies[k]));
                    sw.WriteLine((long)(monies[k]));
                }
                // sw.Write((long)(monies[k]) + "\t");
                // 
                sw.Flush();
                // Console.WriteLine("Lost Sales = " + lostSales);
                sw.WriteLine();

            }
            sw.Close();



            
        }
    }

    class Location{
        public string name;
        
        public double demand;
        public double startDemand;
        public double inv;
        public double orderSize;
        public double restockPoint;
        public double leadTime;

        public Location(string n, double d,double i,double o,double r,double c){
            name = n;
            demand=d;
            startDemand = d;
            inv=i;
            orderSize=o;
            restockPoint=r;
            leadTime=o/c;
        }

        public void ResetDemand(){
            demand = startDemand;
        }
    }
}
