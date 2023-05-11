using DasboardAloha.Entities.Configuration.ModelsConfiguration;
using DashboardAloha.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DashboardAloha.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DashboardController : ControllerBase
    {
        private readonly ILogger<DashboardController> logger;
        private readonly IDashboardService DashboardService;
        public DashboardController(ILogger<DashboardController> logger,
            IDashboardService DashboardService)
        {
            this.logger = logger;
            this.DashboardService = DashboardService;
        }

        [HttpGet]
        [Route("GetListUsersActives")]
        public async Task<IActionResult> GetListUsersActives()
        {
            try
            {
                logger.LogInformation("Inicio GetListUsersActives");

                ResponseServiceModel response = await this.DashboardService.GetListUsersActives();
                if (response.IsSuccess)
                {
                    logger.LogInformation($"GetListUsersActives correcto");
                    return Ok(response.Response);
                }
                else
                {
                    logger.LogInformation($"GetListUsersActives sin resultados");
                    return BadRequest(response.Response);
                }

            }
            catch (Exception ex)
            {
                logger.LogError("Error GetListUsersActives", ex);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetTotalSalesDashboard")]
        public async Task<IActionResult> GetTotalSalesDashboard()
        {
            try
            {
                logger.LogInformation("Inicio GetTotalSalesDashboard");

                ResponseServiceModel response = await this.DashboardService.GetTotalSalesDashboard();
                if (response.IsSuccess)
                {
                    logger.LogInformation($"GetTotalSalesDashboard correcto");
                    return Ok(response.Response);
                }
                else
                {
                    logger.LogInformation($"GetTotalSalesDashboard sin resultados");
                    return BadRequest(response.Response);
                }

            }
            catch (Exception ex)
            {
                logger.LogError("Error GetTotalSalesDashboard", ex);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetUsersDesertersDashboard")]
        public async Task<IActionResult> GetUsersDesertersDashboard()
        {
            try
            {
                logger.LogInformation("Inicio GetUsersDesertersDashboard");

                ResponseServiceModel response = await this.DashboardService.GetUsersDesertersDashboard();
                if (response.IsSuccess)
                {
                    logger.LogInformation($"GetUsersDesertersDashboard correcto");
                    return Ok(response.Response);
                }
                else
                {
                    logger.LogInformation($"GetUsersDesertersDashboard sin resultados");
                    return BadRequest(response.Response);
                }

            }
            catch (Exception ex)
            {
                logger.LogError("Error GetUsersDesertersDashboard", ex);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetUsersXContentDashboard")]
        public async Task<IActionResult> GetUsersXContentDashboard()
        {
            try
            {
                logger.LogInformation("Inicio GetUsersXContentDashboard");

                ResponseServiceModel response = await this.DashboardService.GetUsersXContentDashboard();
                if (response.IsSuccess)
                {
                    logger.LogInformation($"GetUsersXContentDashboard correcto");
                    return Ok(response.Response);
                }
                else
                {
                    logger.LogInformation($"GetUsersXContentDashboard sin resultados");
                    return BadRequest(response.Response);
                }

            }
            catch (Exception ex)
            {
                logger.LogError("Error GetUsersXContentDashboard", ex);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetUsersGenderDashboard")]
        public async Task<IActionResult> GetUsersGenderDashboard()
        {
            try
            {
                logger.LogInformation("Inicio GetUsersGenderDashboard");

                ResponseServiceModel response = await this.DashboardService.GetUsersGenderDashboard();
                if (response.IsSuccess)
                {
                    logger.LogInformation($"GetUsersGenderDashboard correcto");
                    return Ok(response.Response);
                }
                else
                {
                    logger.LogInformation($"GetUsersGenderDashboard sin resultados");
                    return BadRequest(response.Response);
                }

            }
            catch (Exception ex)
            {
                logger.LogError("Error GetUsersGenderDashboard", ex);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetUsersActiveDashboard")]
        public async Task<IActionResult> GetUsersActiveDashboard()
        {
            try
            {
                logger.LogInformation("Inicio GetUsersActiveDashboard");

                ResponseServiceModel response = await this.DashboardService.GetUsersActiveDashboard();
                if (response.IsSuccess)
                {
                    logger.LogInformation($"GetUsersActiveDashboard correcto");
                    return Ok(response.Response);
                }
                else
                {
                    logger.LogInformation($"GetUsersActiveDashboard sin resultados");
                    return BadRequest(response.Response);
                }

            }
            catch (Exception ex)
            {
                logger.LogError("Error GetUsersActiveDashboard", ex);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetUsersRegisterDashboard")]
        public async Task<IActionResult> GetUsersRegisterDashboard()
        {
            try
            {
                logger.LogInformation("Inicio GetUsersRegisterDashboard");

                ResponseServiceModel response = await this.DashboardService.GetUsersRegisterDashboard();
                if (response.IsSuccess)
                {
                    logger.LogInformation($"GetUsersRegisterDashboard correcto");
                    return Ok(response.Response);
                }
                else
                {
                    logger.LogInformation($"GetUsersRegisterDashboard sin resultados");
                    return BadRequest(response.Response);
                }

            }
            catch (Exception ex)
            {
                logger.LogError("Error GetUsersRegisterDashboard", ex);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetCountUsersActive")]
        public async Task<IActionResult> GetCountUsersActive()
        {
            try
            {
                logger.LogInformation("Inicio GetCountUsersActive");

                //var searchModel = searchJson.ToObject<SearchBindingModel>();

                ResponseServiceModel response = await this.DashboardService.GetCountUsersActive();
                if (response.IsSuccess)
                {
                    logger.LogInformation($"GetCountUsersActive correcto");
                    return Ok(response.Response);
                }
                else
                {
                    logger.LogInformation($"GetCountUsersActive sin resultados");
                    return BadRequest(response.Response);
                }

            }
            catch (Exception ex)
            {
                logger.LogError("Error GetCountUsersActive", ex);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetCountUsersRegisters")]
        public async Task<IActionResult> GetCountUsersRegisters()
        {
            try
            {
                logger.LogInformation("Inicio GetCountUsersRegisters");

                //var searchModel = searchJson.ToObject<SearchBindingModel>();

                ResponseServiceModel response = await this.DashboardService.GetCountUsersRegisters();
                if (response.IsSuccess)
                {
                    logger.LogInformation($"GetCountUsersRegisters correcto");
                    return Ok(response.Response);
                }
                else
                {
                    logger.LogInformation($"GetCountUsersRegisters sin resultados");
                    return BadRequest(response.Response);
                }

            }
            catch (Exception ex)
            {
                logger.LogError("Error GetCountUsersRegisters", ex);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetCountUsersRegistersXMembership")]
        public async Task<IActionResult> GetCountUsersRegistersXMembership()
        {
            try
            {
                logger.LogInformation("Inicio GetCountUsersRegistersXMembership");

                ResponseServiceModel response = await this.DashboardService.GetCountUsersRegistersXMembership();
                if (response.IsSuccess)
                {
                    logger.LogInformation($"GetCountUsersRegistersXMembership correcto");
                    return Ok(response.Response);
                }
                else
                {
                    logger.LogInformation($"GetCountUsersRegistersXMembership sin resultados");
                    return BadRequest(response.Response);
                }

            }
            catch (Exception ex)
            {
                logger.LogError("Error GetCountUsersRegistersXMembership", ex);
                return BadRequest(ex.Message);
            }
        }
    }
}