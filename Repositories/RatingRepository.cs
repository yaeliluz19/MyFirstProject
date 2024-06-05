using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using MyFirstProject;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class RatingRepository : IRatingRepository
    {
        private Market326354982Context _market326354982;
        public IConfiguration _configuration { get; }
        public RatingRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<Rating> CreateRating(Rating rating)
        {
            try
            {
                
                string query = "INSERT INTO RATING(HOST,METHOD,PATH,REFERER,USER_AGENT,Record_Date)" + "VALUES(@HOST,@METHOD,@PATH,@REFERER,@USER_AGENT,@Record_Date)";
                using (SqlConnection cn = new SqlConnection(_configuration["ConnectionStrings"]))
                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cmd.Parameters.Add("@HOST", SqlDbType.NChar, 50).Value = rating.Host;
                    cmd.Parameters.Add("@METHOD", SqlDbType.NChar, 10).Value = rating.Method;
                    cmd.Parameters.Add("@PATH", SqlDbType.NChar, 50).Value = rating.Path;
                    cmd.Parameters.Add("@REFERER", SqlDbType.NChar, 100).Value = rating.Referer;
                    cmd.Parameters.Add("@USER_AGENT", SqlDbType.NChar, int.MaxValue).Value = rating.UserAgent;
                    cmd.Parameters.Add("@Record_Date", SqlDbType.DateTime).Value = rating.RecordDate;



                    cn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    cn.Close();


                }
                return rating;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
