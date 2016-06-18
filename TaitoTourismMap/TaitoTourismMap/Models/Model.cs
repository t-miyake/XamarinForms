using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using TK.CustomMap;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace TaitoTourismMap
{
    public class Model
    {
        // Singleton instance.
        private static readonly Model _Model = new Model();
        public static Model Instance
        {
            get
            {
                return _Model;
            }
        }
        private Model() { }

        public MapSpan MapRegion { get; set; }

        public ObservableCollection<TKCustomMapPin> CustomPins = new ObservableCollection<TKCustomMapPin>();
        public ObservableCollection<ItemProperty> TouristSpot = new ObservableCollection<ItemProperty>();

        public object GetObjectFromJson(string jsonString, Type t)
        {
            var serializer = new DataContractJsonSerializer(t);
            var jsonBytes = Encoding.Unicode.GetBytes(jsonString);
            var stream = new MemoryStream(jsonBytes);
            return serializer.ReadObject(stream);
        }

        //API keyの取得はここから(https://developers.google.com/maps/)
        const string googleApiKey = "API KEY";

        public void FetchTouristSpotData(string genreName)
        {
            var client = new HttpClient();

            var apiUrl = $"https://www.chiikinogennki.soumu.go.jp/k-cloud-api/v001/kanko/{genreName}/json?place=東京都,台東区&limit=50";
            var rawTouristSpotDataJson = client.GetStringAsync(apiUrl).Result;
            var convertedTouristSpotDataJson = (TouristSpot.Rootobject)GetObjectFromJson(rawTouristSpotDataJson, typeof(TouristSpot.Rootobject));

            string imageUrl;
            string location;
            foreach (var ts in convertedTouristSpotDataJson.tourspots)
            {
                //写真がないデータはTS.viewsがnullになっているため、TS.views[0].fidにアクセスできない。それを回避する。
                if (ts.views == null)
                {
                    imageUrl = "";
                }
                else
                {
                    imageUrl = $"https://www.chiikinogennki.soumu.go.jp/k-cloud-api/v001/kanko/view/{ts.mng.refbase}/{ts.views[0].fid}";
                }

                //GoogleのAPIの制限対策(1秒間のリクエスト数10回)のために、1回あたり0.101秒停止させる。
                var task = Task.Delay(101);
                task.Wait();

                //住所が途中までしか入っていないデータ(花やしき)があるため、その対策。
                if (ts.place.street == null)
                {
                    location = $"{ts.place.pref.written} {ts.place.city.written}";
                }
                else
                {
                    location = $"{ts.place.pref.written} {ts.place.city.written} {ts.place.street.written}";
                }

                var googleApiUrl = $@"https://maps.googleapis.com/maps/api/geocode/json?address=""{location}""&key={googleApiKey}";
                var rawGeocodeJson = client.GetStringAsync(googleApiUrl).Result;
                var convertedGeocodeJson = (Geocode.Rootobject)GetObjectFromJson(rawGeocodeJson, typeof(Geocode.Rootobject));

                TouristSpot.Add(new ItemProperty
                {
                    Name = ts.name.name1.written,
                    Description = ts.descs[0].body,
                    Place = location,
                    ImageURL = imageUrl,
                    Genre = $"{ts.genres[0].M}/{ts.genres[0].S}",
                    Latitude = convertedGeocodeJson.results[0].geometry.location.lat,
                    Longitude = convertedGeocodeJson.results[0].geometry.location.lng
                });

                CustomPins.Add(new TKCustomMapPin
                {
                    IsVisible = true,
                    Title = ts.name.name1.written,
                    Subtitle = ts.descs[0].body,
                    Position = new Position(convertedGeocodeJson.results[0].geometry.location.lat, convertedGeocodeJson.results[0].geometry.location.lng),
                    ShowCallout = true,
                    DefaultPinColor = Color.Red
                });
            }
        }
    }
}