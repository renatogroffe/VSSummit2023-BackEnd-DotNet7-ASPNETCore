using Microsoft.AspNetCore.Mvc;
using APITemperatura.Models;

namespace APITemperatura.Controllers;

[ApiController]
[Route("[controller]")]
public class ConversorTemperaturasController : ControllerBase
{
    private readonly ILogger<ConversorTemperaturasController> _logger;

    public ConversorTemperaturasController(ILogger<ConversorTemperaturasController> logger)
    {
        _logger = logger!;
    }

    [HttpGet($"Fahrenheit/{{{nameof(tempFahrenheit)}}}")]
    [ProducesResponseType(typeof(Temperatura), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(FalhaCalculo), StatusCodes.Status400BadRequest)]
    public ActionResult<Temperatura> GetConversaoFahrenheit(double tempFahrenheit)
    {
        _logger.LogInformation(
            $"Recebida temperatura para conversão: {tempFahrenheit}");

        if (tempFahrenheit < -459.67)
        {
            var mensagemErro =
                $"Valor de temperatura em Fahrenheit invalido: {tempFahrenheit}";
            _logger.LogError(mensagemErro);
            return new BadRequestObjectResult(
                new FalhaCalculo()
                {
                    Mensagem = mensagemErro
                });
        }

        var resultado = new Temperatura(tempFahrenheit);
        _logger.LogInformation(
            $"{resultado.Fahrenheit} graus Fahrenheit = " +
            $"{resultado.Celsius} graus Celsius = " +
            $"{resultado.Kelvin} graus Kelvin");
        return resultado;
    }
}