using Newtonsoft.Json;
using System.Net;

namespace Weather_Application
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GetWeather();
        }

        private async void GetWeather()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://api.openweathermap.org");
            var response = await client.GetAsync("/data/2.5/weather?q=" +
                textBox1.Text + "&appid=e8cdd28ed3c60f69ca38c17f259d5275&units=imperial");
            var stringResult = await response.Content.ReadAsStringAsync();
            WeatherInfo.root info = JsonConvert.DeserializeObject<WeatherInfo.root>(stringResult);
            lab_temp.Text = info.main.temp.ToString() + "°F";
            lab_feelsLike.Text = info.main.feels_like.ToString() + "°F";
            lab_humid.Text = info.main.humidity.ToString() + "%";
            lab_windSpeed.Text = info.wind.speed.ToString() + "mph";
            lab_describ.Text = info.weather[0].description.ToString();
            lab_sunrise.Text = ConvertSun(info.sys.sunrise).ToString();
            lab_sunset.Text = ConvertSun(info.sys.sunset).ToString();
            pictureBox1.ImageLocation = "http://openweathermap.org/img/wn/" +
                info.weather[0].icon + "@2x.png";
        }

        DateTime ConvertSun(double milSecs)
        {
            DateTime today = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            today = today.AddSeconds(milSecs).ToLocalTime();
            return today;
        }
    }

}