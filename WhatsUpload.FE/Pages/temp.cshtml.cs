using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

namespace WhatsUpload.FE.Pages
{
    public class tempModel : PageModel
    {
        public string connString { get; }

        public tempModel(IConfiguration configuration)
        {
            connString = configuration["ConnectionStrings:DefaultConnection"];
        }



        public class TopTracks
        {
            public string TrackId { get; set; }
            public string Name { get; set; }
            public string Category { get; set; }
            public string Composer { get; set; }
            public string TimePlayed { get; set; }
        }

        public List<TopTracks> TracksList { get; set; }

        public void OnGet()
        {
            try
            {
                string query = @"Select a.sid, b.Name, b.Composer, sum(a.secondsPlayed /60) TimePlayed " +
                                "from SongLog a " +
                                "join Songs b on b.kpid = a.sid " +
                                "group by a.sid, b.Name, b.Composer " +
                                "order by TimePlayed desc";

                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();
                    TracksList = new List<TopTracks>();

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                TopTracks oTrack = new TopTracks();
                                oTrack.TrackId = reader["sid"].ToString();
                                oTrack.Name = reader["Name"].ToString();
                                oTrack.Composer = reader["Composer"].ToString();
                                oTrack.TimePlayed = reader["TimePlayed"].ToString();
                                TracksList.Add(oTrack);
                            }
                            //                            var JsonResult = JsonConvert.SerializeObject(oTracksList);
                        }
                    }
                }

            }
            catch (SqlException ex)
            {
                string msg = "Insert Error:";
                msg += ex.Message;
            }
        }

    }
}