using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using PostHandler.Model;
using PostHandler.Data.Contracts;
using PostHandler.Data.Repositories;
using PostHandler.Foundation.Configurations;
using PostHandler.Endpoints.Models;
using Newtonsoft.Json;

namespace PostHandler.Endpoints.Controllers
{
    [RoutePrefix("endpoint")]
    public class EndpointsController : BaseController
    {
        private INRGPostRepository _nrgPostRepositoryProvider;
        public EndpointsController()
        {
            _nrgPostRepositoryProvider = NRGPostRepository.Create(APIConfigurationManager.Current.APISettings.ReadConnectionString);
        }

        [HttpPost]
        [Route("nrgcampaign")]
        public async Task<IHttpActionResult> ReceiveNRGData([FromBody]NRGPostHandler data)
        {
            _logger = new QueueLogger(data.CustomerNumber, nameof(EndpointsController.ReceiveNRGData));
            try
            {
                if (APIConfigurationManager.Current.APISettings.NRGAuthkey.Equals(data.NRGAuthkey))
                {
                    _nrgPostRepositoryProvider = NRGPostRepository.Create(APIConfigurationManager.Current.APISettings.WriteConnectionString);
                    var result = await _nrgPostRepositoryProvider.InsertNRPostDataAsync(data);
                    var statuscode = "Failure";
                    if (result == 101)
                    {
                        _logger.Message = "Data Inserted successfully BTN: " + data.CustomerNumber;
                        _logger.StackTrace = JsonConvert.SerializeObject(data);
                        statuscode = "Success";
                    }
                    else
                    {
                        _logger.Level = "type: Error,severity: Critical";
                        _logger.Message = "Data not inserted";
                        _logger.StackTrace = JsonConvert.SerializeObject(data);
                        statuscode = "Failure";
                        return BadRequest(statuscode);
                    }
                    return Ok(statuscode); 
                }
                else
                {
                    _logger.Level = "type: Error,severity: Critical";
                    _logger.Message = "Data not inserted";
                    _logger.StackTrace = JsonConvert.SerializeObject(data);
                    return BadRequest("Unauthorized");
                }
            }
            catch (Exception ex)
            {
                _logger.Level = "type: Error,severity: Critical";
                _logger.Message = "Exception raised in ReceiveNRGData on API controller";
                _logger.StackTrace = JsonConvert.SerializeObject(ex, Formatting.None);
                return InternalServerError(ex);
            }
            finally
            {
                await ConcurrentLogger.Enqueue(_logger);
            }
        }
    }
}