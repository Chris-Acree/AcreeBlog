using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using AcreeBlog.Data.Models;
using AcreeBlog.Data.Query;
using Microsoft.AspNetCore.Identity;
using AcreeBlog.Data.Query.Queries;


namespace AcreeBlog.AuthHandlers
{
    public class EditBlogPostAuthorizationHandler : AuthorizationHandler<SameAuthorRequirement, BlogPost>
  {
    private IQueryProcessorAsync _qpa;
    private UserManager<ApplicationUser> _userManager;

    public EditBlogPostAuthorizationHandler(IQueryProcessorAsync qpa,
                                            UserManager<ApplicationUser> userManager)
    {
      _userManager = userManager;
      _qpa = qpa;
    }

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, SameAuthorRequirement requirement, BlogPost resource)
    {
      var user = await _userManager.GetUserAsync(context.User);

      var blogPostUserId = await _qpa.ProcessAsync(
        new GetUserIdFromBlogPost
        {
          blogpost = resource
        }
      );

      if (!string.IsNullOrEmpty(blogPostUserId) &&
          blogPostUserId == user.Id)
      {
        context.Succeed(requirement);
      }

    }
  }

  public class SameAuthorRequirement : IAuthorizationRequirement { }

}
