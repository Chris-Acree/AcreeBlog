using AcreeBlog.Data.Models;
using AcreeBlog.Data.Query;
using AcreeBlog.Data.Query.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace AcreeBlog.AuthHandlers
{
    public class EditAuthorProfileAuthorizationHandler : AuthorizationHandler<AuthorIsUserRequirement, Author>
  {
    private readonly IQueryProcessorAsync _qpa;
    private readonly UserManager<ApplicationUser> _userManager;

    public EditAuthorProfileAuthorizationHandler(IQueryProcessorAsync qpa,
                                                 UserManager<ApplicationUser> userManager)
    {
      _qpa = qpa;
      _userManager = userManager;
    }

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, AuthorIsUserRequirement requirement, Author resource)
    {
      var user = await _userManager.GetUserAsync(context.User);

      var author = await _qpa.ProcessAsync(new GetAuthorByAppUserIdQuery { Id = user.Id });

      if (author != null &&
          author.Id == resource.Id)
      {
        context.Succeed(requirement);
      }
    }
  }

  public class AuthorIsUserRequirement : IAuthorizationRequirement 
  {
  }
}
