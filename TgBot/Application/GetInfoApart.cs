using System.Collections.ObjectModel;
using System.Text.Json;
using MediatR;
using TgBot.Commands;
using TgBot.Domain.Entity;
using TgBot.Dto;
using Vostok.Logging.Abstractions;

namespace TgBot.Application;

public class GetInfoPriceApartHandler : IRequestHandler<GetInfoApart, GetInfoApartResponse>
{
    private readonly ILog _log;
    private const string QueryForGetData = "?ajax=1&similar=1";
    private readonly IHttpClientFactory _clientFactory;
    public GetInfoPriceApartHandler(ILog log, IHttpClientFactory clientFactory)
    {
        _log = log;
        _clientFactory = clientFactory;
    }

    public async Task<GetInfoApartResponse> Handle(GetInfoApart request, CancellationToken cancellationToken)
    {
        var result = new List<ResponseDataApart>();
        var httpclient = _clientFactory.CreateClient();
        foreach (var apart in request.Apartments)
        {
            var response = await GetPriceInfo(apart, httpclient);
            if (response != null) result.Add(response);
        }

        _log.Info("Info about aparts collected");
        return new GetInfoApartResponse(result);
    }

    private async Task<ResponseDataApart?> GetPriceInfo(Apartment apart, HttpClient httpClient)
    {
        var response = await httpClient.GetAsync(apart.UrlApart + QueryForGetData);

        var jsonString = await response.Content.ReadAsStringAsync();
        var price = JsonDocument.Parse(jsonString).RootElement.GetProperty("price").GetString();

        if (long.TryParse(price, out var priceApart))
            return new ResponseDataApart(apart.UrlApart, priceApart);

        _log.Error($"this apart with url:{apart.UrlApart} not existed");
        return null;
    }
}

public record GetInfoApart(Apartment[] Apartments) : IRequest<GetInfoApartResponse>;

public record GetInfoApartResponse(IReadOnlyCollection<ResponseDataApart> ResponseDataAparts)
{
    public bool IsEmpty => ResponseDataAparts.Count == 0;
}