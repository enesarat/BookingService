using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace BookingService.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppartmentController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public AppartmentController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string queryStr = @"
                select name as ""name"",
                        country as ""country""
                from appartments
            ";

            DataTable table = new DataTable();
            string source = _configuration.GetConnectionString("BookingServiceCon");
            NpgsqlDataReader _reader;
            using (NpgsqlConnection _connection = new NpgsqlConnection(source))
            {
                _connection.Open();
                using (NpgsqlCommand _command = new NpgsqlCommand(queryStr, _connection)) {
                    _reader = _command.ExecuteReader();
                    table.Load(_reader);
                    _reader.Close();
                    _connection.Close();
                }
            }
            return new JsonResult(table);
        }
    }
}
