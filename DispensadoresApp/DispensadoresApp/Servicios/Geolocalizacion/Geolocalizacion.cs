using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace DispensadoresApp.Servicios.Geolocalizacion
{
    public class Geolocalizacion
    {
        public double Lat;
        public double Lng;
        public double[]Distancias;
        public Geolocalizacion(double lat,double lng)//, double[]posiciones
        {
            this.Lat = lat;
            this.Lng = lng;
           


           /* double[] distancias = new double[posiciones.Length];
            for (int i = 0; i <=posiciones.Length; i += 1)
            {

                distancias[i] = 1.4;

                

            }
            this.Distancias = distancias;*/

        }
        public double Calcular()
        {
            var Coords = new Location();
            Coords.Latitude = this.Lat;
            Coords.Longitude = this.Lng;
            var sCoords = new Location();
            Coords.Latitude = 3.381991;
            Coords.Longitude = -76.518472;
            return Location.CalculateDistance(Coords, sCoords, 0);
        }

        public double distanceBetween(double lat2, double lon2)
        {
            double theta = Lng - lon2;
            double dist = Math.Sin(deg2rad(Lat))
                    * Math.Sin(deg2rad(lat2))
                    + Math.Cos(deg2rad(Lat))
                    * Math.Cos(deg2rad(lat2))
                    * Math.Cos(deg2rad(theta));
            dist = Math.Acos(dist);
            dist = dist * 180.0 / Math.PI;
            dist = dist * 60 * 1.1515 * 1000;
            return (dist);
        }

        private double deg2rad(double deg)
        {
            return (deg * Math.PI / 180.0);
        }


    }
}
