using JH.CodeAssignment.Api.Models;
using ErrorOr;

namespace JH.CodeAssignment.Api.Services.TwitterInfos;
public interface ITwitterInfoService
{
    ErrorOr<Created> CreateTwitterInfo(IConfiguration config, TwitterInfo request);
    ErrorOr<TwitterInfo> GetTwitterInfo(Guid id);
    ErrorOr<UpdateInsertedTwitterInfo> UpdateInsertTwitterInfo(IConfiguration config, TwitterInfo twitterInfo);
    ErrorOr<Deleted> DeleteTwitterInfo(Guid id);
}