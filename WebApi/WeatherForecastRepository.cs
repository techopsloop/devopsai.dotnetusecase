namespace WebApi
{
    using System.Data.SqlClient;

    public interface IWeatherForecastRepository
    {
        Task<IEnumerable<WeatherForecast>> GetAll();
    }

    public class WeatherForecastRepository : IWeatherForecastRepository
    {
        private readonly string connectionString;

        public WeatherForecastRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public async Task<IEnumerable<WeatherForecast>> GetAll()
        {
            var result = new List<WeatherForecast>();
            var query = "select datze, temperaturec, summary from dbo.WeatherForecastUC";

            using var sqlConnection = new SqlConnection(this.connectionString);

            sqlConnection.Open();

            using var sqlCommand = new SqlCommand(query, sqlConnection);
            using var sqlReader = await sqlCommand.ExecuteReaderAsync();
            if (!sqlReader.HasRows)
            {
                return result;
            }

            try
            {                
                while (await sqlReader.ReadAsync())
                {
                    var weatherForecast = new WeatherForecast()
                    {
                        Date = sqlReader.GetDateTime(0),
                        TemperatureC = sqlReader.GetInt32(1),
                        Summary = sqlReader.GetString(2)
                    };

                    result.Add(weatherForecast);
                }
            }
            catch (Exception)
            {
                await sqlReader.CloseAsync();
                throw;
            }

            return result;
        }
    }
}
