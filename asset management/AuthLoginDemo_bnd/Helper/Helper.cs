using System;
using System.Net;
using System.Net.Sockets;
using AuthLoginDemo_bnd.Models;

namespace AuthLoginDemo_bnd.Helper
{
    public class Helper
    {
        //GET IP OF CURRENT USER
        private static string GetIP()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }

        //LOG GENERATOR
        public static void createLog(AuthenticationContext _context, bool log_situation, int user_id, string process, string statement)
        {        
            DateTime localDate = DateTime.Now;
            LogOperations logger = new LogOperations();

            logger.log_situation = log_situation;
            logger.user_id = user_id;
            logger.process = process;            
            logger.statement = statement;
            logger.date_time = localDate;
            logger.user_ip = GetIP();

            _context.LogOperations.Add(logger);          
        }
    }
}