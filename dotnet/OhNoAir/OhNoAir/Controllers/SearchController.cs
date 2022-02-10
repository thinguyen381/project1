using Microsoft.AspNetCore.Mvc;
using OhNoAir.Models;
using System.Data.SqlClient;

namespace OhNoAir.Controllers
{
    public class SearchController : Controller
    {
        public async Task<IActionResult> Index(string From, string To, DateTime DepartDate, DateTime ReturnDate)
        {

            string s = "";

            //testing
            //testing 2


            string departQueryString = $"Declare @FromID int, @ToID int;" +
                $" Select @FromID = DestinationID from Project1..Destination where DestinationName = '{From}';" +
                $" Select @ToID = DestinationID from Project1..Destination where DestinationName = '{To}'" +

                $" Select FlightID, Date, Price From Project1..Flight " +
                $" where FromID = @FromID and ToID = @ToID" +
                $" and Date='{DepartDate}'";

            List<Flight> departFlights = GetFlightsByQuery(departQueryString);

            string returnQueryString = $"Declare @FromID int, @ToID int;" +
              $" Select @FromID = DestinationID from Project1..Destination where DestinationName = '{To}';" +
              $" Select @ToID = DestinationID from Project1..Destination where DestinationName = '{From}'" +

              $" Select FlightID, Date, Price From Project1..Flight " +
              $" where FromID = @FromID and ToID = @ToID" +
              $" and Date='{ReturnDate}'";
            List<Flight> returnFlights = GetFlightsByQuery(returnQueryString);

            Search result = new Search();
            result.Departs = departFlights;
            result.Returns = returnFlights;

            if (From != null && To != null)
            {
                result.Depart = $"From: {From}    To: {To}";
                result.Return = $"From: {To}    To: {From}";
            }
            

            return View(result);
            //var movies = from m in _context.Movie
            //             select m;

            //if (!String.IsNullOrEmpty(searchString))
            //{
            //    movies = movies.Where(s => s.Title!.Contains(searchString));
            //}

            //return View(await movies.ToListAsync());
        }


        private List<Flight> GetFlightsByQuery(string query)
        {
            List<Flight> results = new List<Flight>();
            string connectionString = "Server=tcp:comsci380.database.windows.net,1433;Initial Catalog=project1;Persist Security Info=False;User ID=admin1;Password=ComSci380;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";


            // Specify the parameter value.
            using (SqlConnection connection =
                new SqlConnection(connectionString))
            {
                // Create the Command and Parameter objects.
                SqlCommand command = new SqlCommand(query, connection);


                // Open the connection in a try/catch block.
                // Create and execute the DataReader, writing the result
                // set to the console window.
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Flight flight = new Flight();
                        flight.FlightID = Convert.ToInt32(reader[0]);
                        flight.Date = Convert.ToDateTime(reader[1]);
                        flight.Price = Convert.ToDecimal(reader[2]);
                        results.Add(flight);
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }
            return results;
        }
    }
}
