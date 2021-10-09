using MarsRover.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MarsRover
{
    public class Program
    {
        #region Private Functions
        private static string ChangeLocation(string fromLocation, string Rotation)
        {
            //Bulunulan Konum ve Rotasyon Birleştiriliyor,
            //Daha Sonra Önceden Tanımlanmış Olan Değerlerden Hangisine Denk Geldiği Kontrol Ediliyor.
            string fullRotation = fromLocation + Rotation;
            Dictionary<string, string> locationRotation = new Dictionary<string, string>();
            //South
            locationRotation.Add("SR", Locations.West);
            locationRotation.Add("SL", Locations.East);
            //West
            locationRotation.Add("WR", Locations.North);
            locationRotation.Add("WL", Locations.South);
            //East
            locationRotation.Add("ER", Locations.South);
            locationRotation.Add("EL", Locations.North);
            //North
            locationRotation.Add("NR", Locations.East);
            locationRotation.Add("NL", Locations.West);

            locationRotation.TryGetValue(fullRotation, out string result);
            return result;
        }
        private static void RecursiveReadKey(AstronautMovementViewModel astronautMovement)
        {
            var newAstronaut = new AstronautLocationViewModel();


            Console.WriteLine("Lütfen Kullanıcı Koordinatlarını Aralarında Birer Boşluklarla Giriniz!");
            var userLocations = Console.ReadLine().Split(" ");

            //Alınan Kullanıcının Bilgileri Sırasıyla Değişkenlere Atanıyor.
            newAstronaut.CoordinateX = Convert.ToInt32(userLocations[0]);
            newAstronaut.CoordinateY = Convert.ToInt32(userLocations[1]);
            newAstronaut.Rotation = userLocations[2];

            //Komutlar Ardarda Alınarak Listemizin İçerisine Atılıyor.
            Console.WriteLine("Lütfen Komutları Aralarında Boşluk Bırakmadan, Ardarda Giriniz!");
            var commands = Console.ReadLine().ToCharArray().Select(s => s.ToString()).ToArray();
            newAstronaut.MovementCommands = commands;

            astronautMovement.AstronautLocation.Add(newAstronaut);

            //Kullanıcı Tanımlamaya Devam Etmek İsterse Y Tuşuna Basarak Devam Edebilir.
            Console.WriteLine("Kullanıcı Tanımlamaya Devam Etmek İstiyorsanız Y istemiyorsanız N tuşuna basınız!");
            var response = Console.ReadKey();
            if (response.Key == ConsoleKey.Y)
            {
                RecursiveReadKey(astronautMovement);
            }
        }
        #endregion


        /// <summary>
        /// Astronotun Gelmesi Gereken Pozisyonu Hesaplar.
        /// </summary>
        /// <param name="movement"></param>
        /// <returns>AstronautMovementViewModel</returns>
        public static AstronautMovementViewModel GetAstronautLocation(AstronautMovementViewModel movement)
        {
         
            string newRotation = "";
            foreach (var astronaut in movement.AstronautLocation)
            {
                //Sayı 0'dan Büyük mü Kontrol Et
                if (astronaut.CoordinateX.IsBiggerZero() && astronaut.CoordinateY.IsBiggerZero())
                {
                    foreach (var command in astronaut.MovementCommands)
                    {
                        //Kullanıcının Konumu Haritanın Boyutlarını Aştıysa Dikkate Alma
                        if (astronaut.CoordinateX <= movement.MapSize.CoordinateX &&
                            astronaut.CoordinateY <= movement.MapSize.CoordinateY)
                        {
                            //Komut Sağ ve Sol Komutuysa Astronot'u Gitmesi Gereken Yöne Çevirir.
                            if (command == "L" || command == "R")
                            {
                                newRotation = ChangeLocation(astronaut.Rotation, command);
                                astronaut.Rotation = newRotation;
                            }
                            if (command == "M")
                            {
                                switch (astronaut.Rotation)
                                {
                                    case Locations.North:
                                        astronaut.CoordinateY += 1;
                                        break;
                                    case Locations.South:
                                        astronaut.CoordinateY -= 1;
                                        break;
                                    case Locations.East:
                                        astronaut.CoordinateX += 1;
                                        break;
                                    case Locations.West:
                                        astronaut.CoordinateX -= 1;
                                        break;
                                    default:
                                        break;
                                }
                            }
                        }

                    }

                }
            }
            return movement;
        }

        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Lütfen Harita Bilgisini, X ve Y Koortinatlarını Aralarında Birer Boşluklarla Giriniz.");
                var mapSize = Console.ReadLine().Split(" ");
                var astronautMovement = new AstronautMovementViewModel()
                {
                    MapSize = new MapSizeViewModel()
                    {
                        CoordinateX = Convert.ToInt32(mapSize[0]),
                        CoordinateY = Convert.ToInt32(mapSize[1]),
                    },
                };

                //Kullanıcının Sonsuz Astronot Tanımı Yapabilmesini Sağlar.
                RecursiveReadKey(astronautMovement);
                var res = GetAstronautLocation(astronautMovement);
                var index = 1;
                foreach (var item in res.AstronautLocation)
                {
                    Console.WriteLine($"Astronot {index} X Koordinatı : {item.CoordinateX }");
                    Console.WriteLine($"Astronot {index} Y Koordinatı : {item.CoordinateY}");
                    Console.WriteLine($"Astronot {index} Rotasyonu : {item.Rotation}");
                    Console.WriteLine("-----------------------------------------");
                    index++;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hata Oluştu Uygun Giriş Yapılmamış Olabilir : " + ex.Message);
            }


            Console.ReadLine();
        }

    }
    #region Extensions
    public static class NumberExtensions
    {
        public static bool IsBiggerZero(this int number)
        {
            if (number < 0)
            {
                return false;
            }
            return true;
        }
    }
    #endregion
}
