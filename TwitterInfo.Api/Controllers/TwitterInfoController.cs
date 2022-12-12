using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using JH.CodeAssignment.Api;
using JH.CodeAssignment.Api.Models;
using JH.CodeAssignment.Api.Services.TwitterInfos;
using JH.CodeAssignment.Api.Contracts.TwitterInfo;
using ErrorOr;
using Tweetinvi.Models.V2;


namespace JH.CodeAssignment.Api.Controllers;

public class TwitterInfosController : ApiController
{
    private readonly ITwitterInfoService _twitterInfoService;

    #region Helpers

    private static TwitterInfoResponse MapTwitterInfoResponse(TwitterInfo twitterInfo)
    {
        return new TwitterInfoResponse(
            twitterInfo.Id,
            twitterInfo.Name,
            twitterInfo.Description,
            twitterInfo.RequestedDateTime,
            twitterInfo.Duration,
            twitterInfo.Tweets,
            twitterInfo.Top10,
            twitterInfo.Note);
    }

    private IActionResult CreatedAtGetTwitterInfo(TwitterInfo twitterInfo)
    {
        return CreatedAtAction(
            actionName: nameof(GetTwitterInfo),
            routeValues: new { id = twitterInfo.Id },
            value: MapTwitterInfoResponse(twitterInfo));
    }

    #endregion Helpers

    public TwitterInfosController(IConfiguration config, ITwitterInfoService twitterInfoService) : base(config)
    {
        _twitterInfoService = twitterInfoService;
    }

    [HttpPost]
    public IActionResult CreateTwitterInfo(CreateTwitterInfoRequest request)
    {
        ErrorOr<TwitterInfo> twitterInfoRequest = TwitterInfo.Create(
            request.Name,
            request.Description,
            request.Duration,
            new List<TweetV2>(),
            new Dictionary<string, int>());

        if(twitterInfoRequest.IsError)
            return Problem(twitterInfoRequest.Errors);

        TwitterInfo twitterInfo = twitterInfoRequest.Value;
        ErrorOr<Created> result = _twitterInfoService.CreateTwitterInfo(_config, twitterInfo);

        return result.Match(
            _ => CreatedAtGetTwitterInfo(twitterInfo),
            errors => Problem(errors));
    }

    [HttpGet("{id:guid}")]
    public IActionResult GetTwitterInfo(Guid id)
    {
        ErrorOr<TwitterInfo> result = _twitterInfoService.GetTwitterInfo(id);

        return result.Match(
            twitterInfo => Ok(MapTwitterInfoResponse(twitterInfo)),
            errors => Problem(errors));
    }

    [HttpPut("{id:guid}")]
    public IActionResult UpdateInsertTwitterInfo(Guid id, UpdateInsertTwitterInfoRequest request)
    {
        ErrorOr<TwitterInfo> twitterInfoRequest = TwitterInfo.Create(
            request.Name,
            request.Description,
            request.Duration,
            request.Tweets,
            request.Top10,
            id);

        if(twitterInfoRequest.IsError)
            return Problem(twitterInfoRequest.Errors);

        TwitterInfo twitterInfo = twitterInfoRequest.Value;
        ErrorOr<UpdateInsertedTwitterInfo> result = _twitterInfoService.UpdateInsertTwitterInfo(_config, twitterInfo);

        // Return 201 if new record else no content
        return result.Match(
            updateInserted => updateInserted.IsNewRecord ? CreatedAtGetTwitterInfo(twitterInfo) : NoContent(),
            errors => Problem(errors));
    }

    [HttpDelete("{id:guid}")]
    public IActionResult DeleteTwitterInfo(Guid id)
    {
        ErrorOr<Deleted> result = _twitterInfoService.DeleteTwitterInfo(id);

        return result.Match(
            _ => NoContent(),
            errors => Problem(errors));
    }
}